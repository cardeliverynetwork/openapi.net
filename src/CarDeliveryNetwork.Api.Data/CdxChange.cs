using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A CDX change
    /// </summary>
    public class CdxChange : ApiEntityBase<CdxChange>
    {
        private static readonly Type[] KnownTypes = new Type[]
        {
            typeof(Vehicle),
            typeof(ContactDetails)
        };

        /// <summary>
        /// The shipment associated with this change
        /// </summary>
        public CdxShipment Shipment { get; set; }

        /// <summary>
        /// The vehicles associated with this change
        /// </summary>
        public List<CdxVehicleChange> CdxVehicles { get; set; }

        /// <summary>
        /// Reads the JSON or XML document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="format">The format of the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public new static CdxChange FromString(string serializedObject, MessageFormat format)
        {
            return Serialization.Deserialise<CdxChange>(serializedObject, format, KnownTypes);
        }
    }
}
