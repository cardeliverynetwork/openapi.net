using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using log4net;

namespace CdnLink
{
    public class CdnLink
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CdnLink));

        private const string ScacCacheFile = "~scac.cache";
        private const char LoadIdPrefixEnd = '-';

        private Dictionary<string, string> _apiKeysByScac;
        private string _loadIdPrefix { get; set; }

        public string ConnectionString { get; private set; }
        public ICdnApi Api { get; private set; }
        public ICdnFtpBox FtpBox { get; private set; }
        public bool IsKeyByScacLookupMode { get { return _apiKeysByScac != null && _apiKeysByScac.Count > 0; } }

        public CdnLink(
            string connectionString,
            ICdnApi api,
            ICdnFtpBox ftp,
            Dictionary<string, string> apiKeysByScacs = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("Config setting CDNLINK_CONNECTIONSTRING must be populated");
            if (string.IsNullOrWhiteSpace(api.Uri))
                throw new ArgumentException("Config setting CDNLINK_API_URL must be populated");
            if (string.IsNullOrWhiteSpace(ftp.Host))
                throw new ArgumentException("Config setting CDNLINK_FTP_HOST must be populated");
            if (string.IsNullOrWhiteSpace(api.ApiKey) && (_apiKeysByScac == null || _apiKeysByScac.Count == 0))
                throw new ArgumentException("Config setting CDNLINK_API_KEY or apiKeyScacList must be populated");

            ConnectionString = connectionString;
            Api = api;
            FtpBox = ftp;
            _apiKeysByScac = apiKeysByScacs;
            _loadIdPrefix = IsKeyByScacLookupMode
                ? null
                : GetApiKeyScac(api.ApiKey);
        }

        public int Send()
        {
            var db = new CdnLinkDataContext(ConnectionString);
            var sends = from send in db.CdnSends
                        where send.Status == (int)CdnSend.SendStatus.Queued
                        select send;

            var sendCount = sends.Count();
            Log.InfoFormat("Send: Processing {0} record{1}.", sendCount, sendCount == 1 ? "" : "s");
            if (sendCount > 0)
            {
                foreach (var send in sends)
                {
                    try
                    {
                        // Set the send as in process
                        send.ProcessingDate = DateTime.Now;
                        send.Status = (int)CdnSend.SendStatus.Processing;
                        db.SubmitChanges();

                        // If we're using loadId and have a prefix, add it now.
                        var loadId = PrepareApiForSend(send);
                        var theJob = send.CdnSendLoad.ToCdnJob();
                        theJob.LoadId = loadId;

                        switch (send.Action)
                        {
                            case null:
                            case (int)CdnSend.SendAction.Create:
                                Api.CreateJob(theJob);
                                break;

                            case (int)CdnSend.SendAction.Cancel:
                                Api.CancelJob(loadId, null);
                                break;

                            case (int)CdnSend.SendAction.Update:
                                Api.UpdateJob(loadId, theJob);
                                break;

                            default:
                                throw new ArgumentException(string.Format("Action {0} is not supported", send.Action), "Action");
                        }

                        // Set the send as sent
                        send.SentDate = DateTime.Now;
                        send.Status = (int)CdnSend.SendStatus.Sent;
                    }
                    catch (HttpResourceFaultException ex)
                    {
                        send.SetAsError(ex.Message, ex.StatusCode.ToString());
                        throw;
                    }
                    catch (Exception ex)
                    {
                        send.SetAsError(ex.Message);
                        throw;
                    }
                    finally
                    {
                        db.SubmitChanges();
                    }
                }
            }
            return sendCount;
        }

        public int Receive()
        {
            var files = FtpBox.GetFileList();
            var fileCount = files.Count;
            Log.InfoFormat("Receive: Processing {0} file{1}.", fileCount, fileCount == 1 ? "" : "s");
            if (fileCount > 0)
            {
                var db = new CdnLinkDataContext(ConnectionString);
                var index = 1;
                foreach (var file in files.ToArray())
                {
                    // If we haven't already processed this file
                    var seenFile = db.CdnReceivedFtpFiles.Count(f => f.Filename.Contains(file)) > 0;
                    if (!seenFile)
                    {
                        Log.InfoFormat("Processing file {0} of {1}: {2}", index++, fileCount, file);
                        var json = FtpBox.GetFileContents(file);
                        var job = Job.FromString(json);
                        if (job == null)
                            throw new Exception(string.Format("JSON produced a null job: {0}", json));

                        // Check that this isn't a test hook message generated by CDN
                        if (job.Id != -1)
                        {
                            // If we have a loadId prefix, strip it now
                            job.LoadId = StripLoadIdPrefix(job.LoadId);

                            var receivedFile = new CdnReceivedFtpFile
                            {
                                Filename = file,
                                JsonMessage = json,
                                CdnReceive = new CdnReceive
                                {
                                    FetchedDate = DateTime.Now,
                                    Status = (int)CdnReceive.ReceiveStatus.Processing
                                }
                            };

                            db.CdnReceivedFtpFiles.InsertOnSubmit(receivedFile);
                            db.SubmitChanges();

                            try
                            {
                                Log.Debug("Receive: Setting the received load");
                                receivedFile.CdnReceivedLoads.Add(new CdnReceivedLoad(job));
                                receivedFile.CdnReceive.Status = (int)CdnReceive.ReceiveStatus.Queued;
                                db.SubmitChanges();
                                Log.Debug("Receive: Done.");
                            }
                            catch (Exception ex)
                            {
                                Log.Debug("Receive: Error:  Writing error info to DB...");
                                receivedFile.CdnReceive.SetAsError(ex.Message);
                                db.SubmitChanges();
                                throw;
                            }
                        }
                    }

                    // Delete file from FTP server
                    Log.DebugFormat("Receive: Deleting FTP file: {0} ...", file);
                    FtpBox.DeleteFile(file);
                }
            }
            return fileCount;
        }

        private string PrepareApiForSend(CdnSend send)
        {
            var workingScac = _loadIdPrefix;

            if (IsKeyByScacLookupMode)
            {
                var shipperScac = send.CdnSendLoad.ShipperScac;
                if (string.IsNullOrWhiteSpace(shipperScac))
                    throw new ArgumentException("ShipperScac must always be populated when using a SCAC/ApiKey lookup list");
                if (!_apiKeysByScac.ContainsKey(shipperScac))
                    throw new ArgumentException(string.Format("apiKeyScacList did not contain an entry for SCAC '{0}'", shipperScac));

                workingScac = shipperScac;
                Api.ApiKey = _apiKeysByScac[shipperScac];
            }

            // Return the prefixed load Id we'll use for this send
            if (!string.IsNullOrWhiteSpace(send.LoadId) &&
                !string.IsNullOrWhiteSpace(workingScac) &&
                !send.LoadId.StartsWith(workingScac))
            {
                return string.Format("{0}{1}{2}", workingScac, LoadIdPrefixEnd, send.LoadId);
            }
            return send.LoadId;
        }

        private string StripLoadIdPrefix(string loadId)
        {
            if (string.IsNullOrWhiteSpace(loadId))
                return "";

            return loadId.Contains(LoadIdPrefixEnd)
                ? loadId.Remove(0, loadId.IndexOf(LoadIdPrefixEnd) + 1)
                : loadId;
        }

        private string GetApiKeyScac(string apiKey, bool update = false)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                return null;

            var cachefile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), ScacCacheFile);
            var scac = File.Exists(cachefile)
                ? File.ReadAllText(cachefile)
                : null;

            if (string.IsNullOrEmpty(scac) || update)
            {
                scac = Api.GetHomeFleet().Scac;
                File.WriteAllText(cachefile, scac);
            }
            return scac;
        }
    }
}
