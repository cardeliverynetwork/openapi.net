using System;
using System.Collections.Generic;
using System.Text;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data.TmwV1
{
    /// <summary>
    /// Class representing a TMW v1 Stop
    /// </summary>
    public class Stop
    {
        private readonly Job _job;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stop"/> class.
        /// </summary>
        public Stop() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Stop"/> class.
        /// </summary>
        /// <param name="job">The API job from which to contruct this Stop</param>
        public Stop(Job job)
        {
            _job = job;
        }

        /// <summary>
        /// Returns a serial representation of the object.
        /// </summary>
        /// <param name="forEvent">The event for which to serialise this job</param>
        /// <param name="timeStamp">Time in UTC that this message was created</param>
        /// <param name="messageGuid">A unique identifier for this message</param>
        /// <returns>The serialized object.</returns>
        public string ToString(WebHookEvent forEvent, DateTime timeStamp, string messageGuid)
        {
            const string lineEndChar = "\r\n";
            
            var message = new StringBuilder();

            message.AppendFormat("ID=SN:{0}{1}", messageGuid, lineEndChar);
            message.AppendFormat("Subject=*Stop Info*{0}", lineEndChar);
            message.AppendFormat("Preview=*Stop Info*{0}", lineEndChar);
            message.AppendFormat("FormID=1-R{0}", lineEndChar);
            message.AppendFormat("DataType=Form{0}", lineEndChar);
            message.AppendFormat("FromName={0}{1}", _job.AssignedAppId, lineEndChar);
            message.AppendFormat("CreateTime={0:yyyy-MM-dd HH:mm:ss}{1}", timeStamp, lineEndChar);
            message.AppendFormat("CreateTimeTZ=0{0}", lineEndChar);
            message.AppendFormat("ReplyMsgID=SN:0{0}", lineEndChar);
            message.AppendFormat("Priority=0{0}", lineEndChar);
            message.AppendFormat("MessageData:{0}", lineEndChar);

            message.AppendFormat("{0}{1}", _job.AllocatedCarrierScac, lineEndChar);
            message.AppendFormat("{0}{1}", _job.AssignedDriverRemoteId, lineEndChar);
            message.AppendFormat("{0}{1}", _job.AssignedTruckRemoteId, lineEndChar);
            message.AppendFormat("{0}{1}", _job.ContractedCarrierScac, lineEndChar);
            message.AppendFormat("{0}{1}", _job.LoadId, lineEndChar);
            message.AppendFormat("{0}{1}", _job.ShipperScac, lineEndChar);
            message.AppendFormat("{0}{1}", _job.TripId, lineEndChar);

            var endPoint = forEvent == WebHookEvent.PickupStop
                ? _job.Pickup
                : _job.Dropoff;

            message.AppendFormat("{0}{1}", endPoint.Destination.QuickCode, lineEndChar);
            message.AppendFormat("{0}{1}", _job.Id, lineEndChar);
            message.AppendFormat("{0}{1}", _job.JobNumber, lineEndChar);
            message.AppendFormat("{0}{1}", endPoint.ProofDocUrl, lineEndChar);
            message.AppendFormat("{0:yyyy-MM-dd HH:mm:ss}{1}", endPoint.Signoff.Time, lineEndChar);

            var status = forEvent == WebHookEvent.PickupStop
                ? "PickedUp"
                : "Complete";

            message.AppendFormat("{0}{1}", status, lineEndChar);
            message.AppendFormat("{0}{1}", endPoint.Destination.Email, lineEndChar);
            message.AppendFormat("{0}{1}", ListToString(endPoint.Signoff.NotSignedReasons), lineEndChar);
            message.AppendFormat("{0}{1}", endPoint.Signoff.SignedBy, lineEndChar);

            foreach (var v in _job.Vehicles)
            {
                message.AppendFormat("{0}{1}", v.Vin, lineEndChar);
                message.AppendFormat("{0}{1}", v.MovementNumber, lineEndChar);
                message.AppendFormat("{0}{1}", v.Status, lineEndChar);
                message.AppendFormat("{0}{1}", v.NonCompletionReason, lineEndChar);

                var damage = forEvent == WebHookEvent.PickupStop
                    ? v.DamageAtPickup
                    : v.DamageAtDropoff;

                message.AppendFormat("{0}{1}", DamageToString(damage), lineEndChar);
            }
            
            return message.ToString();
        }

        private static string ListToString(ICollection<string> list)
        {
            if (list == null || list.Count == 0)
                return "";

            var listAsString = new StringBuilder();
            foreach (var item in list)
                listAsString.AppendFormat("{0}, ", item);
            return listAsString.ToString().Trim(' ', ',');
        }

        private static string DamageToString(ICollection<DamageItem> damage)
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
