using System;
using System.IO;
using System.Net;
using System.Text;
using CarDeliveryNetwork.Api.ClientProxy.Exceptions;
using CarDeliveryNetwork.Api.Data;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.ClientProxy
{
    /// <summary>
    /// A wrapper class for the CDN OpenApi
    /// </summary>
    public class OpenApi
    {
        private MessageFormat _interfaceFormat;

        /// <summary>
        /// The Url of the target CDN API.
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// The calling users's CDN API key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.OpenApi"/> class.
        /// </summary>
        /// <param name="uri">The uri of the target service.</param>
        /// <param name="apiKey">Your API key.</param>
        public OpenApi(string uri = null, string apiKey = null)
        {
            Uri = uri;
            ApiKey = apiKey;
            _interfaceFormat = MessageFormat.Json;
        }

        /// <summary>
        /// Gets the job of the specified RemoteId from Car Delivery Network.
        /// </summary>
        /// <param name="remoteId">RemoteId of the job to get.</param>
        /// <returns>The job of the specified RemoteId.</returns>
        public Job GetJob(string remoteId)
        {
            if(string.IsNullOrEmpty(remoteId))
                throw new Exception("RemoteId must be non null and at least 1 character in length");
            var resource = string.Format("Jobs/{0}", remoteId);
            return Job.FromString(Call(resource, "GET", true), _interfaceFormat);
        }

        /// <summary>
        /// Gets the job of the specified Id from Car Delivery Network.
        /// </summary>
        /// <param name="id">Id of the job to get.</param>
        /// <returns>The job of the specified Id.</returns>
        public Job GetJob(int id)
        {
            if (id == 0)
                throw new Exception("Id must be greater than zero");
            var resource = string.Format("Jobs/{0}", id);
            return Job.FromString(Call(resource, "GET"), _interfaceFormat);
        }

        /// <summary>
        /// Attempts to create the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="job">The job to create.</param>
        /// <returns>The successfully created job.</returns>
        public Job CreateJob(CarDeliveryNetwork.Api.Data.Job job)
        {
            var createdJobs = CreateJobs(new Jobs() { job });
            return createdJobs != null && createdJobs.Count > 0
                ? createdJobs[0]
                : null;
        }

        /// <summary>
        /// Attempts to create the specified jobs on Car Delivery Network.
        /// </summary>
        /// <param name="jobs">The collection of jobs to create.</param>
        /// <returns>A collection of the successfully created jobs.</returns>
        public Jobs CreateJobs(CarDeliveryNetwork.Api.Data.Jobs jobs)
        {
            if (jobs == null || jobs.Count == 0)
                throw new ArgumentException("Jobs collection was null or empty");
            return Jobs.FromString(Call("Jobs", "POST", false, jobs), _interfaceFormat);
        }

        /// <summary>
        /// Calls the API with the specified resource and method.
        /// </summary>
        /// <param name="resuorce">The target resource.</param>
        /// <param name="method">The HTTP method to perform on the target resource.</param>
        /// <param name="isUsingRemoteIds">When true, the target resource is identified by a client specified RemoteId.</param>
        /// <param name="data">The data body for POST and PUT methods.</param>
        /// <returns>The response string from the API call.</returns>
        public string Call(string resuorce, string method, bool isUsingRemoteIds = false, IApiEntity data = null)
        {
            var requestUri = string.Format("{0}/{1}?isremoteid={2}&apikey={3}", Uri, resuorce, isUsingRemoteIds, ApiKey);
            var req = WebRequest.Create(requestUri) as HttpWebRequest;
            req.KeepAlive = false;
            req.ContentType = "application/" + _interfaceFormat.ToString().ToLower();
            req.Method = method.ToUpper();

            if (method == "POST" || method == "PUT")
            {
                var json = data != null
                    ? data.ToString(_interfaceFormat)
                    : null;

                var buffer = !string.IsNullOrEmpty(json)
                    ? Encoding.ASCII.GetBytes(json)
                    : new byte[] { };

                req.ContentLength = buffer.Length;
                using (Stream postData = req.GetRequestStream())
                    postData.Write(buffer, 0, buffer.Length);
            }

            try
            {
                using (var resp = req.GetResponse() as HttpWebResponse)
                using (var respStream = new StreamReader(resp.GetResponseStream()))
                    return respStream.ReadToEnd();
            }
            catch (WebException ex)
            {
                var responseBody = string.Empty;
                using (var respStream = new StreamReader(ex.Response.GetResponseStream()))
                    responseBody = respStream.ReadToEnd();

                // Trim quotation marks from the error string
                responseBody = responseBody.Trim(new char[] { '"' });

                var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                if (statusCode == HttpStatusCode.Forbidden)
                {
                    if (responseBody.Contains("Unauthorized"))
                        throw new UnauthorizedException(
                            "The request was unauthorized.  Please check that you are sending a valid API key", ex);
                    else
                        throw new ForbiddenException(responseBody, ex);
                }

                if (statusCode == HttpStatusCode.BadRequest)
                    throw new BadRequestException(responseBody, ex);

                if (statusCode == HttpStatusCode.InternalServerError)
                    throw new InternalServerErrorException(responseBody, ex);

                throw;
            }
        }

        #region Not implemented

        private void PerformAction(string resourceName, int id, CarDeliveryNetwork.Api.Data.Action action)
        {
            if (action == null)
                throw new ArgumentException("Action cannot be null");
            if (string.IsNullOrEmpty(action.Name))
                throw new ArgumentException("Action.Name must be populated");

            var resource = string.Format("{0}/{1}", resourceName, id);
            Call(resource, "POST", false, action);
        }

        /// <summary>
        /// Attempts to update the specified jobs on Car Delivery Network.
        /// </summary>
        /// <param name="job">The job to update.</param>
        /// <param name="useRemoteId">When true, indicates that the job should be identified by its RemoteId.</param>
        /// <returns></returns>
        private Jobs UpdateJob(CarDeliveryNetwork.Api.Data.Job job, bool useRemoteId = false)
        {
            if (job == null)
                throw new Exception("Job is null");
            var resource = string.Format("Jobs/{0}", useRemoteId ? job.RemoteId : job.Id.ToString());
            return Jobs.FromString(Call(resource, "PUT", useRemoteId, job), _interfaceFormat);
        }

        /// <summary>
        /// Wttempts to put the specified list of jobs into the network of the specified Id.
        /// </summary>
        /// <param name="jobs">Jobs to put into network.</param>
        /// <param name="networkId">The target network.</param>
        private void JobsToNetwork(CarDeliveryNetwork.Api.Data.Jobs jobs, int networkId)
        {
            if (jobs == null || jobs.Count == 0)
                throw new Exception("Jobs collection was null or empty");

            var jobIds = string.Empty;
            foreach (var job in jobs)
                jobIds += string.Format("{0};", job.Id);

            Call(string.Format("Jobs/{0}/Networks/{1}", jobIds, networkId), "PUT");
        }

        #endregion
    }
}
