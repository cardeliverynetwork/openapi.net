using System;
using System.Data.SqlClient;
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
                var hasArg = args != null && args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]);
                var arg = hasArg ? args[0].ToLower() : null;
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
                else if (arg.Contains("version"))
                {
                    Console.WriteLine(GetVersionString());
                }
                else
                {
                    Console.WriteLine(GetUsageString());
                }
                return 0;
            }
            catch (HttpResourceFaultException ex)
            {
                Log.ErrorFormat("HttpResourceFaultException: StatusCode: {0} Message: {1}", ex.StatusCode, ex.Message);
                Console.WriteLine(ex);
                return 1;
            }
            catch (SqlException ex)
            {
                Log.Error(ex.Message, ex);
                if (ex.Message.Contains("Invalid column name"))
                    Console.WriteLine("{0}.  Try running upgrade.sql?", ex.Message);
                else
                    Console.WriteLine(ex);
                return 1;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static string GetVersionString()
        {
            var assemblyVersion = Assembly.GetAssembly(typeof(Program)).GetName().Version;
            return string.Format(
                "CdnLink v{0}.{1}.{2}",
                assemblyVersion.Major,
                assemblyVersion.Minor,
                assemblyVersion.Build);
        }

        private static string GetUsageString()
        {
            var usage = new StringBuilder();
            usage.AppendLine(GetVersionString());
            usage.AppendLine("Usage:");
            usage.AppendLine("    > cdnlink          ... Sends loads to CDN and receives updates from FTP");
            usage.AppendLine("    > cdnlink /receive ... Receives waiting updates from FTP");
            usage.AppendLine("    > cdnlink /send    ... Sends loads to CDN");
            usage.AppendLine("    > cdnlink /version ... Prints CdnLink version info");
            return usage.ToString();
        }
    }
}
