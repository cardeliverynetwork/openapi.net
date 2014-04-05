using System;
using System.Data.SqlClient;
using System.Text;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CdnLink
{
    class Program
    {
        static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            try
            {
                var hasArg = args != null && args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]);
                var arg = hasArg ? args[0].ToLower() : null;
                var cdn = new CdnLink(
                    Helpers.GetSetting("CDNLINK_CONNECTIONSTRING"),
                    new OpenApi(
                        Helpers.GetSetting("CDNLINK_API_URL"),
                        Helpers.GetSetting("CDNLINK_API_KEY")),
                    new FtpBox(
                        Helpers.GetSetting("CDNLINK_FTP_HOST"),
                        Helpers.GetSetting("CDNLINK_FTP_ROOT"),
                        Helpers.GetSetting("CDNLINK_FTP_USER"),
                        Helpers.GetSetting("CDNLINK_FTP_PASS")));

                if (!hasArg)
                {
                    while (cdn.Send() > 0) { }
                    while (cdn.Receive() > 0) { }
                }
                else if (arg.Contains("send"))
                {
                    while (cdn.Send() > 0) { }
                }
                else if (arg.Contains("receive"))
                {
                    while (cdn.Receive() > 0) { }
                }
                else
                {
                    PrintUsage();
                }
            }
            catch (HttpResourceFaultException ex)
            {
                Log.ErrorFormat("HttpResourceFaultException: StatusCode: {0} Message: {1}", ex.StatusCode, ex.Message);
                throw;
            }
            catch (SqlException ex)
            {
                Log.Error(ex.Message, ex);
                if (ex.Message.Contains("Invalid column name"))
                    Console.WriteLine("{0}.  Try running upgrade.sql?", ex.Message);
                else
                    throw;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw;
            }
        }

        private static void PrintUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("Usage:");
            usage.AppendLine("    > cdnlink          ... Sends loads to CDN and receives waiting updates from FTP");
            usage.AppendLine("    > cdnlink /send    ... Sends loads to CDN");
            usage.AppendLine("    > cdnlink /receive ... Receives waiting updates from FTP");
            Console.WriteLine(usage);
        }
    }
}
