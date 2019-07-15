using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CDXSTOP : CDxMessageBase
    {
        private CdxShipment _shipment;
        private Job _job;
        private List<Vehicle> _shipmentVehicles;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipment"></param>
        /// <param name="job"></param>
        /// <param name="shipmentVehicles"></param>
        public CDXSTOP(CdxShipment shipment, Job job, List<Vehicle> shipmentVehicles)
        {
            if (shipment == null)
                throw new ArgumentException("CDXSTOP: Shipment cannot be null");

            if (job == null)
                throw new ArgumentException("CDXSTOP: Job cannot be null");

            if (shipmentVehicles == null)
                throw new ArgumentException("CDXSTOP: Vehicle collection cannot be null");

            _shipment = shipment;
            _job = job;
            _shipmentVehicles = shipmentVehicles;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forEvent"></param>
        /// <param name="eventDateTime"></param>
        /// <param name="fileName"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public string ToString(WebHookEvent forEvent, DateTime eventDateTime, Position position, out string fileName)
        {
            var cdxEvent = Map.WebHookEventToCdxJobStatus(forEvent);

            var stopType = cdxEvent < CdxShipmentStatus.OnWayToDeliver
                ? EndPointType.Collection
                : EndPointType.Delivery;

            var endPoint = cdxEvent < CdxShipmentStatus.OnWayToDeliver
                ? _job.Pickup
                : _job.Dropoff;

            var isFullShipment = _shipment.VehicleCount == _shipmentVehicles.Count;

            var flatFile = new StringBuilder();

            flatFile.AppendFormat("\"{0}\",\"{1:yyyy-MM-dd HH:mm:ss}\",\"{2}\",\"{3}\"{4}",
                "CDXSTOP",
                eventDateTime,
                _shipment.SenderLoadId,
                _shipment.ExchangeId,
                Eol
                );

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\"{7}",
                "SHIPMENT",
                _shipment.SenderScac,
                _shipment.ReceiverScac,
                _shipment.SenderJobNumber,
                _shipment.SenderLoadId,
                _shipment.SenderTripId,
                isFullShipment,
                Eol
                );

            var notSignedReasons = new StringBuilder();

            if (endPoint.Signoff.NotSignedReasons != null)
            {
                foreach (var reason in endPoint.Signoff.NotSignedReasons)
                {
                    notSignedReasons.AppendFormat("{0}, ", reason);
                }
            }

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\"{10}",
                "STOP",
                stopType,
                _shipmentVehicles.Count,
                endPoint.ProofDocUrl,
                endPoint.Destination.Email,
                endPoint.Signoff.NotSignedReasons == null ? 0 : 1,
                endPoint.Signoff.SignedBy,
                notSignedReasons.ToString().TrimEnd(' ').TrimEnd(','),
                endPoint.Signoff.Comment,
                endPoint.Signoff.Url,
                Eol
                );

            foreach (var v in _shipmentVehicles)
            {
                var inventoryStatus = cdxEvent;

                if (!string.IsNullOrWhiteSpace(v.NonCompletionReason))
                {
                    inventoryStatus = cdxEvent == CdxShipmentStatus.PickedUp
                        ? CdxShipmentStatus.NotPickedUp
                        : CdxShipmentStatus.NotDelivered;
                }

                flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\"{16}",
                    "VEHICLE",
                    v.Vin,
                    inventoryStatus,
                    v.NonCompletionReason,
                    v.SignedBy,
                    v.Signature,
                    v.SignoffComment,
                    _shipment.ReceiverJobNumber,
                    _shipment.ReceiverLoadId,
                    _shipment.ReceiverTripId,
                    _job.AssignedDriverName,
                    _job.AssignedDriverRemoteId,
                    _job.AssignedTruckRemoteId,
                    _job.AssignedTruckRemoteId,
                    position == null ? "" : position.Latitude.ToString(),
                    position == null ? "" : position.Longitude.ToString(),
                    Eol
                    );

                var damage = forEvent == WebHookEvent.PickupStop
                    ? v.DamageAtPickup
                    : v.DamageAtDropoff;

                if (damage != null)
                {
                    foreach (var d in damage)
                    {
                        var photos = v.Photos.Where(p => p.Url != null && p.Url.Contains(string.Format("/damage/{0}", d.Id)));
                        if (photos != null)
                        {
                            foreach (var p in photos)
                            {
                                flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"{6}",
                               "DAMAGE",
                               d.Area.Code,
                               d.Type.Code,
                               d.Severity.Code,
                               p.Url,
                               d.Description,
                               Eol
                               );
                            }
                        }
                    }
                }
            }

            flatFile.Append("\"CDXEND\"");

            fileName = string.Format(
                "CDXSTOP_{0}_{1}_{2}_{3:s}.IN",
                cdxEvent,
                _shipment.ExchangeId,
                _shipment.SenderJobNumber,
                DateTime.UtcNow);

            return flatFile.ToString();
        }
    }
}
