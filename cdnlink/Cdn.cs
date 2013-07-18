using System;
using System.Linq;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using log4net;

namespace CdnLink
{
    internal static class Cdn
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Cdn));

        internal static int Send()
        {
            var api = new OpenApi(GetSetting("CDNLINK_API_URL"), GetSetting("CDNLINK_API_KEY"));
            var db = new CdnLinkDataContext();

            var sends = from send in db.CdnSends
                        where send.Status == (int)CdnSend.SendStatus.Processing || send.Status == (int)CdnSend.SendStatus.Queued 
                        orderby send.QueuedDate
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

        internal static int Receive()
        {
            var ftp = new FtpBox(
                GetSetting("CDNLINK_FTP_HOST"),
                GetSetting("CDNLINK_FTP_ROOT"),
                GetSetting("CDNLINK_FTP_USER"),
                GetSetting("CDNLINK_FTP_PASS"));

            var files = ftp.GetFileList();
            var fileCount = files != null ? files.Count : 0;
            if (fileCount > 0)
            {
                _log.InfoFormat("Receive: Processing {0} file(s).", fileCount);

                var db = new CdnLinkDataContext();

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

        /// <summary>
        /// Gets the specified setting from the system environment or web.config, in that order
        /// </summary>
        /// <param name="name">Name of setting to fetch</param>
        /// <returns>The specified setting</returns>
        private static string GetSetting(string name)
        {
            _log.DebugFormat("GetSetting: '{0}'.", name);
            var setting = Environment.GetEnvironmentVariable(name);
            if (setting != null)
            {
                _log.DebugFormat("GotSetting: '{0}' from system environment.", setting);
                return setting;
            }

            setting = Settings.Default[name] as string;
            if (setting != null)
            {
                _log.DebugFormat("GotSetting: '{0}' from application settings.", setting);
                return setting;
            }

            _log.DebugFormat("GetSetting: '{0}' was not found.", name);
            return null;
        }
    }
}
