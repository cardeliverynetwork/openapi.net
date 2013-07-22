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
        const string AppName = "Global\\CdnLink";
        static readonly ILog Log = LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            try
            {
                using (var mutex = new Mutex(false, AppName))
                    if (mutex.WaitOne(0, false))
                    {
                        var hasArg = args != null && args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]);
                        var arg = hasArg ? args[0].ToLower() : null;
                        var cdn = new Cdn(
                            Helpers.GetSetting("CDNLINK_CONNECTIONSTRING"),
                            Helpers.GetSetting("CDNLINK_API_KEY"),
                            Helpers.GetSetting("CDNLINK_API_URL"),
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
                    else
                    {
                        Log.Info("Another instance of the program was already running.");
                    }
            }
            catch (HttpResourceFaultException ex)
            {
                Log.ErrorFormat("HttpResourceFaultException: StatusCode: {0} Message: {1}", ex.StatusCode, ex.Message);
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
