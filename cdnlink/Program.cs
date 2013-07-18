using System;
using System.Text;
using System.Threading;

namespace CdnLink
{
    class Program
    {
        // Global application name for single instance mutex
        const string AppName = "Global\\CdnLink";

        public static int Main(string[] args)
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
                    return 0;
                }
                else
                {
                    Console.WriteLine("Another instance of the program was already running");
                    return 1;
                }
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
