using System;
using System.Net;
using System.Web.Configuration;
using Nancy;

namespace CdnHookToFtp
{
    public class JobsModule : NancyModule
    {
        const string EnvironmentFirstVariable = "ENVIRONMENT_FIRST";
        const string FtpHostVariable = "CDN_FTP_HOST";
        const string FtpUserVariable = "CDN_FTP_USER";
        const string FtpPassVariable = "CDN_FTP_PASS";

        public JobsModule()
            : base("/jobs")
        {
            // GET - Just to show the service is up
            Get["/"] = _ => "Jobs";

            // PUT - To put a job update to server-configured FTP
            // Example: http://example.com/jobs
            // Example: http://example.com/jobs?ftphost=ftp://ftp.example.com/dir&ftpuser=theuser&ftppass=thepass
            Put["/"] = UpdateJob;
        }

        private Response UpdateJob(dynamic _)
        {
            try
            {
                // Get the host, user and pass from environment or web.config
                var ftpHost = (Request.Query.ftphost.Value ?? GetSetting(FtpHostVariable)).Trim('/');
                var ftpUser = Request.Query.ftpuser.Value ?? GetSetting(FtpUserVariable);
                var ftpPass = Request.Query.ftppass.Value ?? GetSetting(FtpPassVariable);

                // Create a unique filename
                var fileName = Guid.NewGuid().ToString();

                // Get extension based on request content type
                var fileExtension = GetRequestType(Request).ToString().ToLower();

                // The full filename to be PUT to FTP root
                var ftpFile = string.Format("{0}/{1}.{2}", ftpHost, fileName, fileExtension);

                // Create the FTP request
                var request = (FtpWebRequest)WebRequest.Create(ftpFile);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(ftpUser, ftpPass);
                request.ContentLength = Context.Request.Body.Length;

                using (var requestStream = request.GetRequestStream())
                {
                    // Copy the body of this request to the new request stream
                    Context.Request.Body.CopyTo(requestStream);
                    requestStream.Close();
                }

                using (var response = (FtpWebResponse)request.GetResponse())
                    response.Close();

                return Nancy.HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Status = Nancy.HttpStatusCode.InternalServerError,
                    Message = ex.GetBaseException().Message
                };

                var response = GetRequestType(Request) == RequestType.Json
                    ? Response.AsJson(error)
                    : Response.AsXml(error);
                response.StatusCode = Nancy.HttpStatusCode.InternalServerError;
                return response;
            }
        }

        /// <summary>
        /// Gets the specified setting from the system environment or web.config, in that order
        /// </summary>
        /// <param name="name">Name of setting to fetch</param>
        /// <returns>The specified setting</returns>
        private string GetSetting(string name)
        {
            var environentFirstSetting = WebConfigurationManager.AppSettings[EnvironmentFirstVariable];
            var isEnvironmentFirst = !string.IsNullOrWhiteSpace(environentFirstSetting) && environentFirstSetting.ToLower() == "true";
            return isEnvironmentFirst
                ? Environment.GetEnvironmentVariable(name) ?? WebConfigurationManager.AppSettings[name]
                : WebConfigurationManager.AppSettings[name];
        }

        /// <summary>
        /// Get the request type (base on Content-Type) of the specified request
        /// </summary>
        /// <param name="request">The request</param>
        /// <returns>The request type</returns>
        private RequestType GetRequestType(Request request)
        {
            return request.Headers.ContentType.Contains("json")
                ? RequestType.Json
                : RequestType.Xml;
        }
    }

    public enum RequestType
    {
        Json,
        Xml
    }

    public class Error
    {
        public Nancy.HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}