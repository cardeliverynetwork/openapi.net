﻿using System;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CDXVEHICLES : CDxMessageBase
    {
        private CdxVehicleExchange _vehicleExchange;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicleExchange"></param>
        public CDXVEHICLES(CdxVehicleExchange vehicleExchange)
        {
            _vehicleExchange = vehicleExchange;
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
            var shipment = _vehicleExchange.Shipment;

            flatFile.AppendFormat("\"{0}\",\"{1:yyyy-MM-dd HH:mm:ss}\",\"{2}\",\"{3}\"{4}",
                "CDXVEHICLES",
                shipment.EventDateTime,
                shipment.SenderInventoryId,
                shipment.ExchangeId,
                Eol
                );

            var vehicles = _vehicleExchange.CdxVehicles;
            var firstVehicle = vehicles[0];

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13:yyyy-MM-dd}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\",\"{18}\",\"{19}\",\"{20}\",\"{21}\",\"{22}\",\"{23}\",\"{24}\"{25}",
                "SHIPMENT",
                shipment.SenderScac,
                shipment.ReceiverScac,
                shipment.SenderJobNumber,
                shipment.SenderLoadId,
                shipment.SenderTripId,
                shipment.Price,
                shipment.Notes,
                "",  // TruckId - Obsolete in CDXVEHICLES
                "",  // DriverId - Obsolete in CDXVEHICLES
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
                firstVehicle.Origin.CountryCode,
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

            fileName = string.Format("CDXVEHICLES_{0}_{1:yyyyMMddHHmmss}.IN", _vehicleExchange.Shipment.ExchangeId, DateTime.UtcNow);

            return flatFile.ToString();
        }
    }
}
