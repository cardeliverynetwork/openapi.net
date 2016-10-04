
namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Class to hold various settings for a CDN region
    /// </summary>
    public class RegionSettings
    {
        /// <summary>
        /// Readonly - Gets the CDN Region to which these settings apply
        /// </summary>
        public virtual string Region { get; set; }

        /// <summary>
        /// Readonly - Gets the vinDELIVER API URL for this device registration
        /// </summary>
        public virtual string ApiUrl { get; set; }

        /// <summary>
        /// Readonly - Gets the vinDELIVER2 API URL for this device registration
        /// </summary>
        public virtual string Api2Url { get; set; }

        /// <summary>
        /// Readonly - Gets the JSON Forwarder URL for this device registration
        /// </summary>
        public virtual string JsonForwarderUrl { get; set; }

        /// <summary>
        /// Readonly - Gets the vinYARD API URL for this device registration
        /// </summary>
        public virtual string VinYardApiUrl { get; set; }

        /// <summary>
        /// Readonly - Gets the SPOG API URL for this device registration
        /// </summary>
        public virtual string SpogApiUrl { get; set; }
    }
}
