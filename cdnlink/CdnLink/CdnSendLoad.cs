using CarDeliveryNetwork.Api.Data;
using CarDeliveryNetwork.Types;

namespace CdnLink
{
    public partial class CdnSendLoad
    {
        public Job ToCdnJob()
        {
            var job = new Job
            {
                LoadId = LoadId,
                AllocatedCarrierScac = AllocatedCarrierScac,
                AssignedDriverRemoteId = AssignedDriverRemoteId,
                AssignedTruckRemoteId = AssignedTruckRemoteId,
                BuyPrice = BuyPrice ?? 0,
                ContractedCarrierScac = ContractedCarrierScac,
                CustomerReference = CustomerReference,
                DriverPay = DriverPay ?? 0,
                JobInitiator = JobInitiator,
                Notes = Notes,
                SellPrice = SellPrice ?? 0,
                ServiceRequired = (ServiceType) ServiceRequired,
                ShipperScac = ShipperScac,
                TripId = TripId,
                Customer =
                {
                    QuickCode = CustomerQuickCode,
                    Contact = CustomerContact,
                    OrganisationName = CustomerOrganisationName,
                    AddressLines = CustomerAddressLines,
                    City = CustomerCity,
                    StateRegion = CustomerStateRegion,
                    ZipPostCode = CustomerZipPostCode,
                    Phone = CustomerPhone,
                    MobilePhone = CustomerMobilePhone,
                    OtherPhone = CustomerOtherPhone,
                    Fax = CustomerFax,
                    Email = CustomerEmail,
                    Notes = CustomerNotes
                },
                Pickup =
                {
                    RequestedDate = PickupRequestedDate,
                    RequestedDateIsExact = PickupRequestedDateIsExact ?? false,
                    Destination =
                    {
                        QuickCode = PickupQuickCode,
                        Contact = PickupContact,
                        OrganisationName = PickupOrganisationName,
                        AddressLines = PickupAddressLines,
                        City = PickupCity,
                        StateRegion = PickupStateRegion,
                        ZipPostCode = PickupZipPostCode,
                        Phone = PickupPhone,
                        MobilePhone = PickupMobilePhone,
                        OtherPhone = PickupOtherPhone,
                        Fax = PickupFax,
                        Email = PickupEmail,
                        Notes = PickupNotes
                    }
                },
                Dropoff =
                {
                    RequestedDate = DropoffRequestedDate,
                    RequestedDateIsExact = DropoffRequestedDateIsExact ?? false,
                    Destination =
                    {
                        QuickCode = DropoffQuickCode,
                        Contact = DropoffContact,
                        OrganisationName = DropoffOrganisationName,
                        AddressLines = DropoffAddressLines,
                        City = DropoffCity,
                        StateRegion = DropoffStateRegion,
                        ZipPostCode = DropoffZipPostCode,
                        Phone = DropoffPhone,
                        MobilePhone = DropoffMobilePhone,
                        OtherPhone = DropoffOtherPhone,
                        Fax = DropoffFax,
                        Email = DropoffEmail,
                        Notes = DropoffNotes
                    }
                }
            };

            if (CdnSendVehicles != null)
            {
                foreach (var vehicle in CdnSendVehicles)
                {
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
                }
            }

            if (CdnSendTranships != null)
            {
                foreach (var tranship in CdnSendTranships)
                {
                    job.Tranships.Add(new Tranship
                    {
                        AssignedDriverRemoteId = tranship.AssignedDriverRemoteId,
                        AssignedTruckRemoteId = tranship.AssignedTruckRemoteId,
                        RequestedDate = tranship.RequestedDate,

                        RequestedDateIsExact = tranship.RequestedDateIsExact.HasValue
                            ? tranship.RequestedDateIsExact.Value
                            : false,

                        ScheduledDate = tranship.RequestedDate,

                        TranshipNumber = tranship.TranshipNumber.HasValue
                            ? tranship.TranshipNumber.Value
                            : 0,

                        TripId = tranship.TripId,

                        Destination = new ContactDetails
                        {
                            QuickCode = tranship.QuickCode,
                            Contact = tranship.Contact,
                            OrganisationName = tranship.OrganisationName,
                            AddressLines = tranship.AddressLines,
                            City = tranship.City,
                            StateRegion = tranship.StateRegion,
                            ZipPostCode = tranship.ZipPostCode,
                            Phone = tranship.Phone,
                            MobilePhone = tranship.MobilePhone,
                            OtherPhone = tranship.OtherPhone,
                            Fax = tranship.Fax,
                            Email = tranship.Email,
                            Notes = tranship.Notes
                        }
                    });
                }
            }

            return job;
        }
    }
}
