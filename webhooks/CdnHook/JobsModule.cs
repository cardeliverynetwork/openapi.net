using System;
using CarDeliveryNetwork.Api.Data;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses.Negotiation;

namespace CdnHook
{
    public class JobsModule : NancyModule
    {
        public JobsModule()
            : base("/jobs")
        {
            Get["/"] = parameters => "Jobs";
            Put["/"] = UpdateJob;
        }

        private Negotiator UpdateJob(dynamic o)
        {
            try
            {
                // Deserialize the job on the request body
                var job = this.Bind<Job>();


                //
                // Your job update code here
                //


                // If all's well - return 200 OK
                return Negotiate
                    .WithStatusCode(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Error - return 500 Server Error and message
                return Negotiate
                    .WithModel(new Error { Message = ex.GetBaseException().Message })
                    .WithStatusCode(HttpStatusCode.InternalServerError);
            }
        }
    }

    public class Error
    {
        public string Message { get; set; }
    }
}