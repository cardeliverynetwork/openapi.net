using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CDXSTOP : CDxMessageBase
    {
        private List<Vehicle> _vehicles;
        private CdxShipment _shipment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipment"></param>
        /// <param name="vehicles"></param>
        public CDXSTOP(CdxShipment shipment, List<Vehicle> vehicles)
        {
            _vehicles = vehicles;
            _shipment = shipment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forEvent"></param>
        /// <param name="eventDateTime"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string ToString(WebHookEvent forEvent, DateTime eventDateTime, out string fileName)
        {
            var flatFile = new StringBuilder();

            flatFile.AppendFormat("\"{0}\",\"{1:yyyy-MM-dd hh:mm:ss}\",\"{2}\",\"{3}\"{4}",
                "CDXSTOP",
                eventDateTime,
<<<<<<< Updated upstream
                _job.LoadId,
=======
                _shipment.ExchangeId,
                _shipment.SenderLoadId,
>>>>>>> Stashed changes
                Eol
                );

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\"{15}",
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
                _shipment.DriverId,
                _shipment.TruckId,
                _shipment.TruckId,
                null, // Lat
                null, // Lon
                Eol
                );

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\"{9}",
                "STOP",
                forEvent, 
                "", 
                "", 
                "", 
                "", 
                "", 
                "", 
                "",
                Eol
                );

            foreach (var v in _vehicles)
            {
                flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"{7}",
                    "VEHICLE",
                    v.Vin, 
                    v.Status, 
                    v.NonCompletionReason, 
                    v.SignedBy, 
                    v.Signature, 
                    v.SignoffComment,
                    Eol
                    );

                var damage = forEvent == WebHookEvent.PickupStop
                    ? v.DamageAtPickup
                    : v.DamageAtDropoff;

                foreach (var d in damage)
                {
                    flatFile.AppendFormat("\"{0}\",\"{1}{2}{3}\",\"{4}\",\"{5}\"{6}",
                        "DAMAGE",
                        d.Area.Code, 
                        d.Type.Code, 
                        d.Severity.Code, 
                        "",  // TODO - Photo URL
                        d.Description,
                        Eol
                        );
                }
            }

            flatFile.Append("\"CDXEND\"");

            fileName = string.Format("CDXSTOP_{0}_{1}_{2}_{3:s}.IN", forEvent, _shipment.ExchangeId, _shipment.SenderJobNumber, DateTime.UtcNow);

            return flatFile.ToString();
        }
    }
}
