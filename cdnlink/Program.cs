using System;
using System.Text;
using System.Threading;
using log4net;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace CdnLink
{
    class Program
    {
        // Global application name for single instance mutex
        const string AppName = "Global\\CdnLink";

        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        public static int Main(string[] args)
        {
            try
            {
                using (var mutex = new Mutex(false, AppName))
                {
                    if (mutex.WaitOne(0, false))
                    {
                        if (args == null || args.Length != 1 || args[0] == null)
                        {
                            PrintUsage();
                        }
                        else if (args[0].ToLower().Contains("send"))
                        {
                            while (Cdn.Send() > 0) ;
                        }
                        else if (args[0].ToLower().Contains("receive"))
                        {
                            while (Cdn.Receive() > 0) ;
                        }
                        else
                        {
                            PrintUsage();
                        }
                    }
                    else
                        _log.Info("Another instance of the program was already running.");
                }
                return 0;
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message, ex);
                return 1;
            }
        }

        private static void PrintUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("Usage:");
            usage.AppendLine("    > cdnlink /send    ... Sends loads to CDN");
            usage.AppendLine("    > cdnlink /receive ... Receives waiting loads from CDN");
            Console.WriteLine(usage);
        }
    }
}
