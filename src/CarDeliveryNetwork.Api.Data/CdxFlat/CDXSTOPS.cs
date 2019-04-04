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

            flatFile.AppendFormat("CDXSTOP,{0},{1},{2},{3}", eventDateTime, _job.CdxExchangeId, _job.LoadId, _shipment.ExchangeId);
            flatFile.AppendFormat("SHIPMENT,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}",
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

            flatFile.AppendFormat("STOP,{0},{1},{2},{3},{4},{5},{6},{7}",
                forEvent, 
                "", 
                "", 
                "", 
                "", 
                "", 
                "", 
                "");

            foreach (var v in _job.Vehicles)
            {
                flatFile.AppendFormat("VEHICLE,{0},{1},{2},{3},{4},{5}", v.Vin, v.Status, v.NonCompletionReason, v.SignedBy, v.Signature, v.SignoffComment);

                var damage = forEvent == WebHookEvent.PickupStop
                    ? v.DamageAtPickup
                    : v.DamageAtDropoff;

                foreach (var d in damage)
                {
                    flatFile.AppendFormat("DAMAGE,{0}{1}{2},{3},\"{4}\"", d.Area.Code, d.Type.Code, d.Severity.Code, "", d.Description);
                }
            }

            flatFile.Append("CDXEND");

            return flatFile.ToString();
        }
    }
}
