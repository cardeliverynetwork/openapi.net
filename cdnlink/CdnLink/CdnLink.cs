﻿using System;
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
        private const string ScacCacheFile = "~scac.cache";
        private static readonly ILog Log = LogManager.GetLogger(typeof(CdnLink));
        private string LoadIdPrefix { get; set; }

        public string ConnectionString { get; private set; }
        public Dictionary<string, string> ScacApiKeys { get; private set; }
        public ICdnApi Api { get; private set; }
        public ICdnFtpBox FtpBox { get; private set; }

        public CdnLink(
            string connectionString,
            ICdnApi api,
            ICdnFtpBox ftpBox,
            Dictionary<string, string> apiKeysByScacs = null)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("connectionString string cannot be null or empty");

            ConnectionString = connectionString;
            Api = api;
            FtpBox = ftpBox;
            LoadIdPrefix = GetApiKeyScac(api.ApiKey);
        
            //ScacApiKeyLookup = apiKeysByScacs != null && apiKeysByScacs.Count > 0
            //    ? apiKeysByScacs
            //    : new Dictionary<string, string> { { GetApiKeyScac(api.ApiKey), api.ApiKey } };
        }

        public int Send()
        {
            var db = new CdnLinkDataContext(ConnectionString);
            var sends = from send in db.CdnSends
                        where send.Status == (int)CdnSend.SendStatus.Queued
                        select send;

            var sendCount = sends.Count();
            Log.InfoFormat("Send: Processing {0} records(s).", sendCount);
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
                        var loadId = AddLoadIdPrefix(send.LoadId);
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
            Log.InfoFormat("Receive: Processing {0} file(s).", fileCount);
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
                        Console.WriteLine("Processing file {0} of {1}", index++, fileCount);
                        var json = FtpBox.GetFileContents(file);
                        var job = Job.FromString(json);
                        if (job == null)
                            throw new Exception(string.Format("Json produced a null job: {0}", json));

                        // Check that this isn't a test hook message generated by CDN
                        if (job.Id != -1)
                        {
                            // If we have a loadId prefix, strip it now
                            job.LoadId = StripLoadIdPrefix(job.LoadId);

                            var receivedFile = new CdnReceivedFtpFile
                            {
                                Filename = file,
                                JsonMessage = json
                            };

                            var receive = new CdnReceive
                            {
                                FetchedDate = DateTime.Now,
                                Status = (int) CdnReceive.ReceiveStatus.Processing
                            };

                            receivedFile.CdnReceive = receive;

                            db.CdnReceivedFtpFiles.InsertOnSubmit(receivedFile);
                            db.SubmitChanges();

                            try
                            {
                                Log.Debug("Receive: Setting the received load");
                                receivedFile.CdnReceivedLoads.Add(new CdnReceivedLoad(job));
                                Log.Debug("Receive: Setting the status to queued");
                                receive.Status = (int) CdnReceive.ReceiveStatus.Queued;
                                Log.Debug("Receive: Submittig ...");
                                db.SubmitChanges();
                                Log.Debug("Receive: Done.");
                            }
                            catch (Exception ex)
                            {
                                Log.Debug("Receive: Error:  Setting error info ...");
                                receive.SetAsError(ex.Message);
                                Log.Debug("Receive: Error:  Saving error info ...");
                                db.SubmitChanges();
                                Log.Debug("Receive: Re-throwing.");
                                throw;
                            }
                        }
                    }

                    // Delete file from FTP server
                    Log.DebugFormat("Receive: Deleting FTP file: {0} ...", file);
                    FtpBox.DeleteFile(file);
                    Log.Debug("Receive: Done.");
                }
            }
            return fileCount;
        }

        private string AddLoadIdPrefix(string loadId)
        {
            if (!string.IsNullOrWhiteSpace(loadId) &&
                !string.IsNullOrWhiteSpace(LoadIdPrefix) &&
                !loadId.StartsWith(LoadIdPrefix))
            {
                return string.Format("{0}-{1}", LoadIdPrefix, loadId);
            }
            return loadId;
        }

        private string StripLoadIdPrefix(string loadId)
        {
            var prefix = LoadIdPrefix + "-";
            return loadId != null && loadId.StartsWith(prefix)
                ? loadId.Remove(0, prefix.Length)
                : loadId;
        }

        private string GetApiKeyScac(string apiKey, bool update = false)
        {
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
