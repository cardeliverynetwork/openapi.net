using System;
using System.Globalization;
using CarDeliveryNetwork.Types;

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
        /// The number of vehicles on this shipment
        /// </summary>
        public int VehicleCount { get; set; }

        /// <summary>
        ///  Gets a DateTime representation of the EventDateTime string
        /// </summary>
        /// <returns></returns>
        public DateTime GetEventDateTime()
        {
            DateTime eventDateTime;

            return DateTime.TryParseExact(
                EventDateTime,
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out eventDateTime) ? eventDateTime : DateTime.UtcNow;
        }
    }
}
