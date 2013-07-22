using System;
using System.Linq;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using log4net;

namespace CdnLink
{
    public class CdnLink
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CdnLink));

        public string ConnectionString { get; private set; }
        public ICdnApi Api { get; private set; }
        public ICdnFtpBox FtpBox { get; private set; }

        public CdnLink(
            string connectionString,
            ICdnApi api,
            ICdnFtpBox ftpBox)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentException("connectionString string cannot be null or empty");

            ConnectionString = connectionString;
            Api = api;
            FtpBox = ftpBox;
        }

        public int Send()
        {
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
                        Api.CreateJob(send.CdnSendLoad.ToCdnJob());

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
            var fileCount = files.Count;
            if (fileCount > 0)
            {
                Log.InfoFormat("Receive: Processing {0} file(s).", fileCount);

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

            return fileCount;
        }
    }
}
