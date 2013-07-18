using CarDeliveryNetwork.Api.Data;
using CarDeliveryNetwork.Types;

namespace CdnLink
{
    public partial class CdnSendLoad
    {
        public CdnSendLoad(Job job)
        {
            LoadId = job.LoadId;
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
            PickupRequestedDatesExact = job.Pickup.RequestedDateIsExact;
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
            DropoffRequestedDatesExact = job.Dropoff.RequestedDateIsExact;
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

            // Vehicles
            foreach (var vehicle in job.Vehicles)
                this.CdnSendVehicles.Add(new CdnSendVehicle
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
        }

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
            job.Pickup.RequestedDateIsExact = PickupRequestedDatesExact ?? false;
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
            job.Dropoff.RequestedDateIsExact = DropoffRequestedDatesExact ?? false;
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
