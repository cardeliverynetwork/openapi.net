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

        private Response UpdateJob(dynamic o)
        {
            try
            {
                // Deserialize the job on the request body
                var job = this.Bind<Job>();

                // If we're using CDN Ids 
                if (job.Id == 0)
                    throw new Exception("Received job Id of 0");

                // If we're using RemoteIds
                if (string.IsNullOrWhiteSpace(job.RemoteId))
                    throw new Exception("Received empty RemoteId");

                //
                //
                // Your job update code here ...
                //
                //

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                var response = Request.Headers.ContentType.Contains("json")
                    ? Response.AsJson(new Error { Message = ex.GetBaseException().Message })
                    : Response.AsXml(new Error { Message = ex.GetBaseException().Message });
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }

    public class Error
    {
        public string Message { get; set; }
    }
}