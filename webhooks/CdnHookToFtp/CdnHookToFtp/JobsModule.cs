using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Configuration;
using FluentFTP;
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
            // Example: http://build.cardeliverynetwork.com/jobs?ftphost=ftp://ftp.example.com/dir&ftpuser=theuser&ftppass=thepass&filename=CDN1234&fileextension=IN

            // For ePoC or ePoD drop only (BCA)
            // Example: http://build.cardeliverynetwork.com/jobs?ftphost=ftp://ftp.example.com/dir&ftpuser=theuser&ftppass=thepass&filename=CDN1234.pdf&getbody=true

            // Don't add an extension to the file
            // Example: http://build.cardeliverynetwork.com/jobs?ftphost=ftp://ftp.example.com/dir&ftpuser=theuser&ftppass=thepass&filename=CDN1234.pdf&addfileextension=false

            // Use FTPS
            // Example: http://build.cardeliverynetwork.com/jobs?ftphost=ftp://ftp.example.com/dir&ftpuser=theuser&ftppass=thepass&ftptype=ftps

            Put["/"] = UpdateJob;

            Post["/"] = UpdateJob;
        }

        private Response UpdateJob(dynamic _)
        {
            try
            {
                // Get the host, user and pass from URL, environment or web.config
                var ftpHost = Request.Query.ftphost.Value ?? GetSetting(FtpHostVariable);
                var ftpUser = Request.Query.ftpuser.Value ?? GetSetting(FtpUserVariable);
                var ftpPass = Request.Query.ftppass.Value ?? GetSetting(FtpPassVariable);

                FtpType ftpType = FtpType.Ftp;
                Enum.TryParse(Request.Query.ftptype.Value ?? "ftp", true, out ftpType);

                // Use passed filename from URL or create a unique one
                var fileName = Request.Query.filename.Value ?? Guid.NewGuid().ToString();

                // Find out if the body is just a URL to get
                var getbody = Request.Query.getbody.Value == "true";
                if (getbody)
                {
                    string fetchUrl;

                    using (var reader = new StreamReader(Context.Request.Body, Encoding.UTF8))
                    {
                        fetchUrl = reader.ReadToEnd();
                    }

                    using (var response = WebRequest.Create(fetchUrl).GetResponse())
                    using (var responseStream = response.GetResponseStream())
                    {
                        UploadFile(ftpHost, ftpUser, ftpPass, responseStream, fileName, ftpType);
                    }
                }
                else
                {
                    // If explicitly specified as true, or unspecified, add a file extension
                    var addFileExtension = Request.Query.addfileextension.Value == "true" || Request.Query.addfileextension.Value == null;

                    // Use passed fileextension from URL or get based on request content type
                    var fileExtension = addFileExtension
                        ? (Request.Query.fileextension.Value ?? GetRequestType(Request).ToString().ToLower())
                        : null;

                    // The full filename to be PUT to FTP root
                    var ftpFile = fileExtension == null
                        ? fileName
                        : string.Format("{0}.{1}", fileName, fileExtension);

                    UploadFile(ftpHost, ftpUser, ftpPass, Context.Request.Body, ftpFile, ftpType);
                }

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
        /// Uploads the specified stream to FTP or SFTP
        /// </summary>
        /// <param name="ftpHost">The ftp host and path eg: ftp://ftp.example.com/customer/in </param>
        /// <param name="ftpUser">The ftp username</param>
        /// <param name="ftpPass">The ftp password</param>
        /// <param name="stream">Stream to write to the request</param>
        /// <param name="ftpFileName">File name of remote file</param>
        /// <param name="ftpType">The Type of FTP to use</param>
        private void UploadFile(string ftpHost, string ftpUser, string ftpPass, Stream stream, string ftpFileName, FtpType ftpType)
        {
            // Create a Uri object to help parse up the host and path
            var ftpHostUri = new Uri(ftpHost.TrimEnd('/'));

            switch (ftpType)
            {
                case FtpType.FtpS:
                    UploadFileToFfpS(ftpHostUri, ftpUser, ftpPass, stream, ftpFileName);
                    break;
                case FtpType.Ftp:
                default:
                    UploadFileToFtp(ftpHostUri, ftpUser, ftpPass, stream, ftpFileName);
                    break;
            }
        }

        /// <summary>
        /// Uploads the specified stream to SFTP
        /// </summary>
        /// <param name="ftpHost">The ftp host and path eg: ftp://ftp.example.com/customer/in </param>
        /// <param name="ftpUser">The ftp username</param>
        /// <param name="ftpPass">The ftp password</param>
        /// <param name="stream">Stream to write to the request</param>
        /// <param name="ftpFileName">File name of remote file</param>
        private void UploadFileToFfpS(Uri ftpHost, string ftpUser, string ftpPass, Stream stream, string ftpFileName)
        { 
            // The full path and filename whilst uploading in progress
            var uploadPath = string.Format("{0}/{1}.incomplete", ftpHost.AbsolutePath.TrimEnd('/'), ftpFileName);
            var renamePath = string.Format("{0}/{1}", ftpHost.AbsolutePath.TrimEnd('/'), ftpFileName);

            // create an FTP client
            var client = new FtpClient();
            client.Host = ftpHost.Host;
            client.Credentials = new NetworkCredential(ftpUser, ftpPass);
            client.Connect();
            client.UploadFile(stream, uploadPath);
            client.Rename(uploadPath, renamePath);
            client.Disconnect();
        }

        /// <summary>
        /// Uploads the specified stream to FTP
        /// </summary>
        /// <param name="ftpHost">The ftp host and path eg: ftp://ftp.example.com/customer/in </param>
        /// <param name="ftpUser">The ftp username</param>
        /// <param name="ftpPass">The ftp password</param>
        /// <param name="stream">Stream to write to the request</param>
        /// <param name="ftpFileName">File name of remote file</param>
        private void UploadFileToFtp(Uri ftpHost, string ftpUser, string ftpPass, Stream stream, string ftpFileName)
        {
            // The full path and filename whilst uploading in progress
            var ftpUploadingPath = string.Format("{0}/{1}.incomplete", ftpHost.AbsoluteUri.TrimEnd('/'), ftpFileName);

            // Create the FTP upload request
            var uploadRequest = GetFtpRequest(ftpUploadingPath, ftpUser, ftpPass, WebRequestMethods.Ftp.UploadFile);
            uploadRequest.ContentLength = Context.Request.Body.Length;

            // Copy stream to the FTP upload request stream
            using (var requestStream = uploadRequest.GetRequestStream())
            {
                stream.CopyTo(requestStream);
                requestStream.Close();
            }

            // Create the FTP rename request
            var renameRequest = GetFtpRequest(ftpUploadingPath, ftpUser, ftpPass, WebRequestMethods.Ftp.Rename);
            renameRequest.RenameTo = string.Format("./{0}", ftpFileName);

            // Upload file
            using (var response = (FtpWebResponse)uploadRequest.GetResponse())
                response.Close();

            // Rename file
            using (var response = (FtpWebResponse)renameRequest.GetResponse())
                response.Close();
        }

        /// <summary>
        /// Creates an ftp request with the specified credentias and method
        /// </summary>
        private FtpWebRequest GetFtpRequest(string requestUriString, string ftpUser, string ftpPass, string method)
        {
            var request = (FtpWebRequest)WebRequest.Create(requestUriString);
            request.Credentials = new NetworkCredential(ftpUser, ftpPass);
            request.Method = method;
            return request;
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