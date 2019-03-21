using System;

namespace CarDeliveryNetwork.Api.Data
{
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
}
