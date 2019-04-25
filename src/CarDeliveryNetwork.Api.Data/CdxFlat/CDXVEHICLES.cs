using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CDXVEHICLES
    {
        private CdxShipment _shipment;
        private List<CdxVehicle> _vehicels;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shipment"></param>
        /// <param name="vehicles"></param>
        public CDXVEHICLES(CdxShipment shipment, List<CdxVehicle> vehicles)
        {
            if (shipment == null)
                throw new ArgumentException("Shipment cannot be null");

            if (vehicles == null)
                throw new ArgumentException("Vehile collection cannot be null");

            if (vehicles.Count == 0)
                throw new ArgumentException("Vehile collection cannot be empty");

            _shipment = shipment;
            _vehicels = vehicles;
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

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{4}\"",
                "CDXVEHICLES",
                eventDateTime, 
                _shipment.SenderInventoryId, 
                _shipment.ExchangeId);

            var firstVehicle = _vehicels[0];

            Debug.Assert(firstVehicle.Origin != null, "Must have origin");

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\",\"{18}\",\"{19}\",\"{20}\",\"{21}\",\"{22}\",\"{23}\",\"{24}\"",
                "SHIPMENT",
                _shipment.SenderScac,
                _shipment.ReceiverScac,
                _shipment.SenderJobNumber,
                _shipment.SenderLoadId,
                _shipment.SenderTripId,
                _shipment.Price,
                _shipment.Notes,
                _shipment.TruckId,
                _shipment.DriverId,
                firstVehicle.Origin.QuickCode,
                firstVehicle.Origin.InternalQuickCode,
                firstVehicle.Origin.LocationCode,
                firstVehicle.ScheduledPickupDate,
                firstVehicle.Origin.OrganizationName,
                firstVehicle.Origin.AddressLines,
                "", // Address lines 2
                firstVehicle.Origin.City,
                firstVehicle.Origin.StateRegion,
                firstVehicle.Origin.ZipPostCode,
                firstVehicle.Origin.Contact,
                firstVehicle.Origin.Email,
                firstVehicle.Origin.Phone,
                firstVehicle.Origin.Notes,
                firstVehicle.Origin.CountryCode
                );

            foreach (var v in _vehicels)
            {
                flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\",\"{18}\",\"{19}\",\"{20}\",\"{21}\",\"{22}\",\"{23}\",\"{24}\",\"{25}\",\"{26}\",\"{27}\",\"{28}\",\"{29}\"",
                "VEHICLE",
                v.ShipperScac,
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
                v.Destination.CountryCode
                );
            }

            flatFile.Append("\"CDXEND\"");

            return flatFile.ToString();
        }
    }
}
