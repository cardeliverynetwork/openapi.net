using CarDeliveryNetwork.Api.Data;

namespace CdnLink
{
    public partial class CdnReceivedVehicle
    {
        public CdnReceivedVehicle(Vehicle vehicle)
            : this()
        {
            CdnVehicleId = vehicle.Id;
            Location = vehicle.Location;
            Make = vehicle.Make;
            Model = vehicle.Model;
            MovementNumber = vehicle.MovementNumber;
            NonCompletionReason = vehicle.NonCompletionReason;
            Notes = vehicle.Notes;
            Registration = vehicle.Registration;
            Status = (int)vehicle.Status;
            Variant = vehicle.Variant;
            Vin = vehicle.Vin;

            if (vehicle.DamageAtPickup != null)
                foreach (var damage in vehicle.DamageAtPickup)
                    CdnReceivedDamages.Add(new CdnReceivedDamage(damage, "Pickup"));

            if (vehicle.DamageAtDropoff != null)
                foreach (var damage in vehicle.DamageAtDropoff)
                    CdnReceivedDamages.Add(new CdnReceivedDamage(damage, "Dropoff"));
        }
    }
}
