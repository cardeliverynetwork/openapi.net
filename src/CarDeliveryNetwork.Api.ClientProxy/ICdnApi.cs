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
        /// Creates the specified job on Car Delivery Network.
        /// </summary>
        /// <param name="job">The job to create.</param>
        /// <returns>The successfully created job.</returns>
        Job CreateJob(Job job);
    }
}
