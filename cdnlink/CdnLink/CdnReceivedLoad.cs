using CarDeliveryNetwork.Api.Data;

namespace CdnLink
{
    public partial class CdnReceivedLoad
    {
        public CdnReceivedLoad(Job job)
            : this()
        {
            LoadId = job.LoadId;
            CdnId = job.Id;
            AllocatedCarrierScac = job.AllocatedCarrierScac;
            AssignedDriverRemoteId = job.AssignedDriverRemoteId;
            BuyPrice = job.BuyPrice;
            CustomerReference = job.CustomerReference;
            DriverPay = job.DriverPay;
            JobInitiator = job.JobInitiator;
            Notes = job.Notes;
            SellPrice = job.SellPrice;
            ServiceRequired = (int)job.ServiceRequired;
            ShipperScac = job.ShipperScac;
            TripId = job.TripId;

            // Customer                                                       
            CustomerQuickCode = job.Customer.QuickCode;
            CustomerContact = job.Customer.Contact;
            CustomerOrganisationName = job.Customer.OrganisationName;
            CustomerAddressLines = job.Customer.AddressLines;
            CustomerCity = job.Customer.City;
            CustomerStateRegion = job.Customer.StateRegion;
            CustomerZipPostCode = job.Customer.ZipPostCode;
            CustomerPhone = job.Customer.Phone;
            CustomerMobilePhone = job.Customer.MobilePhone;
            CustomerOtherPhone = job.Customer.OtherPhone;
            CustomerFax = job.Customer.Fax;
            CustomerEmail = job.Customer.Email;
            CustomerNotes = job.Customer.Notes;

            // Pickup                                                         
            PickupRequestedDate = job.Pickup.RequestedDate;
            PickupRequestedDateIsExact = job.Pickup.RequestedDateIsExact;
            PickupQuickCode = job.Pickup.Destination.QuickCode;
            PickupContact = job.Pickup.Destination.Contact;
            PickupOrganisationName = job.Pickup.Destination.OrganisationName;
            PickupAddressLines = job.Pickup.Destination.AddressLines;
            PickupCity = job.Pickup.Destination.City;
            PickupStateRegion = job.Pickup.Destination.StateRegion;
            PickupZipPostCode = job.Pickup.Destination.ZipPostCode;
            PickupPhone = job.Pickup.Destination.Phone;
            PickupMobilePhone = job.Pickup.Destination.MobilePhone;
            PickupOtherPhone = job.Pickup.Destination.OtherPhone;
            PickupFax = job.Pickup.Destination.Fax;
            PickupEmail = job.Pickup.Destination.Email;
            PickupNotes = job.Pickup.Destination.Notes;

            // Dropoff                                                        
            DropoffRequestedDate = job.Dropoff.RequestedDate;
            DropoffRequestedDateIsExact = job.Dropoff.RequestedDateIsExact;
            DropoffQuickCode = job.Dropoff.Destination.QuickCode;
            DropoffContact = job.Dropoff.Destination.Contact;
            DropoffOrganisationName = job.Dropoff.Destination.OrganisationName;
            DropoffAddressLines = job.Dropoff.Destination.AddressLines;
            DropoffCity = job.Dropoff.Destination.City;
            DropoffStateRegion = job.Dropoff.Destination.StateRegion;
            DropoffZipPostCode = job.Dropoff.Destination.ZipPostCode;
            DropoffPhone = job.Dropoff.Destination.Phone;
            DropoffMobilePhone = job.Dropoff.Destination.MobilePhone;
            DropoffOtherPhone = job.Dropoff.Destination.OtherPhone;
            DropoffFax = job.Dropoff.Destination.Fax;
            DropoffEmail = job.Dropoff.Destination.Email;
            DropoffNotes = job.Dropoff.Destination.Notes;

            // Documents
            if (job.Documents != null)
            {
                foreach (var document in job.Documents)
                    CdnReceivedDocuments.Add(new CdnReceivedDocument
                    {
                        CdnId = job.Id,
                        Comment = document.Comment,
                        Title = document.Title,
                        Url = document.Url
                    });
            }

            // Vehicles
            if (job.Vehicles != null)
                foreach (var vehicle in job.Vehicles)
                {
                    var receivedVehicle = new CdnReceivedVehicle
                    {
                        CdnId = job.Id,
                        VehicleId = vehicle.Id,
                        Location = vehicle.Location,
                        Make = vehicle.Make,
                        Model = vehicle.Model,
                        MovementNumber = vehicle.MovementNumber,
                        Notes = vehicle.Notes,
                        Registration = vehicle.Registration,
                        Variant = vehicle.Variant,
                        Vin = vehicle.Vin
                    };

                    // Damage
                    if (vehicle.DamageAtPickup != null)
                        foreach (var damage in vehicle.DamageAtPickup)
                            receivedVehicle.CdnReceivedDamages.Add(new CdnReceivedDamage
                            {
                                VehicleId = vehicle.Id,
                                DamageId = damage.Id,
                                AreaCode = damage.Area.Code,
                                AreaDescription = damage.Area.Description,
                                DamageAt = "Pickup",
                                SeverityCode = damage.Severity.Code,
                                SeverityDescription = damage.Severity.Description,
                                TypeCode = damage.Type.Code,
                                TypeDescription = damage.Type.Description,
                            });

                    if (vehicle.DamageAtDropoff != null)
                        foreach (var damage in vehicle.DamageAtDropoff)
                            receivedVehicle.CdnReceivedDamages.Add(new CdnReceivedDamage
                            {
                                VehicleId = vehicle.Id,
                                DamageId = damage.Id,
                                AreaCode = damage.Area.Code,
                                AreaDescription = damage.Area.Description,
                                DamageAt = "Dropoff",
                                SeverityCode = damage.Severity.Code,
                                SeverityDescription = damage.Severity.Description,
                                TypeCode = damage.Type.Code,
                                TypeDescription = damage.Type.Description,
                            });

                    CdnReceivedVehicles.Add(receivedVehicle);
                }
        }
    }
}
