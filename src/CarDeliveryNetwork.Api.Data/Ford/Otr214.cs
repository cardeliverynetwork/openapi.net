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
        const string ReceiverId = "FORDIT";

        private readonly Job _job;
        private readonly Fleet _contractedCarrier;

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
        /// <param name="contractedCarrier">The carrier fleet</param>
        public Otr214(Job job, Fleet contractedCarrier)
            : this()        
        {
            _job = job;
            _contractedCarrier = contractedCarrier;
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
            var transmissionId = messageGuid;
            var senderId = _contractedCarrier.Scac + "SP";

            var vehicle = _job.Vehicles[vehicleIndex];
            var status = "R1";

            switch (forEvent)
            {
                case WebHookEvent.AssignedToDriver:
                    status = "R1";
                    break;

                case WebHookEvent.PickupDamageRecorded:
                    status = "A9";
                    break;

                case WebHookEvent.PickupStop:
                    status = "XB";
                    break;

                case WebHookEvent.OnWayDeliver:
                    status = "AF";
                    break;

                //case WebHookEvent.Eta:
                //    status = "AF";
                //    break;

                case WebHookEvent.DropoffStop:
                    status = "X1";
                    break;
            }

            var message = new CountedOtr214StringBuilder();

            message.AppendFormatNoCount(
                "ISA*00*          *00*          *ZZ*{0}*ZZ*GTNEXUS        *{1:yyMMdd}*{1:HHmm}*U*00401*{2}*0*{3}*>{4}", 
                senderId.PadRight(15), 
                deviceTime,
                transmissionId.PadLeft(9, '0'), 
                isTest ? "T" : "P", Eol);

            message.AppendFormatNoCount("GS*{0}*{1}*{2}*{3:yyyyMMdd}*{3:HHmm}*{4}*X*004010{5}", MessageType, senderId, ReceiverId, deviceTime, transmissionId, Eol);

            // Start counting lines now
            message.AppendFormat("ST*214*{0}{1}", transmissionId, Eol);  //Transaction Set Control Number 
            message.AppendFormat("B10*{0}*{0}*{1}{2}", _job.LoadId, _job.ContractedCarrierScac, Eol);

            message.AppendFormat("L11*{0}*EQ{1}", vehicle.Vin, Eol);
            message.AppendFormat("L11*{0}*VT{1}", vehicle.Vin, Eol);
            message.AppendFormat("L11*D*4C{0}", Eol);                   // D Compound - Dealer  // C - Compound - Compound
            message.AppendFormat("L11*{0}*4B{1}", _job.Pickup.Destination.QuickCode, Eol);
            message.AppendFormat("L11*{0}*GL{1}", _job.Dropoff.Destination.QuickCode, Eol);     // Destination - Dealer code
            message.AppendFormat("L11*{0}*MCI{1}", _job.AllocatedCarrierScac, Eol);

            if (forEvent == WebHookEvent.PickupDamageRecorded && vehicle.DamageAtPickup != null)
                foreach (var item in vehicle.DamageAtPickup)
                    message.AppendFormat("L11*{0}{1}{2}*BZ{3}", item.Area.Code, item.Type.Code, item.Severity.Code, Eol);

            // Carrier
            var carrierAddress = _contractedCarrier.Contact;
            message.AppendFormat("N1*CA*{0}*94*{1}{2}", _contractedCarrier.Name, _contractedCarrier.Scac, Eol);
            message.AppendFormat("N3*{0}{1}", AddressToOneLine(carrierAddress.AddressLines), Eol);
            message.AppendFormat("N4*{0}*{1}*{2}*{3}*SL*{4}{5}", carrierAddress.City, carrierAddress.StateRegion, carrierAddress.ZipPostCode, carrierAddress.CountryCode, carrierAddress.LocationCode, Eol);

            // From
            var pickupDest = _job.Pickup.Destination;
            message.AppendFormat("N1*SF*{0}-{1}*94*{0}-{2}{3}", pickupDest.QuickCode, pickupDest.OrganisationName, _contractedCarrier.Scac, Eol);
            message.AppendFormat("N3*{0}{1}", AddressToOneLine(pickupDest.AddressLines), Eol);
            message.AppendFormat("N4*{0}*{1}*{2}*{3}*SL*{4}{5}", pickupDest.City, pickupDest.StateRegion, pickupDest.ZipPostCode, pickupDest.CountryCode, pickupDest.LocationCode, Eol);
            message.AppendFormat("G62*69*{0:yyyyMMdd}{1}", _job.Pickup.ScheduledDate, Eol);

            // To
            var deliveryDest = _job.Dropoff.Destination;
            message.AppendFormat("N1*ST*{0}-{1}*94*{0}-{2}{3}", deliveryDest.QuickCode, deliveryDest.OrganisationName, _contractedCarrier.Scac, Eol);
            message.AppendFormat("N3*{0}{1}", AddressToOneLine(deliveryDest.AddressLines), Eol);
            message.AppendFormat("N4*{0}*{1}*{2}*{3}*SL*{4}{5}", deliveryDest.City, deliveryDest.StateRegion, deliveryDest.ZipPostCode, deliveryDest.CountryCode, deliveryDest.LocationCode, Eol);
            message.AppendFormat("G62*17*{0:yyyyMMdd}{1}", _job.Dropoff.ScheduledDate, Eol);

            message.AppendFormat("MS3*{0}*O**J{1}", _job.AllocatedCarrierScac, Eol);
            message.AppendFormat("LX*1{0}", Eol);
            message.AppendFormat("AT7*{0}*{1}***{2:yyyyMMdd}*{2:HHmm}*UT{3}", status, forEvent == WebHookEvent.PickupDamageRecorded ? "BG" : "NS", deviceTime, Eol);
            message.AppendFormat("MS1*{0}*SL*US{1}", pickupDest.LocationCode, Eol);     // Origin // SPLC*SL*CA//
            message.AppendFormat("SE*{0}*{1}{2}", message.LineCount + 1, transmissionId, Eol);

            // Don't count these lines
            message.AppendFormatNoCount("GE*1*{0}{1}", transmissionId, Eol);
            message.AppendFormatNoCount("IEA*1*{0}{1}", transmissionId.PadLeft(9, '0'), Eol);

            filename = string.Format("{0}_{1}_{2}_{3}_{4:yyMMdd}T{4:HHmmssff}.X12", senderId, ReceiverId, MessageType, transmissionId, now);
            return message.ToString();
        }

        // Replaces common line end characters in address lines field for comma + space.
        private static string AddressToOneLine(string address)
        {
            return address == null 
                ? "" 
                : address.Replace("\r\n", ", ").Replace("\r", ", ").Replace("\n", ", ").Trim(' ', ',');
        }
    }
}
