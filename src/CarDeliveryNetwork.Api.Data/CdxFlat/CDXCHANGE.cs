using System;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    public class CDXCHANGE : CDxMessageBase
    {
        private CdxChange _cdxChange;

        public CDXCHANGE(CdxChange cdxChange)
        {
            _cdxChange = cdxChange;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useRealShipper"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string ToString(bool useRealShipper, out string fileName)
        {
            var flatFile = new StringBuilder();

            fileName = string.Format("CDXCHANGE_{0}_{1:yyyyMMddHHmmss}.IN", _cdxChange.Shipment.ExchangeId, DateTime.UtcNow);

            flatFile.AppendFormat("\"{0}\",\"{1:yyyy-MM-dd HH:mm:ss}\",\"{2}\",\"{3}\"{4}",
                "CDXCHNAGE",
                _cdxChange.Shipment.EventDateTime,
                _cdxChange.Shipment.SenderInventoryId,
                _cdxChange.Shipment.ExchangeId,
                Eol
                );

            var vehicles = _cdxChange.CdxVehicles;

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\"{6}",
                "SHIPMENT",
                _cdxChange.Shipment.SenderScac,
                _cdxChange.Shipment.ReceiverScac,
                _cdxChange.Shipment.SenderJobNumber,
                _cdxChange.Shipment.SenderLoadId,
                _cdxChange.Shipment.SenderTripId,
                Eol
                );

            foreach (var v in vehicles)
            {
                flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\",\"{18:yyyy-MM-dd}\",\"{19}\",\"{20}\",\"{21}\",\"{22}\",\"{23}\",\"{24}\",\"{25}\",\"{26}\",\"{27}\",\"{28}\",\"{29}\"{30}",
                    "VEHICLE",
                    useRealShipper ? v.ShipperScac : "CDX",
                    v.Vin,
                    v.Make,
                    v.Model,
                    v.Registration, // Year
                    v.Variant,
                    v.Color,
                    v.VehicleType,
                    v.Weight,
                    v.Location,
                    v.MovementNumber,
                    v.ReferenceNumber,
                    v.Price,
                    v.Notes,
                    v.Destination.QuickCode,
                    v.Destination.InternalQuickCode,
                    v.Destination.LocationCode,
                    v.ScheduledDeliveryDate,
                    v.Destination.OrganizationName,
                    v.Destination.AddressLines,
                    "", // Address lines 2
                    v.Destination.City,
                    v.Destination.StateRegion,
                    v.Destination.ZipPostCode,
                    v.Destination.Contact,
                    v.Destination.Email,
                    v.Destination.Phone,
                    v.Destination.Notes,
                    v.Destination.CountryCode,
                    Eol
                    );
            }

            flatFile.Append("\"CDXEND\"");

            return flatFile.ToString();
        }
    }
}
