using System;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Signature entity
    /// </summary>
    public class Signature : IApiEntity
    {
        /// <summary>
        /// The url of the Signature
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// SignedBy
        /// </summary>
        public string SignedBy { get; set; }

        /// <summary>
        /// The sign time, or time the signature was declared as not available
        /// </summary>
        public DateTime? Time { get; set; }

        /// <summary>
        /// The reason that a signature was not supplied
        /// </summary>
        public string NotSignedReason { get; set; }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return this.ToString(Types.MessageFormat.Json);
        }

        /// <summary>
        /// Returns a serial representation of the object in the specified format.
        /// </summary>
        /// <param name="format">Format to serialize to.</param>
        /// <returns>The serialized object.</returns>
        public string ToString(Types.MessageFormat format)
        {
            return Serialization.Serialize(this, format);
        }
    }
}
