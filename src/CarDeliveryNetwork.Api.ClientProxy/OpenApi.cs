using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using CarDeliveryNetwork.Api.Data;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.ClientProxy
{
    /// <summary>
    /// A wrapper class for the CDN OpenApi
    /// </summary>
    public class OpenApi : ICdnApi
    {
        private readonly MessageFormat _interfaceFormat = MessageFormat.Json;
        private string _username;
        private string _password;

        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        public string Uri { get; private set; }

        /// <summary>
        /// Gets the App.
        /// </summary>
        /// <value>
        /// The application that constructed this instance of OpenApi.
        /// </value>
        public string App { get; private set; }

        /// <summary>
        /// Gets or sets the API key of the calling user.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the username of the calling user.
        /// </summary>
        /// <value>
        /// The Username.
        /// </value>
        public string Username
        {
            get { return _username; }
            set
            {
                if (!IsApi2)
                {
                    throw new ArgumentException("Basic authentication cannot be used before API v2");
                }
                _username = value;
            }
        }

        /// <summary>
        /// Gets or sets the password of the calling user.
        /// </summary>
        /// <value>
        /// The Password.
        /// </value>
        public string Password
        {
            get { return _password; }
            set
            {
                if (!IsApi2)
                {
                    throw new ArgumentException("Basic authentication cannot be used before API v2");
                }
                _password = value;
            }
        }

        /// <summary>
        /// Indicates whether the client is pointing at API2
        /// </summary>
        /// <value>
        /// IsApi2
        /// </value>
        public bool IsApi2 { get { return Uri.Contains("vind2"); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.OpenApi"/> class.
        /// </summary>
        /// <param name="uri">The uri of the target service.</param>
        public OpenApi(string uri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                throw new ArgumentException("uri string cannot be null or empty");
            Uri = uri;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.OpenApi"/> class.
        /// </summary>
        /// <param name="uri">The uri of the target service.</param>
        /// <param name="apiKey">Your API key.</param>
        /// <param name="app">The application constructing this OpenApi instance.</param>
        public OpenApi(string uri, string apiKey, string app = null)
            : this(uri)
        {
            ApiKey = apiKey;
            App = app;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.ClientProxy.OpenApi"/> class.
        /// </summary>
        /// <param name="uri">The uri of the target service.</param>
        /// <param name="username">Your username.</param>
        /// <param name="password">Your password.</param>
        /// <param name="app">The application constructing this OpenApi instance.</param>
        public OpenApi(string uri, string username, string password, string app = null)
            : this(uri)
        {
            Username = username;
            Password = password;
            App = app;
        }

        /// <summary>
        /// Gets the job of the specified LoadId from Car Delivery Network.
        /// </summary>
        /// <param name="loadId">LoadId of the job to get.</param>
        /// <param name="callParams">Paramaters to be added to the API Call URL</param>
        /// <returns>The job of the specified LoadId.</returns>
        public Job GetJob(string loadId, string callParams = null)
        {
            if (string.IsNullOrEmpty(loadId))
                throw new Exception("LoadId must be non null and at least 1 character in length");
            var resource = string.Format("Jobs/{0}", loadId);
            return Job.FromString(CallWithRetry(resource, "GET", true, null, callParams), _interfaceFormat);
        }

        /// <summary>
        /// Gets the job of the specified Id from Car Delivery Network.
        /// </summary>
        /// <param name="id">Id of the job to get.</param>
        /// <param name="callParams">Paramaters to be added to the API Call URL</param>
        /// <returns>The job of the specified Id.</returns>
        public Job GetJob(int id, string callParams = null)
        {
            if (id == 0)
                throw new Exception("Id must be greater than zero");
            var resource = string.Format("Jobs/{0}", id);
            return Job.FromString(CallWithRetry(resource, "GET", false, null, callParams), _interfaceFormat);
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
            return Documents.FromString(CallWithRetry(resource, "GET"), _interfaceFormat);
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
            return Documents.FromString(CallWithRetry(resource, "GET", true), _interfaceFormat);
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
            return Proof.FromString(CallWithRetry(resource, "GET"), _interfaceFormat);
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
        public virtual Jobs CreateJobs(Jobs jobs)
        {
            if (jobs == null || jobs.Count == 0)
                throw new ArgumentException("Jobs collection was null or empty");
            return Jobs.FromString(CallWithRetry("jobs", "POST", false, jobs), _interfaceFormat);
        }

        /// <summary>
        /// Updates the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="id">Id of job to update.</param>
        /// <param name="job">The job update.</param>
        /// <returns>The successfully updated job.</returns>
        public Job UpdateJob(int id, Job job)
        {
            if (id < 1)
                throw new ArgumentException("id must be positive and non zero");
            return UpdateJob(string.Format("Jobs/{0}", id), job, false);
        }

        /// <summary>
        /// Updates the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="loadId">LoadId of job to update.</param>
        /// <param name="job">The job update.</param>
        /// <returns>The successfully updated job.</returns>
        public Job UpdateJob(string loadId, Job job)
        {
            if (string.IsNullOrWhiteSpace(loadId))
                throw new ArgumentException("loadId must be populated");
            return UpdateJob(string.Format("Jobs/{0}", loadId), job, true);
        }

        private Job UpdateJob(string resource, Job job, bool isUsingLoadIds)
        {
            if (job == null)
                throw new ArgumentException("Job connot be null");
            return Job.FromString(CallWithRetry(resource, "PUT", isUsingLoadIds, job), _interfaceFormat);
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
            return Vehicles.FromString(CallWithRetry(resource, "POST", false, vehicles), _interfaceFormat);
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
            return Vehicles.FromString(CallWithRetry(resource, "POST", true, vehicles), _interfaceFormat);
        }

        /// <summary>
        /// Gets a collection of vehicles form the job of the specified Id
        /// </summary>
        /// <param name="jobId">Id of the job resource</param>
        /// <returns>A collection of vehicles from the specified job</returns>
        public Vehicles GetJobVehicles(int jobId)
        {
            var resource = string.Format("Jobs/{0}/Vehicles", jobId);
            return Vehicles.FromString(CallWithRetry(resource, "GET"), _interfaceFormat);
        }

        /// <summary>
        /// Gets a collection of vehicles form the job of the specified LoadId
        /// </summary>
        /// <param name="loadId">LoadId of the job resource</param>
        /// <returns>A collection of vehicles from the specified job</returns>
        public Vehicles GetJobVehicles(string loadId)
        {
            var resource = string.Format("Jobs/{0}/Vehicles", loadId);
            return Vehicles.FromString(CallWithRetry(resource, "GET", true), _interfaceFormat);
        }

        /// <summary>
        /// Cancels the job of the specified Id giving the specified reason
        /// </summary>
        /// <param name="id">Id of job to cancel</param>
        /// <param name="reason">Reason for job cancellation</param>
        public virtual void CancelJob(int id, string reason)
        {
            if (id == 0)
                throw new Exception("Id must be greater than zero");
            PerformJobAction(id, new Data.Action { Name = "cancel", Note = reason });
        }

        /// <summary>
        /// Cancels the job of the specified LoadId giving the specified reason
        /// </summary>
        /// <param name="loadId">LoadId of job to cancel</param>
        /// <param name="reason">Reason for job cancellation</param>
        public void CancelJob(string loadId, string reason)
        {
            if (string.IsNullOrEmpty(loadId))
                throw new Exception("LoadId must be populated");
            PerformJobAction(loadId, new Data.Action { Name = "cancel", Note = reason });
        }

        /// <summary>
        /// Attempts to create the specified users on Car Delivery Network.
        /// </summary>
        /// <param name="users">The collection of users to create.</param>
        /// <returns>A collection of the successfully created users.</returns>
        public Users CreateUsers(Users users)
        {
            if (users == null || users.Count == 0)
                throw new ArgumentException("Users collection was null or empty");
            return Users.FromString(CallWithRetry("Users", "POST", false, users), _interfaceFormat);
        }

        /// <summary>
        /// Gets the home fleet of the calling user
        /// </summary>
        /// <returns>The home fleet of the calling user</returns>
        public Fleet GetHomeFleet()
        {
            return Fleet.FromString(CallWithRetry("HomeFleet", "GET"), _interfaceFormat);
        }

        /// <summary>
        /// Attempts to create the specified fleets on Car Delivery Network.
        /// </summary>
        /// <param name="fleets">The collection of fleets to create.</param>
        /// <returns>A collection of the successfully created fleets.</returns>
        public Users CreateFleets(Fleets fleets)
        {
            if (fleets == null || fleets.Count == 0)
                throw new ArgumentException("Fleets collection was null or empty");
            return Users.FromString(CallWithRetry("Fleets", "POST", false, fleets), _interfaceFormat);
        }

        /// <summary>
        /// Creates the specified devices against the specified Fleet.
        /// </summary>
        /// <param name="fleetId">The Fleet Id or 2 RH fields from CMAC</param>
        /// <param name="devices">The devices to create</param>
        /// <param name="isCmac">Indicates that the fleetId is a CMAC</param>
        /// <param name="sendTestJob">Indicates that a test job should be sent to the new device</param>
        /// <returns>The created devices</returns>
        public Devices CreateFleetDevices(string fleetId, Devices devices, bool isCmac, bool sendTestJob)
        {
            if (devices == null || devices.Count == 0)
                throw new ArgumentException("Devices collection was null or empty");
            var resource = string.Format("Fleets/{0}/Devices", fleetId);
            var callParams = string.Format("iscmac={0}&sendtestjob={1}", isCmac, sendTestJob);
            return Devices.FromString(CallWithRetry(resource, "POST", false, devices, callParams), _interfaceFormat);
        }

        /// <summary>
        /// Creates the specified trucks against the specified Fleet.
        /// </summary>
        /// <param name="fleetId">The Fleet IdC</param>
        /// <param name="trucks">The trucks to create</param>
        /// <param name="isCmac">Indicates that the fleetId is a CMAC</param>
        /// <returns>The created devices</returns>
        public Trucks CreateFleetTrucks(string fleetId, Trucks trucks, bool isCmac)
        {
            if (trucks == null || trucks.Count == 0)
                throw new ArgumentException("Trucks collection was null or empty");
            var resource = string.Format("Fleets/{0}/Trucks", fleetId);
            var callParams = string.Format("iscmac={0}", isCmac);
            return Trucks.FromString(CallWithRetry(resource, "POST", false, trucks, callParams), _interfaceFormat);
        }

        /// <summary>
        /// Attempts to create the specified master destinations on Car Delivery Network.
        /// </summary>
        /// <param name="destinations">The collection of master destinations to create.</param>
        /// <returns>A collection of the successfully created destinations.</returns>
        public ContactDetailss CreateMasterDestinations(ContactDetailss destinations)
        {
            if (destinations == null || destinations.Count == 0)
                throw new ArgumentException("Destinations collection was null or empty");
            return ContactDetailss.FromString(Call("Destinations", "POST", false, destinations), _interfaceFormat);
        }

        /// <summary>
        /// Sends the hooks for the specified hook event, of the specified schema
        /// </summary>
        /// <param name="id">Id of job to send hooks for</param>
        /// <param name="hookEvent">The hook event to send for</param>
        /// <param name="doForSchema">The hook schame to send for</param>
        public void SendJobHooks(int id, WebHookEvent hookEvent, string doForSchema)
        {
            var action = new Data.Action
            {
                Name = "sendhook",
                AssociatedId = hookEvent.ToString()
            };

            var resource = string.Format("Jobs/{0}/Action", id);
            var callParams = string.Format("sendhook_schema={0}", doForSchema);
            CallWithRetry(resource, "POST", false, action, callParams);
        }

        /// <summary>
        /// Allocates the specified jobs to this fleet resource.
        /// </summary>
        /// <param name="id">Resource Id</param>
        /// <param name="jobs">The list of jobs to allocate to this fleet resource</param>
        /// <param name="queryStringParameters">Additonal parameters: scheduled dates and buy price</param>
        public virtual void AllocateJobsToFleet(int id, Jobs jobs, string queryStringParameters = null)
        {
            var resource = string.Format("Fleets/{0}/Jobs", id);
            Call(resource, "PUT", false, jobs, queryStringParameters);
        }

        private void PerformJobAction(int id, Data.Action action)
        {
            PerformAction("Jobs", id, action);
        }

        private void PerformJobAction(string loadId, Data.Action action)
        {
            PerformAction("Jobs", loadId, action);
        }

        private void PerformAction(string resourceName, int id, Data.Action action)
        {
            if (action == null)
                throw new ArgumentException("Action cannot be null");
            if (string.IsNullOrEmpty(action.Name))
                throw new ArgumentException("Action.Name must be populated");

            var resource = string.Format("{0}/{1}/Action", resourceName, id);
            CallWithRetry(resource, "POST", false, action);
        }

        private void PerformAction(string resourceName, string loadId, Data.Action action)
        {
            if (action == null)
                throw new ArgumentException("Action cannot be null");
            if (string.IsNullOrEmpty(action.Name))
                throw new ArgumentException("Action.Name must be populated");

            var resource = string.Format("{0}/{1}/Action", resourceName, loadId);
            CallWithRetry(resource, "POST", true, action);
        }

        /// <summary>
        /// Calls the API with the specified resource and method.  Retries 3 times on "Unable to connect to the remote server"
        /// </summary>
        /// <param name="resource">The target resource.</param>
        /// <param name="method">The HTTP method to perform on the target resource.</param>
        /// <param name="isUsingLoadIds">When true, the target resource is identified by a client specified LoadId.</param>
        /// <param name="data">The data body for POST and PUT methods.</param>
        /// <param name="callParams">Paramater string to be added to the end of the API call.</param>
        /// <returns>The response string from the API call.</returns>
        public string CallWithRetry(
            string resource, 
            string method, 
            bool isUsingLoadIds = false, 
            IApiEntity data = null, 
            string callParams = null)
        {
            var exceptions = new List<Exception>();
            int doTries = 3;

            for (int retry = 0; retry < doTries; retry++)
                try
                {
                    return Call(resource, method, isUsingLoadIds, data, callParams);
                }
                catch (WebException ex)
                {
                    // If something other than cannot reach server, throw immediately
                    if (!ex.Message.Contains("Unable to connect to the remote server"))
                        throw;

                    exceptions.Add(ex);

                    // Pause for increasingly long time (2, 4, 8, 16ish seconds, etc)
                    Thread.Sleep(new Random().Next(900, 1100) * (retry + 1));
                }

            throw new AggregateException(exceptions);
        }

        /// <summary>
        /// Calls the API with the specified resource and method.
        /// </summary>
        /// <param name="resource">The target resource.</param>
        /// <param name="method">The HTTP method to perform on the target resource.</param>
        /// <param name="isUsingLoadIds">When true, the target resource is identified by a client specified LoadId.</param>
        /// <param name="data">The data body for POST and PUT methods.</param>
        /// <param name="callParams">Paramater string to be added to the end of the API call.</param>
        /// <returns>The response string from the API call.</returns>
        public string Call(
            string resource, 
            string method, 
            bool isUsingLoadIds = false, 
            IApiEntity data = null, 
            string callParams = null)
        {
            // Remove whitespace, get rid of leading ? or &
            callParams = string.IsNullOrWhiteSpace(callParams)
                ? ""
                : new Regex(@"\s+").Replace(callParams, "").TrimStart('?', '&').Insert(0, "&");

            // Add the 'app' paramater if we have it
            if (!string.IsNullOrWhiteSpace(App))
                callParams += string.Concat("&app=", App);

            var requestUri =string.Format("{0}/{1}?apikey={2}&isloadid={3}{4}", Uri, resource, ApiKey, isUsingLoadIds, callParams);
            var req = WebRequest.Create(requestUri) as HttpWebRequest;
            req.KeepAlive = false;
            req.ContentType = "application/" + _interfaceFormat.ToString().ToLower();
            req.Method = method.ToUpper();

            var isUsingBasicAuth = !string.IsNullOrWhiteSpace(Username);
            if (isUsingBasicAuth)
            {
                var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Username + ":" + Password));
                req.Headers.Add("Authorization", "Basic " + encoded);
            }
            
            if (method == "POST" || method == "PUT")
            {
                var json = data == null
                    ? null
                    : data.ToString(_interfaceFormat);

                var buffer = !string.IsNullOrEmpty(json)
                    ? Encoding.UTF8.GetBytes(json)
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
                if (response == null)
                {
                    throw;
                }

                // If API2, read the structured response body for error info
                if (IsApi2)
                {
                    using (var respStream = new StreamReader(response.GetResponseStream()))
                    {
                        var responseBody = respStream.ReadToEnd();

                        if (!string.IsNullOrWhiteSpace(responseBody))
                        {
                            var error = ApiError.FromString(responseBody, _interfaceFormat);
                            if (error != null && error.ResponseStatus != null)
                            {
                                throw new HttpResourceFaultException(response.StatusCode, error.ResponseStatus.Message, ex);
                            }
                        }
                    }  
                }

                // If we haven't re-thrown yet, throw what we've got
                throw new HttpResourceFaultException(response.StatusCode, response.StatusDescription, ex);
            }
        }
    }
}
