using CarDeliveryNetwork.Types;
using System;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CdxStop
    {
        private Job _job;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        public CdxStop(Job job)
        {
            _job = job;
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

            flatFile.AppendFormat("CDXSTOP,{0},{1},{2},{3}", eventDateTime, _job.CdxExchangeId, _job.LoadId, "ReceiverExchangeId");
            flatFile.AppendFormat("SHIPMENT,{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16}",
                "SenderSCAC",
                "ReceiverSCAC",
                "SenderJobNumber",
                "SenderLoadId",
                "SenderTripId",
                "ReceiverJobNumber",
                "ReceiverLoadId",
                "ReceiverTripId",
                "DriverName",
                _job.AssignedDriverRemoteId,
                "Truck",
                _job.AssignedTruckRemoteId,
                "Lat",
                "Lon",
                forEvent,
                "EtaDateTime",
                "GateCode");

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
