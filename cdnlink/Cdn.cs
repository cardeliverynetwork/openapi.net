using System;
using System.Linq;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using CarDeliveryNetwork.Types;
using log4net;

namespace CdnLink
{
    internal static class Cdn
    {
        enum SendStatus
        {
            Queued = 10,
            Processing = 20,
            Sent = 30,

            Error = 70
        }

        enum ReceiveStatus
        {
        }

        private static readonly ILog _log = LogManager.GetLogger(typeof(Cdn));

        internal static int Send()
        {
            try
            {
                // Get records from Send tables
                var db = new CdnLinkDataContext();

                var sends = from send in db.CdnSends
                            where send.Status == (int)SendStatus.Queued
                            select send;

                var api = new OpenApi(GetSetting("CDNLINK_API_URL"), GetSetting("CDNLINK_API_KEY"));

                foreach (var send in sends)
                {
                    send.Status = (int)SendStatus.Processing;
                    send.ProcessingDate = DateTime.Now;
                    db.SubmitChanges();

                    var load = send.CdnSendLoad;
                    var newjob = new Job();

                    // Job details
                    newjob.LoadId = load.LoadId;
                    newjob.AllocatedCarrierScac = load.AllocatedCarrierScac;
                    newjob.AssignedDriverRemoteId = load.AssignedDriverRemoteId;
                    newjob.BuyPrice = load.BuyPrice ?? 0;
                    newjob.CustomerReference = load.CustomerReference;
                    newjob.DriverPay = load.DriverPay ?? 0;
                    newjob.JobInitiator = load.JobInitiator;
                    newjob.Notes = load.Notes;
                    newjob.SellPrice = load.SellPrice ?? 0;
                    newjob.ServiceRequired = (ServiceType)load.ServiceRequired;
                    newjob.ShipperScac = load.ShipperScac;
                    newjob.TripId = load.TripId;

                    // Customer
                    newjob.Customer.QuickCode = load.CustomerQuickCode;
                    newjob.Customer.Contact = load.CustomerContact;
                    newjob.Customer.OrganisationName = load.CustomerOrganisationName;
                    newjob.Customer.AddressLines = load.CustomerAddressLines;
                    newjob.Customer.City = load.CustomerCity;
                    newjob.Customer.StateRegion = load.CustomerStateRegion;
                    newjob.Customer.ZipPostCode = load.CustomerZipPostCode;
                    newjob.Customer.Phone = load.CustomerPhone;
                    newjob.Customer.MobilePhone = load.CustomerMobilePhone;
                    newjob.Customer.OtherPhone = load.CustomerOtherPhone;
                    newjob.Customer.Fax = load.CustomerFax;
                    newjob.Customer.Email = load.CustomerEmail;
                    newjob.Customer.Notes = load.CustomerNotes;

                    // Pickup
                    newjob.Pickup.RequestedDate = load.PickupRequestedDate;
                    newjob.Pickup.RequestedDateIsExact = load.PickupRequestedDatesExact ?? false;
                    newjob.Pickup.Destination.QuickCode = load.PickupQuickCode;
                    newjob.Pickup.Destination.Contact = load.PickupContact;
                    newjob.Pickup.Destination.OrganisationName = load.PickupOrganisationName;
                    newjob.Pickup.Destination.AddressLines = load.PickupAddressLines;
                    newjob.Pickup.Destination.City = load.PickupCity;
                    newjob.Pickup.Destination.StateRegion = load.PickupStateRegion;
                    newjob.Pickup.Destination.ZipPostCode = load.PickupZipPostCode;
                    newjob.Pickup.Destination.Phone = load.PickupPhone;
                    newjob.Pickup.Destination.MobilePhone = load.PickupMobilePhone;
                    newjob.Pickup.Destination.OtherPhone = load.PickupOtherPhone;
                    newjob.Pickup.Destination.Fax = load.PickupFax;
                    newjob.Pickup.Destination.Email = load.PickupEmail;
                    newjob.Pickup.Destination.Notes = load.PickupNotes;

                    // Dropoff
                    newjob.Dropoff.RequestedDate = load.DropoffRequestedDate;
                    newjob.Dropoff.RequestedDateIsExact = load.DropoffRequestedDatesExact ?? false;
                    newjob.Dropoff.Destination.QuickCode = load.DropoffQuickCode;
                    newjob.Dropoff.Destination.Contact = load.DropoffContact;
                    newjob.Dropoff.Destination.OrganisationName = load.DropoffOrganisationName;
                    newjob.Dropoff.Destination.AddressLines = load.DropoffAddressLines;
                    newjob.Dropoff.Destination.City = load.DropoffCity;
                    newjob.Dropoff.Destination.StateRegion = load.DropoffStateRegion;
                    newjob.Dropoff.Destination.ZipPostCode = load.DropoffZipPostCode;
                    newjob.Dropoff.Destination.Phone = load.DropoffPhone;
                    newjob.Dropoff.Destination.MobilePhone = load.DropoffMobilePhone;
                    newjob.Dropoff.Destination.OtherPhone = load.DropoffOtherPhone;
                    newjob.Dropoff.Destination.Fax = load.DropoffFax;
                    newjob.Dropoff.Destination.Email = load.DropoffEmail;
                    newjob.Dropoff.Destination.Notes = load.DropoffNotes;

                    // Vehicles
                    foreach (var vehicle in load.CdnSendVehicles)
                        newjob.Vehicles.Add(new Vehicle()
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

                    var createdJob = api.CreateJob(newjob);
                }
                return 0;
            }
            catch (HttpResourceFaultException ex)
            {
                Console.WriteLine("HttpResourceFaultException: StatusCode: {0} Message: {1}", ex.StatusCode, ex.Message);
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: Message: {0}", ex.Message);
                return 1;
            }
        }

        internal static int Receive()
        {
            var ftp = new FtpBox(
                GetSetting("CDNLINK_FTP_HOST"), 
                GetSetting("CDNLINK_FTP_ROOT"), 
                GetSetting("CDNLINK_FTP_USER"), 
                GetSetting("CDNLINK_FTP_PASS"));

            var files = ftp.GetFileList();
            var fileCount = files != null ? files.Count : 0;

            if (fileCount > 0)
            {
                _log.InfoFormat("Receive: Processing {0} file(s).", fileCount);

                var db = new CdnLinkDataContext();

                foreach (var file in files)
                {
                    // If we haven't already processed this file
                    if (db.CdnReceivedFtpFiles.Where(f => f.Filename.Contains(file)).Count() == 0)
                    {
                        var receivedFile = new CdnReceivedFtpFile();
                        receivedFile.Filename = file;
                        receivedFile.JsonMessage = ftp.GetFileContents(file);
                        db.CdnReceivedFtpFiles.InsertOnSubmit(receivedFile);
                        db.SubmitChanges();
                    }

                    // Delete file from FTP server
                    ftp.DeleteFile(file);
                }
            }
            else
                _log.Info("Receive: Nothing to do.");

            return fileCount;
        }

        /// <summary>
        /// Gets the specified setting from the system environment or web.config, in that order
        /// </summary>
        /// <param name="name">Name of setting to fetch</param>
        /// <returns>The specified setting</returns>
        private static string GetSetting(string name)
        {
            _log.DebugFormat("GetSetting: '{0}'.", name);
            var setting = Environment.GetEnvironmentVariable(name);
            if (setting != null)
            {
                _log.DebugFormat("GotSetting: '{0}' from system environment.", setting);
                return setting;
            }
            
            setting = Settings.Default[name] as string;
            if (setting != null)
            {
                _log.DebugFormat("GotSetting: '{0}' from application settings.", setting);
                return setting;
            }

            _log.DebugFormat("GetSetting: '{0}' was not found.", name);
            return null;
        }
    }
}
