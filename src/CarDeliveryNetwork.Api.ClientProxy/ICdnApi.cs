using CarDeliveryNetwork.Api.Data;

namespace CarDeliveryNetwork.Api.ClientProxy
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICdnApi
    {
        /// <summary>
        /// Gets the URI.
        /// </summary>
        /// <value>
        /// The URI.
        /// </value>
        string Uri { get;}

        /// <summary>
        /// Gets the API key.
        /// </summary>
        /// <value>
        /// The API key.
        /// </value>
        string ApiKey { get; }

        /// <summary>
        /// Gets the App.
        /// </summary>
        /// <value>
        /// The application that constructed this instance of OpenApi.
        /// </value>
        string App { get; }

        /// <summary>
        /// Creates the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="job">The job to create.</param>
        /// <returns>The successfully created job.</returns>
        Job CreateJob(Job job);

        /// <summary>
        /// Cancels the job of the specified Id giving the specified reason.
        /// </summary>
        /// <param name="id">Id of job to cancel.</param>
        /// <param name="reason">Reason for job cancellation.</param>
        void CancelJob(int id, string reason);

        /// <summary>
        /// Cancels the job of the specified LoadId giving the specified reason
        /// </summary>
        /// <param name="loadId">LoadId of job to cancel.</param>
        /// <param name="reason">Reason for job cancellation.</param>
        void CancelJob(string loadId, string reason);

        /// <summary>
        /// Updates the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="id">Id of job to update.</param>
        /// <param name="job">The job update.</param>
        /// <returns>The successfully updated job.</returns>
        Job UpdateJob(int id, Job job);

        /// <summary>
        /// Updates the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="loadId">LoadId of job to update.</param>
        /// <param name="job">The job update.</param>
        /// <returns>The successfully updated job.</returns>
        Job UpdateJob(string loadId, Job job);
    }
}
