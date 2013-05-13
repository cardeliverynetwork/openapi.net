using System;
using CarDeliveryNetwork.Api.Data;
using Nancy;
using Nancy.ModelBinding;

namespace CdnHook
{
    public class JobsModule : NancyModule
    {
        public JobsModule()
            : base("/jobs")
        {
            Put["/"] = UpdateJob;
        }

        private Response UpdateJob(dynamic o)
        {
            try
            {
                // Deserialize the job on the request body
                var job = this.Bind<Job>();

                //
                // Your job update code here
                //

                // If all's well - return 200 OK
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                // Error - return 500 Error
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}