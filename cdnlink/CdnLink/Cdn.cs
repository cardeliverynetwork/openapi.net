using System;
using System.Linq;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using log4net;

namespace CdnLink
{
    public class Cdn
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Cdn));

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

            var sendCount = sends != null ? sends.Count() : 0;
            if (sendCount > 0)
            {
                _log.InfoFormat("Send: Processing {0} records(s).", sendCount);

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
                _log.Info("Send: Nothing to do.");

            return sendCount;
        }

        public int Receive()
        {
            var files = FtpBox.GetFileList();
            var fileCount = files != null ? files.Count : 0;
            if (fileCount > 0)
            {
                _log.InfoFormat("Receive: Processing {0} file(s).", fileCount);

                var db = new CdnLinkDataContext(ConnectionString);

                foreach (var file in files.ToArray())
                {
                    // If we haven't already processed this file
                    var seenFile = db.CdnReceivedFtpFiles.Where(f => f.Filename.Contains(file)).Count() > 0;
                    if (!seenFile)
                    {
                        var receivedFile = new CdnReceivedFtpFile();
                        receivedFile.Filename = file;
                        receivedFile.JsonMessage = FtpBox.GetFileContents(file);

                        var receive = new CdnReceive();
                        receive.FetchedDate = DateTime.Now;
                        receive.Status = (int)CdnReceive.ReceiveStatus.Processing;

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
                _log.Info("Receive: Nothing to do.");

            return fileCount;
        }
    }
}
