using System.Linq;
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
            JobNumber = job.JobNumber;
            Mileage = job.Mileage;
            Notes = job.Notes;
            SellPrice = job.SellPrice;
            ServiceRequired = (int)job.ServiceRequired;
            ShipperScac = job.ShipperScac;
            Status = (int)job.Status;
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

            if (job.Pickup.Signoff != null)
            {
                PickupNotSignedReason = job.Pickup.Signoff.NotSignedReason;
                PickupSignedBy = job.Pickup.Signoff.SignedBy;
                PickupTime = job.Pickup.Signoff.Time;
                PickupUrl = job.Pickup.Signoff.Url; 
            }

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

            if (job.Dropoff.Signoff != null)
            {
                DropoffNotSignedReason = job.Dropoff.Signoff.NotSignedReason;
                DropoffSignedBy = job.Dropoff.Signoff.SignedBy;
                DropoffTime = job.Dropoff.Signoff.Time;
                DropoffUrl = job.Dropoff.Signoff.Url; 
            }

            // Create Vehicle Records
            if (job.Vehicles != null)
                foreach (var vehicle in job.Vehicles)
                {
                    CdnReceivedVehicles.Add(new CdnReceivedVehicle(vehicle));
                    
                    // Create Vehicle Document records
                    foreach (var vPhoto in vehicle.Photos)
                    {
                        var vDoc = new CdnReceivedDocument(vPhoto);
                        vDoc.CdnVehicleId = vehicle.Id;
                        CdnReceivedDocuments.Add(vDoc);
                    }
                }

            // Create any Document records weren't already added from the vehicles
            if (job.Documents != null)
                foreach (var document in job.Documents)
                    if(CdnReceivedDocuments.Where(d=> d.Url == document.Url).Count() == 0)
                        CdnReceivedDocuments.Add(new CdnReceivedDocument(document));
        }
    }
}
