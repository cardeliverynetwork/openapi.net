using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;

namespace CdnHook
{
    public class JobsModule : NancyModule
    {
        public JobsModule() : base("/jobs")
        {
            Post["/{id}"] = parameters =>
            {
                var id = parameters.id;
                return HttpStatusCode.OK;
            };
        }
    }
}