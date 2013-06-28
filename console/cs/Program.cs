using System;
using System.Collections.Generic;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using CarDeliveryNetwork.Types;

namespace console
{
    class Program
    {
        // Car Delivery Network API url
        const string ServiceUrl = "http://localhost/cdn/openapi";

        // const string ServiceUrl = "https://go.cardeliverynetwork.com/trainingus/openapi";   // CDN US Training
        // const string ServiceUrl = "https://go.cardeliverynetwork.com/traininguk/openapi";   // CDN UK Training

        // API user's key
         const string ServiceApiKey = "fb04420a-49d0-4585-af57-1d390bfa12e7"; // local dev key - do not use

        static void Main(string[] args)
        {
            try
            {
                // Construct an instance of the API
                var api = new OpenApi(ServiceUrl, ServiceApiKey);


                //////////////////
                // Create a job //
                //////////////////

                // The load Id, can only be used once
                var loadId = "TestLoad2";

                // Create the new job.
                var newjob = api.CreateJob(GetTestJob(loadId));


                ///////////////////////
                // Add more vehicles //
                ///////////////////////
                
                // Identifying the job by its Id, create some extra vehicles
                api.CreateJobVehicles(newjob.Id, new Vehicles { new Vehicle { Vin = "aaaa" } });
              
                // Identifying the job by its LoadId, create some extra vehicles
                api.CreateJobVehicles(loadId, new Vehicles { new Vehicle { Vin = "bbbb" } });


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
            var customer = new ContactDetails
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