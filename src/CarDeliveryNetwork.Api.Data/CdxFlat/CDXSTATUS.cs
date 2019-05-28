using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CDXSTATUS : CDxMessageBase
    {
        private List<Vehicle> _vehicles;
        private CdxShipment _shipment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipment"></param>
        /// <param name="vehicles"></param>
        public CDXSTATUS(CdxShipment shipment, List<Vehicle> vehicles)
        {
            _vehicles = vehicles;
            _shipment = shipment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forEvent"></param>
        /// <param name="eventDateTime"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string ToString(WebHookEvent forEvent, DateTime eventDateTime, out string fileName)
        {
            var flatFile = new StringBuilder();

            flatFile.AppendFormat("\"{0}\",\"{1:yyyy-MM-dd hh:mm:ss}\",\"{2}\",\"{3}\"{4}",
                "CDXSTATUS",
                eventDateTime,
<<<<<<< Updated upstream
                _job.LoadId, 
=======
                _shipment.ExchangeId,
                _shipment.SenderLoadId,
>>>>>>> Stashed changes
                Eol
                );

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\"{18}",
                "SHIPMENT",
                _shipment.SenderScac,
                _shipment.ReceiverScac,
                _shipment.SenderJobNumber,
                _shipment.SenderLoadId,
                _shipment.SenderTripId,
                _shipment.ReceiverJobNumber,
                _shipment.ReceiverLoadId,
                _shipment.ReceiverTripId,
                _shipment.DriverId,
                _shipment.DriverId,
                _shipment.TruckId,
                _shipment.TruckId,
                null, // Lat
                null, // Lon
                forEvent,
                null, // EtaDateTime
                null,  // Gate Code
                Eol
                );

            foreach (var v in _vehicles)
            {
                flatFile.AppendFormat("\"{0}\",\"{1}\"{2}", 
                    "VEHICLE",
                    v.Vin,
                    Eol
                    );
            }

            flatFile.Append("\"CDXEND\"");

            fileName = string.Format("CDXSTATUS_{0}_{1}_{2}_{3:s}.IN", forEvent, _shipment.ExchangeId, _shipment.SenderJobNumber, DateTime.UtcNow);

            return flatFile.ToString();
        }
    }
}
