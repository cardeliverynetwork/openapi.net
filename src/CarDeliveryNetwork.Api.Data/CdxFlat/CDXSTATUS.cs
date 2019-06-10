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
        private CdxShipment _shipment;
        private Job _job;
        private List<Vehicle> _shipmentVehicles;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipment"></param>
        /// <param name="job"></param>
        /// <param name="shipmentVehicles"></param>
        public CDXSTATUS(CdxShipment shipment, Job job, List<Vehicle> shipmentVehicles)
        {
            if (shipment == null)
                throw new ArgumentException("CDXSTATUS: Shipment cannot be null");

            if (job == null)
                throw new ArgumentException("CDXSTATUS: Job cannot be null");

            if (shipmentVehicles == null)
                throw new ArgumentException("CDXSTATUS: Vehicle collection cannot be null");

            _shipment = shipment;
            _job = job;
            _shipmentVehicles = shipmentVehicles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forEvent"></param>
        /// <param name="eventDateTime"></param>
        /// <param name="fileName"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public string ToString(WebHookEvent forEvent, DateTime eventDateTime, Position position, out string fileName)
        {
            var cdxEvent = Map.WebHookEventToCdxJobStatus(forEvent);
            var endPoint = cdxEvent < CdxShipmentStatus.OnWayToDeliver
                ? _job.Pickup
                : _job.Dropoff;

            var flatFile = new StringBuilder();

            flatFile.AppendFormat("\"{0}\",\"{1:yyyy-MM-dd HH:mm:ss}\",\"{2}\",\"{3}\"{4}",
                "CDXSTATUS",
                eventDateTime,
                _shipment.ExchangeId,
                _shipment.SenderLoadId,
                Eol
                );

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16:yyyy-MM-dd HH:mm}\",\"{17}\"{18}",
                "SHIPMENT",
                _shipment.SenderScac,
                _shipment.ReceiverScac,
                _shipment.SenderJobNumber,
                _shipment.SenderLoadId,
                _shipment.SenderTripId,
                _shipment.ReceiverJobNumber,
                _shipment.ReceiverLoadId,
                _shipment.ReceiverTripId,
                _job.AssignedDriverName,
                _job.AssignedDriverRemoteId,
                _job.AssignedTruckRemoteId,
                _job.AssignedTruckRemoteId,
                position == null ? "" : position.Latitude.ToString(),
                position == null ? "" : position.Longitude.ToString(),
                cdxEvent,
                endPoint.Eta,
                endPoint.GateOutCode,  // Gate Code
                Eol
                );

            foreach (var v in _shipmentVehicles)
            {
                flatFile.AppendFormat("\"{0}\",\"{1}\"{2}", 
                    "VEHICLE",
                    v.Vin,
                    Eol
                    );
            }

            flatFile.Append("\"CDXEND\"");

            fileName = string.Format(
                "CDXSTATUS_{0}_{1}_{2}_{3:s}.IN",
                cdxEvent, 
                _shipment.ExchangeId, 
                _shipment.SenderJobNumber, 
                DateTime.UtcNow);

            return flatFile.ToString();
        }
    }
}
