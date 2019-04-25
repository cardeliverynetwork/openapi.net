using CarDeliveryNetwork.Types;
using System;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CDXSTOP
    {
        private Job _job;
        private CdxShipment _shipment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="shipment"></param>
        public CDXSTOP(Job job, CdxShipment shipment)
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

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\"",
                "CDXSTOP",
                eventDateTime,
                _job.CdxExchangeId,
                _job.LoadId,
                _shipment.ExchangeId
                );

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\"",
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
                null  // Gate Code
                );

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\"",
                "STOP",
                forEvent, 
                "", 
                "", 
                "", 
                "", 
                "", 
                "", 
                ""
                );

            foreach (var v in _job.Vehicles)
            {
                flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"",
                    "VEHICLE",
                    v.Vin, 
                    v.Status, 
                    v.NonCompletionReason, 
                    v.SignedBy, 
                    v.Signature, 
                    v.SignoffComment
                    );

                var damage = forEvent == WebHookEvent.PickupStop
                    ? v.DamageAtPickup
                    : v.DamageAtDropoff;

                foreach (var d in damage)
                {
                    flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"",
                        "DAMAGE",
                        d.Area.Code, 
                        d.Type.Code, 
                        d.Severity.Code, 
                        "", 
                        d.Description);
                }
            }

            flatFile.Append("\"CDXEND\"");

            return flatFile.ToString();
        }
    }
}
