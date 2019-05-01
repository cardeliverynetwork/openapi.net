﻿using System;
using System.Collections.Generic;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    public class CdxMessage : ApiEntityBase<CdxMessage>
    {
        private static readonly Type[] KnownTypes = new Type[]
        {
            typeof(Vehicle),
            typeof(ContactDetails),
            typeof(DamageItem)
        };

        public CdxShipment Shipment { get; set; }
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

        public override string ToString()
        {
            return Serialization.Serialize(this, MessageFormat.Xml, KnownTypes);
        }
    }
}
