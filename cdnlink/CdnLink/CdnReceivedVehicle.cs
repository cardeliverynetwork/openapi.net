using CarDeliveryNetwork.Api.Data;

namespace CdnLink
{
    public partial class CdnReceivedVehicle
    {
        public CdnReceivedVehicle(Vehicle vehicle)
            : this()
        {
            VehicleId = vehicle.Id;
            Location = vehicle.Location;
            Make = vehicle.Make;
            Model = vehicle.Model;
            MovementNumber = vehicle.MovementNumber;
            Notes = vehicle.Notes;
            Registration = vehicle.Registration;
            Variant = vehicle.Variant;
            Vin = vehicle.Vin;

            if (vehicle.DamageAtPickup != null)
                foreach (var damage in vehicle.DamageAtPickup)
                    CdnReceivedDamages.Add(new CdnReceivedDamage(damage, "Pickup") { VehicleId = vehicle.Id });

            if (vehicle.DamageAtDropoff != null)
                foreach (var damage in vehicle.DamageAtDropoff)
                    CdnReceivedDamages.Add(new CdnReceivedDamage(damage, "Dropoff") { VehicleId = vehicle.Id });
        }
    }
}
