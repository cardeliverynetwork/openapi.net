﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        const string ServiceApiKey = "D19CD37E-9722-4881-A40F-A7797D8C3669"; // local dev key - do not use

        static void Main(string[] args)
        {
            // Construct an instance of the API
            var api = new OpenApi(ServiceUrl, ServiceApiKey);

            // Create a job
            var newjob = api.CreateJob(GetTestJob());

            // Get some details about a job
            var targetJobId = 9697;
            var job = api.GetJob(targetJobId);
            var docs = api.GetJobDocuments(targetJobId);
            api.CancelJob(targetJobId, "Cancelled by customer.");
        }

        private static Job GetTestJob()
        {
            var newjob = new Job();

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
    }
}