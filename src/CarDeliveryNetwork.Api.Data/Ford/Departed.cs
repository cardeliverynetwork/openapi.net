using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data.Ford
{
    /// <summary>
    /// Class representing a TMW v1 Stop
    /// </summary>
    public class Departed
    {
        private readonly Job _job;
        private readonly StringBuilder _message;
        private int _lineCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="Departed"/> class.
        /// </summary>
        public Departed()
        {
            _message = new StringBuilder();
            _lineCount = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Departed"/> class.
        /// </summary>
        /// <param name="job">The API job from which to contruct this Stop</param>
        public Departed(Job job)
            : this()        
        {
            _job = job;
        }

        /// <summary>
        /// Returns a serial representation of the object.
        /// </summary>
        /// <param name="forEvent">The event for which to serialise this job</param>
        /// <param name="timeStamp">Time in UTC that this message was created</param>
        /// <param name="messageGuid">A unique identifier for this message</param>
        /// <param name="deviceTime">Time in UTC that the associaed message was created on the device</param>
        /// <param name="isTest">When true, indicated that this is a test message</param>
        /// <returns>The serialized object.</returns>
        public string ToString(WebHookEvent forEvent, DateTime timeStamp, string messageGuid, DateTime? deviceTime, bool isTest = false)
        {
            const string lineEndChar = "\r\n";
            
            var vehicle = _job.Vehicles[0];
            var hasPickupDamage = vehicle.DamageAtPickup != null && vehicle.DamageAtPickup.Count > 0;
            var status = "R1"; // Not used but not damaging, probably ..

            switch (forEvent)
            {
                case WebHookEvent.PickupStop:
                    status = hasPickupDamage ? "XB" : "A9";
                    break;
                case WebHookEvent.OnWayPickup:
                    status = "AF";
                    break;
                case WebHookEvent.DropoffStop:
                    status = "X1";
                    break;
            }

            _message.AppendFormat("ISA*00*          *00*          *ZZ*{0}       *ZZ*GTNEXUS        *{1:YYMMdd}*{1:HHmm}*U*00401*000000263*0*{2}*>{3}", "SENDERID", deviceTime, isTest ? "T" : "P", lineEndChar);
            _message.AppendFormat("GS*QM*{0}*FORDIT*{1:YYYYMMdd}*{1:HHmm}*263*X*00401{2}", "SENDERID", deviceTime, lineEndChar);

            // Start counting lines now
            MessageAppendFmtCounted("ST*214*000000001{0}", lineEndChar); // TODO - 000000001 is a Transaction Set Control Number??
            MessageAppendFmtCounted("B10*ZZMC60001*STDSHBL60001*ZZMC{0}", lineEndChar);

            MessageAppendFmtCounted("L11*{0}*EQ{1}", _job.AssignedTruckRemoteId, lineEndChar);
            MessageAppendFmtCounted("L11*{0}*VT{1}", vehicle.Vin, lineEndChar);
            MessageAppendFmtCounted("L11*D*4C{0}", lineEndChar);
            MessageAppendFmtCounted("L11*{0}*4B{1}", _job.Pickup.Destination.QuickCode, lineEndChar);
            MessageAppendFmtCounted("L11*{0}*GL{1}", _job.Dropoff.Destination.QuickCode, lineEndChar);
            MessageAppendFmtCounted("L11*GSDBCODE*MCI{0}", lineEndChar);

            if (forEvent == WebHookEvent.PickupStop && hasPickupDamage)
            {
                MessageAppendFmtCounted("L11*{0}*BZ{1}", DamageToString(vehicle.DamageAtDropoff), lineEndChar);   
            }

            // Carrier
            MessageAppendFmtCounted("N1*CA*{0}*94*{1}{2}", _job.AllocatedCarrierScac, _job.AllocatedCarrierScac, lineEndChar); // TODO - {0} is carrier name ?
            MessageAppendFmtCounted("N3*100 Automobile Street{0}", lineEndChar);
            MessageAppendFmtCounted("N4* Louisville*KY * 40201 * US * SL * 286545000{0}", lineEndChar);

            // From
            var pickupDest = _job.Pickup.Destination;
            MessageAppendFmtCounted("N1*SF*{0}-{0}*94*{0}-{0}{1}", pickupDest.QuickCode, lineEndChar);
            MessageAppendFmtCounted("N3*{0}{1}", pickupDest.AddressLines, lineEndChar);
            MessageAppendFmtCounted("N4*{0}*{1}*{2}*US*SL*{0}", pickupDest.City, pickupDest.StateRegion, pickupDest.ZipPostCode, lineEndChar);
            MessageAppendFmtCounted("G62*69*{0:YYYYMMdd}{1}", _job.Pickup.ScheduledDate, lineEndChar);

            // To
            var deliveryDest = _job.Pickup.Destination;
            MessageAppendFmtCounted("N1*SF*{0}-{0}*94*{0}-{0}{1}", deliveryDest.QuickCode, lineEndChar);
            MessageAppendFmtCounted("N3*{0}{1}", deliveryDest.AddressLines, lineEndChar);
            MessageAppendFmtCounted("N4*{0}*{1}*{2}*US*SL*{0}", deliveryDest.City, deliveryDest.StateRegion, deliveryDest.ZipPostCode, lineEndChar);
            MessageAppendFmtCounted("G62*17*{0:YYYYMMdd}{1}", _job.Dropoff.ScheduledDate, lineEndChar);

            MessageAppendFmtCounted("MS3*{0}*M**J{1}", _job.AllocatedCarrierScac, lineEndChar);
            MessageAppendFmtCounted("LX*{0}{1}", "Incrementing number", lineEndChar); // TODO - Add an incrementing number
            MessageAppendFmtCounted("AT7*{0}*NS***{1:YYYYMMdd}*{1:HHmm}*UT{2}", status, deviceTime, lineEndChar);
            MessageAppendFmtCounted("MS1*{0}*SL*US{1}", "City SPLC code???", lineEndChar); // TODO - What city code?
            MessageAppendFmtCounted("K1*Empty{0}", lineEndChar);
            MessageAppendFmtCounted("SE*{0}*000000001{1}", _lineCount + 1, lineEndChar);  // TODO - 000000001 is a Transaction Set Control Number??

            // Don't count these lines
            _message.AppendFormat("GE*1*263{0}", lineEndChar);
            _message.AppendFormat("IEA*1*000000263{0}", lineEndChar);

            return _message.ToString();
        }

        private void MessageAppendFmtCounted(string format, params object[] args)
        {
            _message.AppendFormat(format, args);
            _lineCount++;
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
                damageAsString.AppendFormat("{0}{1}{2};", item.Area.Code, item.Type.Code, item.Severity.Code);
            return damageAsString.ToString().Trim(';');
        }
    }
}
