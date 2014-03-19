using System;
using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Signature entity
    /// </summary>
    public class Signature : IApiEntity
    {
        /// <summary>
        /// Readonly - The url of the Signature
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Readonly - SignedBy
        /// </summary>
        public string SignedBy { get; set; }

        /// <summary>
        /// Readonly - The sign time, or time the signature was declared as not available
        /// </summary>
        public DateTime? Time { get; set; }

        /// <summary>
        /// Readonly - A list of reasons that a signature was not supplied
        /// </summary>
        public List<string> NotSignedReasons { get; set; }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return ToString(Types.MessageFormat.Json);
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
