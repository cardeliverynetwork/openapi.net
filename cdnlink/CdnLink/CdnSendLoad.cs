using CarDeliveryNetwork.Api.Data;
using CarDeliveryNetwork.Types;

namespace CdnLink
{
    public partial class CdnSendLoad
    {
        public Job ToCdnJob()
        {
            var job = new Job();

            // Job details
            job.LoadId = LoadId;
            job.AllocatedCarrierScac = AllocatedCarrierScac;
            job.AssignedDriverRemoteId = AssignedDriverRemoteId;
            job.BuyPrice = BuyPrice ?? 0;
            job.CustomerReference = CustomerReference;
            job.DriverPay = DriverPay ?? 0;
            job.JobInitiator = JobInitiator;
            job.Notes = Notes;
            job.SellPrice = SellPrice ?? 0;
            job.ServiceRequired = (ServiceType)ServiceRequired;
            job.ShipperScac = ShipperScac;
            job.TripId = TripId;

            // Customer
            job.Customer.QuickCode = CustomerQuickCode;
            job.Customer.Contact = CustomerContact;
            job.Customer.OrganisationName = CustomerOrganisationName;
            job.Customer.AddressLines = CustomerAddressLines;
            job.Customer.City = CustomerCity;
            job.Customer.StateRegion = CustomerStateRegion;
            job.Customer.ZipPostCode = CustomerZipPostCode;
            job.Customer.Phone = CustomerPhone;
            job.Customer.MobilePhone = CustomerMobilePhone;
            job.Customer.OtherPhone = CustomerOtherPhone;
            job.Customer.Fax = CustomerFax;
            job.Customer.Email = CustomerEmail;
            job.Customer.Notes = CustomerNotes;

            // Pickup
            job.Pickup.RequestedDate = PickupRequestedDate;
            job.Pickup.RequestedDateIsExact = PickupRequestedDateIsExact ?? false;
            job.Pickup.Destination.QuickCode = PickupQuickCode;
            job.Pickup.Destination.Contact = PickupContact;
            job.Pickup.Destination.OrganisationName = PickupOrganisationName;
            job.Pickup.Destination.AddressLines = PickupAddressLines;
            job.Pickup.Destination.City = PickupCity;
            job.Pickup.Destination.StateRegion = PickupStateRegion;
            job.Pickup.Destination.ZipPostCode = PickupZipPostCode;
            job.Pickup.Destination.Phone = PickupPhone;
            job.Pickup.Destination.MobilePhone = PickupMobilePhone;
            job.Pickup.Destination.OtherPhone = PickupOtherPhone;
            job.Pickup.Destination.Fax = PickupFax;
            job.Pickup.Destination.Email = PickupEmail;
            job.Pickup.Destination.Notes = PickupNotes;

            // Dropoff
            job.Dropoff.RequestedDate = DropoffRequestedDate;
            job.Dropoff.RequestedDateIsExact = DropoffRequestedDateIsExact ?? false;
            job.Dropoff.Destination.QuickCode = DropoffQuickCode;
            job.Dropoff.Destination.Contact = DropoffContact;
            job.Dropoff.Destination.OrganisationName = DropoffOrganisationName;
            job.Dropoff.Destination.AddressLines = DropoffAddressLines;
            job.Dropoff.Destination.City = DropoffCity;
            job.Dropoff.Destination.StateRegion = DropoffStateRegion;
            job.Dropoff.Destination.ZipPostCode = DropoffZipPostCode;
            job.Dropoff.Destination.Phone = DropoffPhone;
            job.Dropoff.Destination.MobilePhone = DropoffMobilePhone;
            job.Dropoff.Destination.OtherPhone = DropoffOtherPhone;
            job.Dropoff.Destination.Fax = DropoffFax;
            job.Dropoff.Destination.Email = DropoffEmail;
            job.Dropoff.Destination.Notes = DropoffNotes;

            // Vehicles
            foreach (var vehicle in CdnSendVehicles)
                job.Vehicles.Add(new Vehicle
                {
                    Location = vehicle.Location,
                    Make = vehicle.Make,
                    Model = vehicle.Model,
                    MovementNumber = vehicle.MovementNumber,
                    Notes = vehicle.Notes,
                    Registration = vehicle.Registration,
                    Variant = vehicle.Variant,
                    Vin = vehicle.Vin
                });

            return job;
        }
    }
}
