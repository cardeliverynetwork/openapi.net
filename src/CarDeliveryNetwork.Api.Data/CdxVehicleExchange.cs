using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data
{
    public class CdxShipment
    {
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string SenderInventoryId { get; set; }
        public string SenderScac { get; set; }
        public string ReceiverScac { get; set; }
        public string SenderJobNumber { get; set; }
        public string SenderLoadId { get; set; }
        public string SenderTripId { get; set; }
        public string ReceiverJobNumber { get; set; }
        public string ReceiverLoadId { get; set; }
        public string ReceiverTripId { get; set; }
        public decimal? Price { get; set; }
        public string Notes { get; set; }
        public string TruckId { get; set; }
        public string DriverId { get; set; }
    }

    public class CdxVehicle : Vehicle
    {
        public string ShipperScac { get; set; }
        public string VehicleType { get; set; }
        public string ReferenceNumber { get; set; }
        public decimal? Price { get; set; }
        public DateTime? ScheduledPickupDate { get; set; }
        public DateTime? ScheduledDeliveryDate { get; set; }
        public ContactDetails Origin { get; set; }
        public ContactDetails Destination { get; set; }
    }

    public class CdxVehicleExchange : ApiEntityBase<CdxVehicleExchange>
    {
        public CdxShipment Shipment { get; set; }
        public List<CdxVehicle> Vehicles { get; set; }

        /// <summary>
        /// Reads the JSON or XML document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="format">The format of the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public new static CdxVehicleExchange FromString(string serializedObject, MessageFormat format)
        {
            return Serialization.Deserialise<CdxVehicleExchange>(serializedObject, format, new Type[] { typeof(Vehicle), typeof(ContactDetails) });
        }

        public override string ToString()
        {
            return Serialization.Serialize(this, Types.MessageFormat.Xml, new Type[] { typeof(Vehicle), typeof(ContactDetails) });
        }
    }
}
