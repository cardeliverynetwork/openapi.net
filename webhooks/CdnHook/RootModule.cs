using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace CdnHook
{
    public class RootModule : NancyModule
    {
        public RootModule() 
        {
            Get["/"] = parameters => "Car Delivery Network ASP.NET + Nancy WebHook Stub";
        }
    }
}