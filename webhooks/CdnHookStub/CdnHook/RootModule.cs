using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Nancy;

namespace CdnHook
{
    public class RootModule : NancyModule
    {
        public RootModule() 
        {
            Get["/"] = _ =>
                {
                    var version = Assembly.GetAssembly(typeof(RootModule)).GetName().Version;
                    return "Car Delivery Network ASP.NET + Nancy WebHook Stub v" + version.ToString();
                };
        }
    }
}