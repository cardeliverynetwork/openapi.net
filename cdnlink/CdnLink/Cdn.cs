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

        private string _connectionString;
        private string _apiUrl; 
        private string _apiKey; 
        private string _ftpHost; 
        private string _ftpRoot; 
        private string _ftpUsr; 
        private string _ftpPass;

        public Cdn(
            string connectionString, 
            string apiUrl, 
            string apiKey, 
            string ftpHost,
            string ftpRoot,
            string ftpUsr,
            string ftpPass)
        {
            _connectionString = connectionString;
            _apiUrl = apiUrl;
            _apiKey = apiKey;
            _ftpHost = ftpHost;
            _ftpRoot = ftpRoot;
            _ftpUsr = ftpUsr;
            _ftpPass = ftpPass;
        }

        public int Send()
        {
            var api = new OpenApi(_apiUrl, _apiKey);
            var db = new CdnLinkDataContext(_connectionString);

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
            var ftp = new FtpBox(_ftpHost, _ftpRoot, _ftpUsr, _ftpPass);
            var files = ftp.GetFileList();
            var fileCount = files != null ? files.Count : 0;
            if (fileCount > 0)
            {
                _log.InfoFormat("Receive: Processing {0} file(s).", fileCount);

                var db = new CdnLinkDataContext(_connectionString);

                foreach (var file in files)
                {
                    // If we haven't already processed this file
                    if (db.CdnReceivedFtpFiles.Where(f => f.Filename.Contains(file)).Count() == 0)
                    {
                        var receivedFile = new CdnReceivedFtpFile();
                        receivedFile.Filename = file;
                        receivedFile.JsonMessage = ftp.GetFileContents(file);
                        db.CdnReceivedFtpFiles.InsertOnSubmit(receivedFile);
                        db.SubmitChanges();
                    }

                    // Delete file from FTP server
                    ftp.DeleteFile(file);
                }
            }
            else
                _log.Info("Receive: Nothing to do.");

            return fileCount;
        }
    }
}
