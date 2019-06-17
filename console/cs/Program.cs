using System;
using System.Collections.Generic;
using System.IO;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using CarDeliveryNetwork.Types;

namespace console
{
    class Program
    {
        const bool useApi2 = false;

        // API 2 URLs
        //const string ServiceUrl = "http://localhost/vindeliver/api/vind2/openapi";                                    // Local Dev
        // const string ServiceUrl = "https://build1.cardeliverynetwork.com/vindeliver2uatuk/api/vind2/openapi";   // UK UAT
        // const string ServiceUrl = "https://vindeliver.cardeliverynetwork.com/uk/api/vind2/openapi";             // UK Training
        // const string ServiceUrl = "https://vindeliver.cardeliverynetwork.com/us/api/vind2/openapi";             // US Training

        // Legacy API URLs
        // const string ServiceUrl = "http://localhost/cdn/openapi";                                            // Local Dev
        // const string ServiceUrl = "https://go.cardeliverynetwork.com/trainingus/openapi";                    // US Training
        // const string ServiceUrl = "https://go.cardeliverynetwork.com/traininguk/openapi";                    // UK Training
         const string ServiceUrl = "https://go.cardeliverynetwork.com/us/openapi";                    // UK Training

        // http://go.cardeliverynetwork.com/us/OpenApi/Jobs/4089747/Action?apikey=d8dd7eee-1eb7-4d38-9e6b-c6f10333a6bd&format=json,

        // API 2 user's credentials
        const string ServiceUsername = "cwallis";
        const string ServicePassword = "test";

        // Legacy API user's key
        //const string ServiceApiKey = "1dfbedad-5f79-45c1-b835-408d24688377"; // local dev key - do not use

        const string ServiceApiKey = "d8dd7eee-1eb7-4d38-9e6b-c6f10333a6bd"; // us live

        static void Main(string[] args)
        {
            try
            {
                TestCdx();
                return;

                // Construct an instance of the API
                var api = useApi2
                    ? new OpenApi(ServiceUrl, ServiceUsername, ServicePassword, "The Calling App")
                    : new OpenApi(ServiceUrl, ServiceApiKey, "The Calling App");

                //////////////////
                // Create a job //
                //////////////////

                var kwjob = api.GetJob(4089747);
                var kw = api.GetHomeFleet();
                var otr = new CarDeliveryNetwork.Api.Data.Ford.Otr214(kwjob, kw);

                string filename;
                var otrString = otr.ToString(0, WebHookEvent.PickupStop, DateTime.Now, "lala123", DateTime.Now, true, out filename);


                // The load Id, can only be used once
                var loadId = string.Format("Load-{0}", DateTime.UtcNow.Ticks);

                // Create the new job.
                var newjob = api.CreateJob(GetTestJob(loadId));

                ///////////////////////
                // Add more vehicles //
                ///////////////////////

                // Identifying the job by its Id, create some extra vehicles
                //api.CreateJobVehicles(newjob.Id, new Vehicles { new Vehicle { Vin = "aaaa" } });

                // Identifying the job by its LoadId, create some extra vehicles
                //api.CreateJobVehicles(loadId, new Vehicles { new Vehicle { Vin = "bbbb" } });


                //////////////////////////////////
                // Retrieve various job details //
                //////////////////////////////////

                // Identifying the job by its Id, get all vehicles for the job
                var allVehicles = api.GetJobVehicles(newjob.Id);

                // Identifying the job by its LoadId, get all vehicles for the job
                allVehicles = api.GetJobVehicles(loadId);

                // Retrieve the entire job by its LoadId
                var job = api.GetJob(loadId);

                // Get any documents against the job
                var docs = api.GetJobDocuments(loadId);
            }
            catch (HttpResourceFaultException ex)
            {
                Console.WriteLine("HttpResourceFaultException: StatusCode: {0} Message: {1}", ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: Message: {0}", ex.Message);
            }
        }

        private static void TestCdx()
        {
            var cdx = new CdxVehicleExchange
            {
                Shipment = new CdxShipment
                {
                    EventDateTime = (DateTime.UtcNow - TimeSpan.FromSeconds(42)).ToString("yyyy-MM-dd hh:mm:ss"),
                    SenderInventoryId = "USAA-8333306-00052-G1316414",
                    SenderScac = "USAA",
                    ReceiverScac = "CENT",
                    SenderJobNumber = "Unknown",
                    SenderLoadId = "USAA-8333306-00052-G1316414",
                    SenderTripId = "8333306",
                    ReceiverLoadId = "Unknown",
                    ReceiverTripId = "Unknown",
                    ReceiverJobNumber = "Unknown",
                    Price = 12312,
                    Notes = "shipmentnotes",
                    TruckId = "Unknown",
                    DriverId = "Unknown",
                },
                CdxVehicles = new List<CdxVehicle>
                {
                    new CdxVehicle
                    {
                         Make = "GMC",
                         Model = "EQUINOX: 1XR26",
                         ShipperScac = "GST",
                         Vin = "3GNAXKEV6KS604218",
                         Variant = "GD1",
                         Color = "color",
                         ScheduledPickupDate = DateTime.UtcNow + TimeSpan.FromDays(1),
                         ScheduledDeliveryDate = DateTime.UtcNow + TimeSpan.FromDays(2),
                         VehicleType = "vehicletype",
                         Weight = 123,
                         Location = "location",
                         MovementNumber = "movementnumber",
                         ReferenceNumber = "referencenumber",
                         Price = 5643,
                         Notes = "vehiclenotes",
                         Origin = new ContactDetails
                         {
                            QuickCode = "1316414",
                            InternalQuickCode = "G1316414",
                            LocationCode = "senderlocationcode",
                            OrganizationName = "SOUTH CHARLOTTE CHEVY",
                            AddressLines = "9325 South BLVD",
                            City = "Charlotte",
                            StateRegion = "NC",
                            ZipPostCode = "28273",
                            CountryCode = "US",
                            Contact = "*",
                            Email = "email",
                            Phone = "7045516400",
                            Notes = "Origin Notes",
                         },
                         Destination = new ContactDetails
                         {
                            QuickCode = "1316415",
                            InternalQuickCode = "G1316415",
                            LocationCode = "senderlocationcode",
                            OrganizationName = "Billy Bob",
                            AddressLines = "9326 South BLVD",
                            City = "Charlotte",
                            StateRegion = "NC",
                            ZipPostCode = "28273",
                            CountryCode = "US",
                            Contact = "Billy",
                            Email = "email",
                            Phone = "7045516401",
                            Notes = "Destination Notes",
                         }
                    },
                    //new Vehicle{},
                }
            };

            var cdsstr = cdx.ToString(MessageFormat.Xml);
            var xml = File.ReadAllText("TestFiles\\CdxVehicleExchange.xml");
            var deserialisedExchange = CdxVehicleExchange.FromString(xml, MessageFormat.Xml);

            var cdxStatus = new CdxStatus
            {
                Shipment = new CdxShipment
                {
                    ExchangeId = 42,
                    EventDateTime = (DateTime.UtcNow - TimeSpan.FromSeconds(42)).ToString("yyyy-MM-dd hh:mm:ss"),
                    SenderScac = "SenderScac",
                    ReceiverScac = "ReceiverScac",
                    SenderJobNumber = "SenderJobNumber",
                    SenderLoadId = "USAA-8333306-00052-G1316414",
                    SenderTripId = "8333306",
                    ReceiverJobNumber = "ReceiverJobNumber",
                    ReceiverLoadId = "ReceiverLoadId",
                    ReceiverTripId = "ReceiverTripId",
                    DriverId = "DriverId",
                    TruckId = "TruckId",
                    Status = CdxShipmentStatus.OnWayToPickup,
                },
                CdxVehicles = new List<CdxVehicle>
                {
                    new CdxVehicle
                    {
                         Vin = "3GNAXKEV6KS604218",
                    },
                    new CdxVehicle
                    {
                         Vin = "3GNAXKEV6KS604219",
                    },
                    new CdxVehicle
                    {
                         Vin = "3GNAXKEV6KS604220",
                    }
                }
            };

            cdsstr = cdxStatus.ToString(MessageFormat.Xml);
            xml = File.ReadAllText("TestFiles\\CdxStatus.xml");
            var deserialisedStatus = CdxStatus.FromString(xml, MessageFormat.Xml);

            var cdxStop = new CdxStop
            {
                StopType = 0,
                NumberOfVehicles = 3,
                ProofDocUrl = "https://go.cardeliverynetwork.com/us/Proof.aspx?uid=327110:C003F94",
                EmailList = "abc@emailserver.com, 123@emailserver.com",
                SignatureType = 0,
                SignedBy = "James Shaw",
                NotSignedReasons = "No one available to sign, Subject to inspection, Customer refused to sign device",
                SignOffComment = "Damage already on vehicles",
                Signature = "https://go.cardeliverynetwork.com/us/Proof.aspx?uid=327110:C003F94",

                Shipment = new CdxShipment
                {
                    ExchangeId = 42,
                    EventDateTime = (DateTime.UtcNow - TimeSpan.FromSeconds(42)).ToString("yyyy-MM-dd hh:mm:ss"),
                    SenderScac = "SenderScac",
                    ReceiverScac = "ReceiverScac",
                    SenderJobNumber = "SenderJobNumber",
                    SenderLoadId = "USAA-8333306-00052-G1316414",
                    SenderTripId = "8333306",
                    ReceiverJobNumber = "ReceiverJobNumber",
                    ReceiverLoadId = "ReceiverLoadId",
                    ReceiverTripId = "ReceiverTripId",
                    DriverId = "DriverId",
                    TruckId = "TruckId",
                    Status = CdxShipmentStatus.OnWayToPickup
                },
                CdxVehicles = new List<CdxVehicle>
                {
                    new CdxVehicle
                    {
                         Vin = "3GNAXKEV6KS604219",
                         Status = VehicleStatus.PickedUp,
                         SignedBy = "Mike Thorby",
                         Signature = "https://go.cardeliverynetwork.com/us/Proof.aspx?uid=327110:C003F94",
                         SignoffComment = "SignoffComment"
                    },
                    new CdxVehicle
                    {
                         Vin = "3GNAXKEV6KS604218",
                         Status = VehicleStatus.PickedUp,
                         SignedBy = "Mike Thorby",
                         Signature = "https://go.cardeliverynetwork.com/us/Proof.aspx?uid=327110:C003F94",
                         SignoffComment = "SignoffComment",
                         DamageAtPickup = new List<DamageItem>
                         {
                             new DamageItem
                             {
                                 Area = new DamageItem.DamageArea{ Code = "17" },
                                 Type = new DamageItem.DamageType{ Code = "14" },
                                 Severity = new DamageItem.DamageSeverity{ Code = "1" },
                                 Description = "17:Quarter Panel/Pick-Up Box-Right > 14:Dented-Paint/Chrome not damaged > 1:Less than 1 inch"
                             },
                             new DamageItem
                             {
                                 Area = new DamageItem.DamageArea{ Code = "03" },
                                 Type = new DamageItem.DamageType{ Code = "07" },
                                 Severity = new DamageItem.DamageSeverity{ Code = "1" },
                                 Description = "03:Bumper/Cover/Ext-Front > 07:Gouged > 1:Less than 1 inch"
                             }
                         }
                    },
                    new CdxVehicle
                    {
                         Vin = "3GNAXKEV6KS604220",
                         Status = VehicleStatus.NotPickedUp,
                         NonCompletionReason = "Not Available",
                         SignedBy = "Mike Thorby",
                         Signature = "https://go.cardeliverynetwork.com/us/Proof.aspx?uid=327110:C003F94",
                         SignoffComment = "SignoffComment"
                    }
                }
            };

            cdsstr = cdxStop.ToString(MessageFormat.Xml);
            xml = File.ReadAllText("TestFiles\\CdxStop.xml");
            var deserialisedStop = CdxStop.FromString(xml, MessageFormat.Xml);
        }

        private static Job GetTestJob(string loadId = null)
        {
            var newjob = new Job();
            newjob.LoadId = loadId;

            // Setup some basic job details
            newjob.JobInitiator = "Chris Wallis";
            newjob.CustomerReference = "REF123";
            newjob.Notes = "These are the notes";
            newjob.ServiceRequired = ServiceType.Transported;

            // Setup the customer record
            newjob.Customer.QuickCode = "COMPANY1";
            newjob.Customer.Contact = "John Smith";
            newjob.Customer.OrganisationName = "The Company";
            newjob.Customer.AddressLines = "82a Wellington Street";
            newjob.Customer.City = "Thame";
            newjob.Customer.StateRegion = "Oxfordshire";
            newjob.Customer.ZipPostCode = "OX9 3BN";
            newjob.Customer.Phone = "123 123 123";
            newjob.Customer.Notes = "The Customer Notes";
            newjob.Customer.Email = "jsmith@example.com";

            // Setup the pick-up details
            newjob.Pickup.RequestedDate = DateTime.Today + TimeSpan.FromDays(1);
            newjob.Pickup.RequestedDateIsExact = true;
            newjob.Pickup.Destination.QuickCode = "WHVCS";
            newjob.Pickup.Destination.Contact = "Paul Pie";
            newjob.Pickup.Destination.OrganisationName = "We Have Cars Ltd.";
            newjob.Pickup.Destination.AddressLines = "4 Well Dean\r\nCastlefields";
            newjob.Pickup.Destination.City = "Prudhoe";
            newjob.Pickup.Destination.StateRegion = "Northumberland";
            newjob.Pickup.Destination.ZipPostCode = "NE42 5QQ";
            newjob.Pickup.Destination.Phone = "456 456 456";
            newjob.Pickup.Destination.Notes = "These are the pickup notes";

            // Setup the drop-off details
            newjob.Dropoff.RequestedDate = DateTime.Today + TimeSpan.FromDays(3);
            newjob.Dropoff.RequestedDateIsExact = false;
            newjob.Dropoff.Destination.QuickCode = "WNDCS";
            newjob.Dropoff.Destination.Contact = "Peter Piper";
            newjob.Dropoff.Destination.OrganisationName = "We Need Cars Ltd.";
            newjob.Dropoff.Destination.AddressLines = "1 Yeomans Court";
            newjob.Dropoff.Destination.City = "Hemel Hempstead";
            newjob.Dropoff.Destination.StateRegion = "Hertfordshire";
            newjob.Dropoff.Destination.ZipPostCode = "HP2 7GJ";
            newjob.Dropoff.Destination.Phone = "789 789 789";
            newjob.Dropoff.Destination.Notes = "These are the drop-off notes";

            // Add some vehicles
            newjob.Vehicles.Add(new Vehicle()
            {
                Registration = "JJ9 876",
                Vin = "09876543210987654",
                Make = "Vauxhall",
                Model = "Corsa"
            });

            newjob.Vehicles.Add(new Vehicle()
            {
                Registration = "GG1 123",
                Vin = "12345678901234567",
                Make = "Renault",
                Model = "5",
                Variant = "GTI",
                Notes = "Pocket rocket"
            });
            return newjob;
        }

        private static Jobs GetMultiDropJobs()
        {
            var customer = new Customer
            {
                // Customer address details
            };

            var pickupEndPoint = new EndPoint
            {
                RequestedDate = DateTime.Today,
                RequestedDateIsExact = false,
                Destination = new ContactDetails
                {
                    // Pickup address details
                }
            };

            var drop1EndPoint = new EndPoint
            {
                RequestedDate = DateTime.Today + TimeSpan.FromDays(3),
                Destination = new ContactDetails
                {
                    // Drop 1 address details
                }
            };

            var drop2EndPoint = new EndPoint
            {
                RequestedDate = DateTime.Today + TimeSpan.FromDays(3),
                Destination = new ContactDetails
                {
                    // Drop 2 address details
                }
            };

            var drop1Job = new Job
            {
                LoadId = "Drop1",
                CustomerReference = "LoadA",
                Customer = customer,
                Pickup = pickupEndPoint,
                Dropoff = drop1EndPoint,
                Vehicles = new List<Vehicle>
                {
                    new Vehicle{ Vin = "Vehicle111" },
                    new Vehicle{ Vin = "Vehicle222" }
                }
            };

            var drop2Job = new Job
            {
                LoadId = "Drop2",
                CustomerReference = "LoadA",
                Customer = customer,
                Pickup = pickupEndPoint,
                Dropoff = drop2EndPoint,
                Vehicles = new List<Vehicle>
                {
                    new Vehicle{ Vin = "Vehicle333" },
                }
            };

            return new Jobs { drop1Job, drop2Job };
        }
    }
}