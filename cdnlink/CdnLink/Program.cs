using System;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

        public static int Main(string[] args)
        {
            try
            {
                if (args == null || args.Length == 0)
                    Console.WriteLine(GetUsageString());
                else if (args.Length == 1 && args[0].Contains("/version"))
                    Console.WriteLine(GetVersionString());
                else
                {
                    var cdn = new CdnLink(
                        Helpers.GetSetting("CDNLINK_CONNECTIONSTRING"),
                        new OpenApi(
                            Helpers.GetSetting("CDNLINK_API_URL"),
                            Helpers.GetSetting("CDNLINK_API_KEY"),
                            "CdnLink"),
                        new FtpBox(
                            Helpers.GetSetting("CDNLINK_FTP_HOST"),
                            Helpers.GetSetting("CDNLINK_FTP_ROOT"),
                            Helpers.GetSetting("CDNLINK_FTP_USER"),
                            Helpers.GetSetting("CDNLINK_FTP_PASS")),
                            Helpers.GetDictionarySetting("CDNLINK_SCAC_API_KEY_LOOKUP"));

                    if (args.Contains("/send"))
                        cdn.Send();
                    if (args.Contains("/receive"))
                        cdn.Receive();
                }
                return 0;
            }
            catch (ArgumentException ex)
            {
                Log.Error(ex.Message);
                return 1;
            }
            catch (HttpResourceFaultException ex)
            {
                Log.ErrorFormat("HttpResourceFaultException: StatusCode: {0} Message: {1}", ex.StatusCode, ex.Message);
                return 1;
            }
            catch (SqlException ex)
            {
                var message = ex.Message.Contains("Invalid column name")
                    ? string.Format("{0}.  Try running upgrade.sql?", ex.Message)
                    : ex.ToString();
                Log.Error(message);
                return 1;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return 1;
            }
        }

        private static string GetUsageString()
        {
            var usage = new StringBuilder();
            usage.AppendLine(GetVersionString());
            usage.AppendLine();
            usage.AppendLine("Examples:");
            usage.AppendLine("  > cdnlink /send          ... Sends loads to CDN");
            usage.AppendLine("  > cdnlink /receive       ... Receives waiting updates from FTP");
            usage.AppendLine("  > cdnlink /version       ... Prints CdnLink version info");
            usage.AppendLine("  > cdnlink /send /receive ... Send and receive");
            return usage.ToString();
        }

        private static string GetVersionString()
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            return string.Format("CdnLink v{0}.{1}.{2}", version.Major, version.Minor, version.Build);
        }
    }
}
