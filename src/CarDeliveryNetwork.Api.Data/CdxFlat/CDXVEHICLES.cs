using System;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CDXVEHICLES
    {
        private CdxVehicleExchange _vehicleExchage;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vehicleExchage"></param>
        public CDXVEHICLES(CdxVehicleExchange vehicleExchage)
        {
            _vehicleExchage = vehicleExchage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var flatFile = new StringBuilder();
            var shipment = _vehicleExchage.Shipment;

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{4}\"",
                "CDXVEHICLES",
                shipment.EventDateTime,  //TODO - put this in the right format
                shipment.SenderInventoryId,
                shipment.ExchangeId);

            var vehicles = _vehicleExchage.CdxVehicles;
            var firstVehicle = vehicles[0];

            flatFile.AppendFormat("\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\",\"{9}\",\"{10}\",\"{11}\",\"{12}\",\"{13}\",\"{14}\",\"{15}\",\"{16}\",\"{17}\",\"{18}\",\"{19}\",\"{20}\",\"{21}\",\"{22}\",\"{23}\",\"{24}\"",
                "SHIPMENT",
                shipment.SenderScac,
                shipment.ReceiverScac,
                shipment.SenderJobNumber,
                shipment.SenderLoadId,
                shipment.SenderTripId,
                shipment.Price,
                shipment.Notes,
                shipment.TruckId,
                shipment.DriverId,
                firstVehicle.Origin.QuickCode,
                firstVehicle.Origin.InternalQuickCode,
                firstVehicle.Origin.LocationCode,
                firstVehicle.ScheduledPickupDate, //TODO - put this in the right format
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

            foreach (var v in vehicles)
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
                v.ScheduledDeliveryDate, //TODO - put this in the right format
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
