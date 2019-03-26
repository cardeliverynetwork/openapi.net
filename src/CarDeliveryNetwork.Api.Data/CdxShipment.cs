using System;

namespace CarDeliveryNetwork.Api.Data
{
    public class CdxShipment
    {
        public int? ExchangeId { get; set; }
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
        public int? Price { get; set; }
        public string Notes { get; set; }
        public string TruckId { get; set; }
        public string DriverId { get; set; }
    }
}
