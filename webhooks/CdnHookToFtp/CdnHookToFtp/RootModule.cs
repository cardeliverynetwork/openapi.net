using System.Reflection;
using Nancy;


namespace CdnHookToFtp
{
    public class RootModule : NancyModule
    {
        public RootModule() 
        {
            // GET - Just to show the service is up
            Get("/", _ =>
                {
                    var version = Assembly.GetAssembly(typeof(RootModule)).GetName().Version;
                    return "Car Delivery Network ASP.NET + Nancy FTP WebHook Stub v" + version.ToString();
                });
        }
    }
}