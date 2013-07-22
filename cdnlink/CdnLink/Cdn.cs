using System;
using System.Linq;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using log4net;

namespace CdnLink
{
    public class Cdn
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Cdn));

        public string ConnectionString { get; private set; }
        public string ApiUrl { get; private set; }
        public string ApiKey { get; private set; }
        public ICdnFtpBox FtpBox { get; private set; }

        public Cdn(
            string connectionString,
            string apiUrl,
            string apiKey,
            ICdnFtpBox ftpBox)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("connectionString string cannot be null or empty");
            if (string.IsNullOrWhiteSpace(apiUrl))
                throw new ArgumentException("apiUrl string cannot be null or empty");
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new ArgumentException("apiKey string cannot be null or empty");
            if (ftpBox == null)
                throw new ArgumentException("ftpBox cannot be null");

            ConnectionString = connectionString;
            ApiUrl = apiUrl;
            ApiKey = apiKey;
            FtpBox = ftpBox;
        }

        public int Send()
        {
            var api = new OpenApi(ApiUrl, ApiKey);
            var db = new CdnLinkDataContext(ConnectionString);
            var sends = from send in db.CdnSends
                        where send.Status == (int)CdnSend.SendStatus.Queued
                        select send;

            var sendCount = sends.Count();
            if (sendCount > 0)
            {
                Log.InfoFormat("Send: Processing {0} records(s).", sendCount);

                foreach (var send in sends)
                    try
                    {
                        // Set the send as in process
                        send.ProcessingDate = DateTime.Now;
                        send.Status = (int)CdnSend.SendStatus.Processing;
                        db.SubmitChanges();

                        // Send to CDN
                        api.CreateJob(send.CdnSendLoad.ToCdnJob());

                        // Set the send as sent
                        send.SentDate = DateTime.Now;
                        send.Status = (int)CdnSend.SendStatus.Sent;
                        db.SubmitChanges();
                    }
                    catch (HttpResourceFaultException ex)
                    {
                        send.SetAsError(ex.Message, ex.StatusCode.ToString());
                        db.SubmitChanges();
                        throw;
                    }
                    catch (Exception ex)
                    {
                        send.SetAsError(ex.Message);
                        db.SubmitChanges();
                        throw;
                    }
            }
            else
                Log.Info("Send: Nothing to do.");

            return sendCount;
        }

        public int Receive()
        {
            var files = FtpBox.GetFileList();
            if (files != null && files.Count > 0)
            {
                Log.InfoFormat("Receive: Processing {0} file(s).", files.Count);

                var db = new CdnLinkDataContext(ConnectionString);

                foreach (var file in files.ToArray())
                {
                    // If we haven't already processed this file
                    var seenFile = db.CdnReceivedFtpFiles.Count(f => f.Filename.Contains(file)) > 0;
                    if (!seenFile)
                    {
                        var receivedFile = new CdnReceivedFtpFile
                        {
                            Filename = file,
                            JsonMessage = FtpBox.GetFileContents(file)
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
                            receivedFile.CdnReceivedLoad = new CdnReceivedLoad(Job.FromString(receivedFile.JsonMessage));
                            receive.Status = (int)CdnReceive.ReceiveStatus.Queued;
                            db.SubmitChanges();
                        }
                        catch (Exception ex)
                        {
                            receive.SetAsError(ex.Message);
                            db.SubmitChanges();
                            throw;
                        }
                    }

                    // Delete file from FTP server
                    FtpBox.DeleteFile(file);
                }
            }
            else
                Log.Info("Receive: Nothing to do.");

            return files == null ? 0 : files.Count;
        }
    }
}
