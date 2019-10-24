using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A vehicle extension for CDx
    /// </summary>
    public class CdxVehicle : Vehicle
    {
        /// <summary>
        /// The Shipper's SCAC
        /// </summary>
        public string ShipperScac { get; set; }
        
        /// <summary>
        /// Vehicle Type
        /// </summary>
        public string VehicleType { get; set; }

        /// <summary>
        /// A unique reference number for this vehicle 
        /// </summary>
        public string ReferenceNumber { get; set; }

        /// <summary>
        /// The price for moving this vehicle
        /// </summary>
        public int? Price { get; set; }

        /// <summary>
        /// Scheduled Pickup Date
        /// </summary>
        public DateTime? ScheduledPickupDate { get; set; }

        /// <summary>
        /// Scheduled Delivery Date
        /// </summary>
        public DateTime? ScheduledDeliveryDate { get; set; }

        /// <summary>
        /// This vehicle's origin or pickup location
        /// </summary>
        public ContactDetails Origin { get; set; }

        /// <summary>
        /// This vehicle's eventual destination
        /// </summary>
        public ContactDetails Destination { get; set; }

        /// <summary>
        /// Sign off comment
        /// </summary>
        public string SignOffComment { get; set; }

        /// <summary>
        /// CdxShipmentStatus of this CDX vehicle
        /// </summary>
        public CdxShipmentStatus? CdxDeliveryStatus { get; set; }

        /// <summary>
        /// The RemoteId (unique Id) for the driver assigned to this vehicle 
        /// </summary>
        public string DriverId { get; set; }

        /// <summary>
        /// The RemoteId (unique Id) for the truck assigned to this vehicle 
        /// </summary>
        public string TruckId { get; set; }

        /// <summary>
        /// The job that this vehicle is part of
        /// </summary>
        public string ReceiverJobNumber { get; set; }

        /// <summary>
        /// The load Id used by the end carrier
        /// </summary>
        public string ReceiverLoadId { get; set; }

        /// <summary>
        /// The trip Id used by the end carrier
        /// </summary>
        public string ReceiverTripId { get; set; }

        /// <summary>
        /// The Latitude at which this status event occurred
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// The Longitude at which this status event occurred
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// The gate out code for PickedUp /OnWayToDeliver events
        /// </summary>
        public string GateCode { get; set; }
    }
}
