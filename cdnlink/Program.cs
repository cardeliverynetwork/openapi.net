using System;
using System.Text;
using System.Threading;
using CarDeliveryNetwork.Api.Data;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CdnLink
{
    class Program
    {
        // Global application name for single instance mutex
        const string AppName = "Global\\CdnLink";

        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            try
            {
                using (var mutex = new Mutex(false, AppName))
                {
                    if (!mutex.WaitOne(0, false))
                    {
                        _log.Info("Another instance of the program was already running.");
                        return;
                    }

                    var hasArg = args != null && args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]);
                    var arg = hasArg ? args[0].ToLower() : null;

                    var cdn = new Cdn(
                        GetSetting("CDNLINK_CONNECTIONSTRING"),
                        GetSetting("CDNLINK_API_KEY"),
                        GetSetting("CDNLINK_API_URL"),
                        GetSetting("CDNLINK_FTP_HOST"),
                        GetSetting("CDNLINK_FTP_ROOT"),
                        GetSetting("CDNLINK_FTP_USER"),
                        GetSetting("CDNLINK_FTP_PASS"));

                    if (!hasArg)
                    {
                        while (cdn.Send() > 0) ;
                        while (cdn.Receive() > 0) ;
                    }
                    else if (hasArg && arg.Contains("send"))
                    {
                        while (cdn.Send() > 0) ;
                    }
                    else if (hasArg && arg.Contains("receive"))
                    {
                        while (cdn.Receive() > 0) ;
                    }
                    else
                    {
                        PrintUsage();
                    }   
                }
            }
            catch (HttpResourceFaultException ex)
            {
                _log.ErrorFormat("HttpResourceFaultException: StatusCode: {0} Message: {1}", ex.StatusCode, ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
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

        public static string GetSetting(string name)
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
