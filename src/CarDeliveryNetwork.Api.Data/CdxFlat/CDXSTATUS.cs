using CarDeliveryNetwork.Types;
using System;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CDXSTATUS : CDxMessageBase
    {
        private Job _job;
        private CdxShipment _shipment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="shipment"></param>
        public CDXSTATUS(Job job, CdxShipment shipment)
        {
            _job = job;
            _shipment = shipment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forEvent"></param>
        /// <param name="eventDateTime"></param>
        /// <returns></returns>
        public string ToString(WebHookEvent forEvent, DateTime eventDateTime)
        {
            var flatFile = new StringBuilder();

            flatFile.AppendFormat("\"{0}\",\"{1:yyyy-MM-dd hh:mm:ss}\",\"{2}\",\"{3}\",\"{4}\"{5}",
                "CDXSTATUS",
                eventDateTime, 
                _job.CdxExchangeId, 
                _job.LoadId, 
                _shipment.ExchangeId,
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
                _job.AssignedDriverRemoteId,
                _shipment.TruckId,
                _job.AssignedTruckRemoteId,
                null, // Lat
                null, // Lon
                forEvent,
                null, // EtaDateTime
                null,  // Gate Code
                Eol
                );

            foreach (var v in _job.Vehicles)
            {
                flatFile.AppendFormat("\"{0}\",\"{1}\"{2}", 
                    "VEHICLE",
                    v.Vin,
                    Eol
                    );
            }

            flatFile.Append("\"CDXEND\"");

            return flatFile.ToString();
        }
    }
}
