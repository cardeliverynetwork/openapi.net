using System;
using System.Collections.Generic;
using System.Text;
using CarDeliveryNetwork.Types;
using ApiData = CarDeliveryNetwork.Api.Data;

namespace CarDeliveryNetwork.Api.Data.TmwV1
{
    /// <summary>
    /// Class representing a TMW v1 Stop
    /// </summary>
    public class Stop
    {
        ApiData.Job _job;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stop"/> class.
        /// </summary>
        public Stop() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stop"/> class.
        /// </summary>
        /// <param name="job">The API job from which to contruct this Stop</param>
        public Stop(ApiData.Job job)
        {
            _job = job;
        }

        /// <summary>
        /// Returns a serial representation of the object.
        /// </summary>
        /// <param name="forEvent">The event for which to serialise this job</param>
        /// <param name="timeStamp">Time in UTC that this message was created</param>
        /// <returns>The serialized object.</returns>
        public string ToString(WebHookEvent forEvent, DateTime timeStamp, string messageGuid)
        {
            var lineEndChar = "\r\n";
            
            var stop = new StringBuilder();

            stop.AppendFormat("ID=SN:{0}{1}", messageGuid, lineEndChar);
            stop.AppendFormat("Subject=*Stop Info*{0}", lineEndChar);
            stop.AppendFormat("Preview=*Stop Info*{0}", lineEndChar);
            stop.AppendFormat("FormID=1-R{0}", lineEndChar);
            stop.AppendFormat("DataType=Form{0}", lineEndChar);
            stop.AppendFormat("FromName={0}{1}", _job.AssignedAppId, lineEndChar);
            stop.AppendFormat("CreateTime={0:yyyy-MM-dd hh:mm:ss}{1}", timeStamp, lineEndChar);
            stop.AppendFormat("CreateTimeTZ=0{0}", lineEndChar);
            stop.AppendFormat("ReplyMsgID=SN:0{0}", lineEndChar);
            stop.AppendFormat("Priority=0{0}", lineEndChar);
            stop.AppendFormat("MessageData:{0}", lineEndChar);

            stop.AppendFormat("{0}{1}", _job.AllocatedCarrierScac, lineEndChar);
            stop.AppendFormat("{0}{1}", _job.AssignedDriverRemoteId, lineEndChar);
            stop.AppendFormat("{0}{1}", _job.AssignedTruckRemoteId, lineEndChar);
            stop.AppendFormat("{0}{1}", _job.ContractedCarrierScac, lineEndChar);
            stop.AppendFormat("{0}{1}", _job.LoadId, lineEndChar);
            stop.AppendFormat("{0}{1}", _job.ShipperScac, lineEndChar);
            stop.AppendFormat("{0}{1}", _job.TripId, lineEndChar);

            var endPoint = forEvent == WebHookEvent.PickupStop
                ? _job.Pickup
                : _job.Dropoff;

            stop.AppendFormat("{0}{1}", endPoint.Destination.QuickCode, lineEndChar);
            stop.AppendFormat("{0}{1}", _job.Id, lineEndChar);
            stop.AppendFormat("{0}{1}", _job.JobNumber, lineEndChar);
            stop.AppendFormat("{0}{1}", endPoint.ProofDocUrl, lineEndChar);
            stop.AppendFormat("{0:yyyy-MM-dd hh:mm:ss}{1}", endPoint.Signoff.Time, lineEndChar);

            var status = forEvent == WebHookEvent.PickupStop
                ? "PickedUp"
                : "Complete";

            stop.AppendFormat("{0}{1}", status, lineEndChar);
            stop.AppendFormat("{0}{1}", endPoint.Destination.Email, lineEndChar);
            stop.AppendFormat("{0}{1}", ListToString(endPoint.Signoff.NotSignedReasons), lineEndChar);
            stop.AppendFormat("{0}{1}", endPoint.Signoff.SignedBy, lineEndChar);

            foreach (var v in _job.Vehicles)
            {
                stop.AppendFormat("{0}{1}", v.Vin, lineEndChar);
                stop.AppendFormat("{0}{1}", v.MovementNumber, lineEndChar);
                stop.AppendFormat("{0}{1}", v.Status.ToString(), lineEndChar);
                stop.AppendFormat("{0}{1}", v.NonCompletionReason, lineEndChar);

                var damage = forEvent == WebHookEvent.PickupStop
                    ? v.DamageAtPickup
                    : v.DamageAtDropoff;

                stop.AppendFormat("{0}{1}", DamageToString(damage), lineEndChar);
            }
            
            return stop.ToString();
        }

        private string ListToString(List<string> list)
        {
            if (list == null || list.Count == 0)
                return "";

            var listAsString = new StringBuilder();
            foreach (var item in list)
                listAsString.AppendFormat("{0}, ", item);
            return listAsString.ToString().Trim(' ', ',');
        }

        private string DamageToString(List<DamageItem> damage)
        {
            if (damage == null || damage.Count == 0)
                return "";

            var damageAsString = new StringBuilder();
            foreach (var item in damage)
                damageAsString.AppendFormat("{0}-{1}-{2}, ", item.Area.Code, item.Type.Code, item.Severity.Code);
            return damageAsString.ToString().Trim(' ', ',');
        }
    }
}
