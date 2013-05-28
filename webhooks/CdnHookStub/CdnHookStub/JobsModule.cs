using System;
using CarDeliveryNetwork.Api.Data;
using Nancy;
using Nancy.ModelBinding;

namespace CdnHookStub
{
    public class JobsModule : NancyModule
    {
        public JobsModule()
            : base("/jobs")
        {
            // GET - Just to show the service is up
            Get["/"] = parameters => "Jobs";

            // PUT - Do the update (Could be POST if required)
            Put["/"] = UpdateJob;
        }

        private Response UpdateJob(dynamic o)
        {
            try
            {
                // Deserialize the job on the request body
                var job = this.Bind<Job>();

                // This is a test job.  Return immediate success
                if (job.Id == -1)
                    return HttpStatusCode.OK;

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
                var error = new Error { Status = HttpStatusCode.InternalServerError, Message = ex.GetBaseException().Message };
                var response = Request.Headers.ContentType.Contains("json")
                    ? Response.AsJson(error)
                    : Response.AsXml(error);
                response.StatusCode = HttpStatusCode.InternalServerError;
                return response;
            }
        }
    }

    public class Error
    {
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}