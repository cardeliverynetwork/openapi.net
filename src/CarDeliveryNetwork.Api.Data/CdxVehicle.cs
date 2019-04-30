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
    }
}
