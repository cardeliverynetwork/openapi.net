using System;
using System.Globalization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A CDx shipment
    /// </summary>
    public class CdxShipment
    {
        /// <summary>
        /// A unique Id for this CDx shipment
        /// </summary>
        public int? ExchangeId { get; set; }

        /// <summary>
        /// Time shipment was created at client (Format: 2019-04-16 10:28:17)
        /// </summary>
        public string EventDateTime { get; set; }

        /// <summary>
        /// SenderInventoryId
        /// </summary>
        public string SenderInventoryId { get; set; }

        /// <summary>
        /// The Sender's SCAC
        /// </summary>
        public string SenderScac { get; set; }

        /// <summary>
        /// the receiver's SCAC
        /// </summary>
        public string ReceiverScac { get; set; }

        /// <summary>
        /// A job number identifying this shipment to the sender
        /// </summary>
        public string SenderJobNumber { get; set; }

        /// <summary>
        /// A Load Id identifying this load to the sender
        /// </summary>
        public string SenderLoadId { get; set; }

        /// <summary>
        /// A Load Id identifying this trip to the receiver
        /// </summary>
        public string ReceiverTripId { get; set; }

        /// <summary>
        /// A job number identifying this shipment to the receiver
        /// </summary>
        public string ReceiverJobNumber { get; set; }

        /// <summary>
        /// A Load Id identifying this load to the receiver
        /// </summary>
        public string ReceiverLoadId { get; set; }

        /// <summary>
        /// A Load Id identifying this trip to the sender
        /// </summary>
        public string SenderTripId { get; set; }

        /// <summary>
        /// A price for the entire shipment
        /// </summary>
        public int? Price { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// TruckId
        /// </summary>
        public string TruckId { get; set; }

        /// <summary>
        /// DriverId 
        /// </summary>
        public string DriverId { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        ///  Gets a DateTime representation of the EventDateTime string
        /// </summary>
        /// <returns></returns>
        public DateTime GetEventDateTime()
        {
            DateTime eventDateTime = DateTime.UtcNow;

            DateTime.TryParseExact(
                EventDateTime,
                "yyyy-MM-dd hh:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, 
                out eventDateTime);

            return eventDateTime;
        }
    }
}
