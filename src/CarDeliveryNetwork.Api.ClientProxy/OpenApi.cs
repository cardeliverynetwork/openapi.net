using System;
using System.IO;
using System.Net;
using System.Text;
using CarDeliveryNetwork.Api.Data;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.ClientProxy
{
    /// <summary>
    /// A wrapper class for the CDN OpenApi
    /// </summary>
    public class OpenApi : ICdnApi
    {
        private readonly MessageFormat _interfaceFormat;

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public string Uri { get; private set; }

        /// <summary>
        /// Gets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string ApiKey { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.OpenApi"/> class.
        /// </summary>
        /// <param name="uri">The uri of the target service.</param>
        /// <param name="apiKey">Your API key.</param>
        public OpenApi(string uri, string apiKey)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentException("uri string cannot be null or empty");
            if (apiKey == null)
                throw new ArgumentException("apiKey string cannot be null");

            _interfaceFormat = MessageFormat.Json;
            Uri = uri;
            ApiKey = apiKey;
        }

        /// <summary>
        /// Gets the job of the specified LoadId from Car Delivery Network.
        /// </summary>
        /// <param name="loadId">LoadId of the job to get.</param>
        /// <returns>The job of the specified LoadId.</returns>
        public Job GetJob(string loadId)
        {
            if (string.IsNullOrEmpty(loadId))
                throw new Exception("LoadId must be non null and at least 1 character in length");
            var resource = string.Format("Jobs/{0}", loadId);
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
        /// Gets the the list of documents for the job of the specified id.
        /// </summary>
        /// <param name="id">Job Id</param>
        /// <returns>Documents on the job of the specified Id</returns>
        public Documents GetJobDocuments(int id)
        {
            if (id == 0)
                throw new Exception("Id must be greater than zero");
            var resource = string.Format("Jobs/{0}/Documents", id);
            return Documents.FromString(Call(resource, "GET"), _interfaceFormat);
        }

        /// <summary>
        /// Gets the the list of documents for the job of the specified LoadId.
        /// </summary>
        /// <param name="loadId">Job LoadId</param>
        /// <returns>Documents on the job of the specified LoadId</returns>
        public Documents GetJobDocuments(string loadId)
        {
            if (string.IsNullOrEmpty(loadId))
                throw new Exception("LoadId must be non null and at least 1 character in length");
            var resource = string.Format("Jobs/{0}/Documents", loadId);
            return Documents.FromString(Call(resource, "GET", true), _interfaceFormat);
        }

        /// <summary>
        /// Gets the proof document for the endpoint of the specified JobId and Pin.
        /// </summary>
        /// <param name="id">The Job Id</param>
        /// <param name="pin">The EndPoint Pin</param>
        /// <returns>the proof document for the endpoint of the specified JobId and Pin.</returns>
        public Proof GetProof(int id, string pin)
        {
            if (id == 0)
                throw new Exception("Id must be greater than zero");
            var resource = string.Format("Proof/{0}/{1}", id, pin);
            return Proof.FromString(Call(resource, "GET"), _interfaceFormat);
        }

        /// <summary>
        /// Create the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="job">The job to create.</param>
        /// <returns>The successfully created job.</returns>
        public Job CreateJob(Job job)
        {
            var createdJobs = CreateJobs(new Jobs { job });
            return createdJobs != null && createdJobs.Count > 0
                ? createdJobs[0]
                : null;
        }

        /// <summary>
        /// Attempts to create the specified jobs on Car Delivery Network.
        /// </summary>
        /// <param name="jobs">The collection of jobs to create.</param>
        /// <returns>A collection of the successfully created jobs.</returns>
        public Jobs CreateJobs(Jobs jobs)
        {
            if (jobs == null || jobs.Count == 0)
                throw new ArgumentException("Jobs collection was null or empty");
            return Jobs.FromString(Call("Jobs", "POST", false, jobs), _interfaceFormat);
        }

        /// <summary>
        /// Attempts to create the specified vehicles on the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="jobId">Id of the job to create vehicles against.</param>
        /// <param name="vehicles">The collection of vehicles to create.</param>
        /// <returns>A collection of the newly created vehicles.</returns>
        public Vehicles CreateJobVehicles(int jobId, Vehicles vehicles)
        {
            if (vehicles == null || vehicles.Count == 0)
                throw new ArgumentException("Vehicles collection was null or empty");
            var resource = string.Format("Jobs/{0}/Vehicles", jobId);
            return Vehicles.FromString(Call(resource, "POST", false, vehicles), _interfaceFormat);
        }

        /// <summary>
        /// Attempts to create the specified vehicles on the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="loadId">LoadId of the job to create vehicles against.</param>
        /// <param name="vehicles">The collection of vehicles to create.</param>
        /// <returns>A collection of the newly created vehicles.</returns>
        public Vehicles CreateJobVehicles(string loadId, Vehicles vehicles)
        {
            if (vehicles == null || vehicles.Count == 0)
                throw new ArgumentException("Vehicles collection was null or empty");
            var resource = string.Format("Jobs/{0}/Vehicles", loadId);
            return Vehicles.FromString(Call(resource, "POST", true, vehicles), _interfaceFormat);
        }

        /// <summary>
        /// Gets a collection of vehicles form the job of the specified Id
        /// </summary>
        /// <param name="jobId">Id of the job resource</param>
        /// <returns>A collection of vehicles from the specified job</returns>
        public Vehicles GetJobVehicles(int jobId)
        {
            var resource = string.Format("Jobs/{0}/Vehicles", jobId);
            return Vehicles.FromString(Call(resource, "GET"), _interfaceFormat);
        }

        /// <summary>
        /// Gets a collection of vehicles form the job of the specified LoadId
        /// </summary>
        /// <param name="loadId">LoadId of the job resource</param>
        /// <returns>A collection of vehicles from the specified job</returns>
        public Vehicles GetJobVehicles(string loadId)
        {
            var resource = string.Format("Jobs/{0}/Vehicles", loadId);
            return Vehicles.FromString(Call(resource, "GET", true), _interfaceFormat);
        }

        /// <summary>
        /// Cancels the job of the specified Id giving the specified reason
        /// </summary>
        /// <param name="id">Id of job to cancel</param>
        /// <param name="reason">Reason for job cancellation</param>
        public void CancelJob(int id, string reason)
        {
            if (id == 0)
                throw new Exception("Id must be greater than zero");
            PerformJobAction(id, new Data.Action { Name = "cancel", Note = reason });
        }

        private void PerformJobAction(int id, Data.Action action)
        {
            PerformAction("Jobs", id, action);
        }

        private void PerformJourneyAction(int id, Data.Action action)
        {
            PerformAction("Journeys", id, action);
        }

        private void PerformAction(string resourceName, int id, Data.Action action)
        {
            if (action == null)
                throw new ArgumentException("Action cannot be null");
            if (string.IsNullOrEmpty(action.Name))
                throw new ArgumentException("Action.Name must be populated");

            var resource = string.Format("{0}/{1}/Action", resourceName, id);
            Call(resource, "POST", false, action);
        }

        /// <summary>
        /// Calls the API with the specified resource and method.
        /// </summary>
        /// <param name="resuorce">The target resource.</param>
        /// <param name="method">The HTTP method to perform on the target resource.</param>
        /// <param name="isUsingLoadIds">When true, the target resource is identified by a client specified LoadId.</param>
        /// <param name="data">The data body for POST and PUT methods.</param>
        /// <returns>The response string from the API call.</returns>
        public string Call(string resuorce, string method, bool isUsingLoadIds = false, IApiEntity data = null)
        {
            var requestUri = string.Format("{0}/{1}?isloadid={2}&apikey={3}", Uri, resuorce, isUsingLoadIds, ApiKey);
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
                var response = ex.Response as HttpWebResponse;
                if (response != null)
                    throw new HttpResourceFaultException(response.StatusCode, response.StatusDescription, ex);
                throw;
            }
        }
    }
}
