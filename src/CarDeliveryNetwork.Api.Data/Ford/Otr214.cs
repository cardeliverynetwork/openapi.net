using System;
using System.Collections.Generic;
using System.Text;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data.Ford
{
    /// <summary>
    /// Class representing a TMW v1 Stop
    /// </summary>
    public class Otr214
    {
        private class CountedOtr214StringBuilder
        {
            private readonly StringBuilder _message = new StringBuilder();

            public int LineCount { get; private set; }

            public CountedOtr214StringBuilder AppendFormat(string format, params object[] args)
            {
                _message.AppendFormat(format, args);
                LineCount++;
                return this;
            }

            public CountedOtr214StringBuilder AppendFormatNoCount(string format, params object[] args)
            {
                _message.AppendFormat(format, args);
                return this;
            }

            public override string ToString()
            {
                return _message.ToString();
            }
        }

        const string Eol = "\r\n";
        const string MessageType = "QM";

        private readonly Job _job;

        /// <summary>
        /// Initializes a new instance of the <see cref="Otr214"/> class.
        /// </summary>
        public Otr214()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Otr214"/> class.
        /// </summary>
        /// <param name="job">The API job from which to contruct this Stop</param>
        public Otr214(Job job)
            : this()        
        {
            _job = job;
        }

        /// <summary>
        /// Returns a serial representation of the object.
        /// </summary>
        /// <param name="vehicleIndex">Index of the vehicle on this job to process</param>
        /// <param name="forEvent">The event for which to serialise this job</param>
        /// <param name="timeStamp">Time in UTC that this message was created</param>
        /// <param name="messageGuid">A unique identifier for this message</param>
        /// <param name="deviceTime">Time in UTC that the associaed message was created on the device</param>
        /// <param name="isTest">When true, indicated that this is a test message</param>
        /// <param name="filename">Returns the filename for this message</param>
        /// <returns>The serialized object.</returns>
        public string ToString(
            int vehicleIndex, 
            WebHookEvent forEvent, 
            DateTime timeStamp, 
            string messageGuid, 
            DateTime? deviceTime, 
            bool isTest,
            out string filename)
        {
            var now = DateTime.UtcNow;
            var senderId = "SENDERID";
            var receiverId = "RECEIVERID";
            var transmissionId = messageGuid;

            var vehicle = _job.Vehicles[vehicleIndex];
            var hasPickupDamage = vehicle.DamageAtPickup != null && vehicle.DamageAtPickup.Count > 0;
            var status = "R1"; // Not used but not damaging, probably ..

            switch (forEvent)
            {
                case WebHookEvent.PickupStop:
                    status = hasPickupDamage ? "XB" : "A9";
                    break;
                case WebHookEvent.OnWayDeliver:
                    status = "AF";
                    break;
                case WebHookEvent.DropoffStop:
                    status = "X1";
                    break;
            }

            var message = new CountedOtr214StringBuilder();

            message.AppendFormatNoCount("ISA*00*          *00*          *ZZ*{0}       *ZZ*GTNEXUS        *{1:yyMMdd}*{1:HHmm}*U*00401*000000263*0*{2}*>{3}", "SENDERID", deviceTime, isTest ? "T" : "P", Eol);
            message.AppendFormatNoCount("GS*{0}*{1}*FORDIT*{2:yyyyMMdd}*{2:HHmm}*263*X*00401{3}", MessageType, senderId, deviceTime, Eol);

            // Start counting lines now
            message.AppendFormat("ST*214*{0}{1}", messageGuid, Eol);
            message.AppendFormat("B10*{0}*{0}*{1}{2}", _job.LoadId, _job.ContractedCarrierScac, Eol);

            message.AppendFormat("L11*{0}*EQ{1}", _job.AssignedTruckRemoteId, Eol);
            message.AppendFormat("L11*{0}*VT{1}", vehicle.Vin, Eol);
            message.AppendFormat("L11*D*4C{0}", Eol);
            message.AppendFormat("L11*{0}*4B{1}", _job.Pickup.Destination.QuickCode, Eol);
            message.AppendFormat("L11*{0}*GL{1}", _job.Dropoff.Destination.QuickCode, Eol);
            message.AppendFormat("L11*{0}*MCI{1}", _job.AllocatedCarrierScac, Eol);

            if (forEvent == WebHookEvent.PickupStop && hasPickupDamage)
            {
                message.AppendFormat("L11*{0}*BZ{1}", DamageToString(vehicle.DamageAtDropoff), Eol);   
            }

            // Carrier
            message.AppendFormat("N1*CA*{0}*94*{1}{2}", _job.AllocatedCarrierScac, _job.AllocatedCarrierScac, Eol); // TODO - {0} is carrier name ?
            message.AppendFormat("N3*Carrier Street{0}", Eol);
            message.AppendFormat("N4*Carrier City*State*Zip*US*SL*Carrier City Id{0}", Eol);

            // From
            var pickupDest = _job.Pickup.Destination;
            message.AppendFormat("N1*SF*{0}-{0}*94*{0}-{0}{1}", pickupDest.QuickCode, Eol);
            message.AppendFormat("N3*{0}{1}", pickupDest.AddressLines, Eol);
            message.AppendFormat("N4*{0}*{1}*{2}*US*SL*{3}", pickupDest.City, pickupDest.StateRegion, pickupDest.ZipPostCode, Eol);
            message.AppendFormat("G62*69*{0:yyyyMMdd}{1}", _job.Pickup.ScheduledDate, Eol);

            // To
            var deliveryDest = _job.Dropoff.Destination;
            message.AppendFormat("N1*SF*{0}-{0}*94*{0}-{0}{1}", deliveryDest.QuickCode, Eol);
            message.AppendFormat("N3*{0}{1}", deliveryDest.AddressLines, Eol);
            message.AppendFormat("N4*{0}*{1}*{2}*US*SL*{3}", deliveryDest.City, deliveryDest.StateRegion, deliveryDest.ZipPostCode, Eol);
            message.AppendFormat("G62*17*{0:yyyyMMdd}{1}", _job.Dropoff.ScheduledDate, Eol);

            message.AppendFormat("MS3*{0}*M**J{1}", _job.AllocatedCarrierScac, Eol);
            message.AppendFormat("LX*{0}{1}", messageGuid, Eol);
            message.AppendFormat("AT7*{0}*NS***{1:yyyyMMdd}*{1:HHmm}*UT{2}", status, deviceTime, Eol);
            message.AppendFormat("MS1*{0}*SL*US{1}", "City SPLC code", Eol);
            message.AppendFormat("K1*Empty{0}", Eol);
            message.AppendFormat("SE*{0}*{1}{2}", message.LineCount + 1, transmissionId, Eol);

            // Don't count these lines
            message.AppendFormatNoCount("GE*1*263{0}", Eol);
            message.AppendFormatNoCount("IEA*1*000000263{0}", Eol);

            // TODO - check the time code.  Add milliseconds
            filename = string.Format("{0}_{1}_{2}_{3}_{4:yyMMdd}T{4:HHmmssff}.X12", senderId, receiverId, MessageType, transmissionId, now);
            return message.ToString();
        }

        private static string DamageToString(ICollection<DamageItem> damage)
        {
            if (damage == null || damage.Count == 0)
                return "";

            var damageAsString = new StringBuilder();
            foreach (var item in damage)
                damageAsString.AppendFormat("{0}{1}{2};", item.Area.Code, item.Type.Code, item.Severity.Code);
            return damageAsString.ToString().Trim(';');
        }
    }
}
