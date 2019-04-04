using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A CDc vehicle exchange
    /// </summary>
    public class CdxVehicleExchange : ApiEntityBase<CdxVehicleExchange>
    {
        private static readonly new Type[] KnownTypes = new Type[]
        {
            typeof(Vehicle),
            typeof(ContactDetails)
        };

        /// <summary>
        /// The Shipment associated with this CdxVehicleExchange
        /// </summary>
        public CdxShipment Shipment { get; set; }

        /// <summary>
        /// The vehicles associated with this CdxVehicleExchange
        /// </summary>
        public List<CdxVehicle> CdxVehicles { get; set; }

        /// <summary>
        /// Reads the JSON or XML document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="format">The format of the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public new static CdxVehicleExchange FromString(string serializedObject, MessageFormat format)
        {
            return Serialization.Deserialise<CdxVehicleExchange>(serializedObject, format, KnownTypes);
        }

        /// <summary>
        /// Serialise this CdxVehicleExchange to XML
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Serialization.Serialize(this, MessageFormat.Xml, KnownTypes);
        }
    }
}
