using CarDeliveryNetwork.Types;
using System;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CdxStatus
    {
        private Job _job;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        public CdxStatus(Job job)
        {
            _job = job;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventDateTime"></param>
        /// <param name="forEvent"></param>
        /// <returns></returns>
        public string ToString(DateTime eventDateTime, WebHookEvent forEvent)
        {
            var flatFile = new StringBuilder();

            flatFile.AppendFormat("CDXSTATUS,{0},{1},{2},{3}", eventDateTime, _job.CdxExchangeId, _job.LoadId, "ReceiverExchangeId");
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

            foreach (var v in _job.Vehicles)
            {
                flatFile.AppendFormat("VEHICLE,{0}", v.Vin);
            }

            flatFile.Append("CDXEND");

            return flatFile.ToString();
        }
    }
}
