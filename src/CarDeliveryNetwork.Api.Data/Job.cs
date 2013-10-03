﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Api.Data.Fenkell05;
using CarDeliveryNetwork.Types;
using CarDeliveryNetwork.Types.Interfaces;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Job entity.
    /// </summary>
    public class Job : ApiEntityBase<Job>, IImportable
    {
        /// <summary>
        /// Optional (40) - A unique identifier for this job, generated by the client system.
        /// </summary>
        /// <remarks>
        /// LoadId is an optional, client system generated, unique Id by which the job can be
        /// referred to on Car Delivery Network.  Once the job is created, LoadId cannot be changed.
        /// </remarks>
        public virtual string LoadId { get; set; }

        /// <summary>
        /// Readonly (50) - The job's unique job number generated by Car Delivery Network.
        /// </summary>
        /// <remarks>
        /// JobNumber is generated by Car Delivery Network on job creation and cannot be changed.
        /// Setting this property will have no effect on the underlying job.
        /// </remarks>
        public virtual string JobNumber { get; set; }

        /// <summary>
        /// Readonly - The job's current status
        /// </summary>
        public virtual JobStatus Status { get; set; }

        /// <summary>
        /// Optional (20) - An id representing the trip this job is part of
        /// </summary>
        public virtual string TripId { get; set; }

        /// <summary>
        /// Mandatory (40) - Name of person/system initiating this job.
        /// </summary>
        public virtual string JobInitiator { get; set; }

        /// <summary>
        /// Optional (50) - A customer reference for this job (Does not have to be unique to this job.
        /// For a unique identifier see the Id and LoadId fields).
        /// </summary>
        public virtual string CustomerReference { get; set; }

        /// <summary>
        /// Optional (ntext) - The job notes. These notes will be sent to the Driver.
        /// </summary>
        public virtual string Notes { get; set; }

        /// <summary>
        /// Mandatory - The service required for this job (Driven, Transported, Either or Auto). Cannot be Either if job is allocated to a network or carrier.
        /// </summary>
        public virtual ServiceType ServiceRequired { get; set; }

  
        /// <summary>
        /// Optional - The estimated mileage of the job. If not specified system will try to calculate the mileage based on google mapping.
        /// </summary>
        public virtual int Mileage { get; set; }

        /// <summary>
        /// Optional - The price that the customer will be charged, in the smallest denomination of the currency 
        /// (pennies, cents etc). If not specified will be set to 0.
        /// </summary>
        public virtual int SellPrice { get; set; }

        /// <summary>
        /// Optional - The price that the transport company will be paid, in the smallest denomination of the currency 
        /// (pennies, cents etc). Must be specified if you are putting the Job into a Fixed Price Network.  If not specified will be set to 0.
        /// </summary>
        public virtual int BuyPrice { get; set; }

        /// <summary>
        /// Optional - The price that the carrier pays the driver
        /// (pennies, cents etc). Only used if you are the Carrier who pays the driver. If not specified will be set to 0.
        /// </summary>
        public virtual int DriverPay { get; set; }

        /// <summary>
        /// Mandatory - The customer details for this job.
        /// </summary>
        public virtual ContactDetails Customer { get; set; }

        /// <summary>
        /// Mandatory - The pick-up details for this job.
        /// </summary>
        public virtual EndPoint Pickup { get; set; }

        /// <summary>
        /// Mandatory - The drop-off details for this job.
        /// </summary>
        public virtual EndPoint Dropoff { get; set; }

        /// <summary>
        /// Mandatory - A collection of vehicles associated with this job.
        /// </summary>
        public virtual List<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Readonly - A list of documents associated with this job
        /// </summary>
        public virtual List<Document> Documents { get; set; }

        /// <summary>
        /// Optional - The SCAC of the shipper to whom this job belongs
        /// </summary>
        /// <remarks>
        /// Specifying ShipperScac during job creation will create the job on behalf of the
        /// specified shipper.  The job will be automatically allocated to the carrier creating the job.  
        /// If ShipperScac is set, AllocatedCarrierScac is ignored.
        /// </remarks>
        public virtual string ShipperScac { get; set; }

        /// <summary>
        /// Optional - The SCAC of the allocated carrier
        /// </summary>
        /// <remarks>
        /// Specifying AllocatedCarrierScac during job creation will ignore the Status field and attempt to 
        /// allocate the job directly to this carrier.  Status will be set to 'Allocated'
        /// </remarks>
        public virtual string AllocatedCarrierScac { get; set; }

        /// <summary>
        /// Optional - The RemoteId of the assigned driver
        /// </summary>
        /// <remarks>
        /// The assigned driver must be of the carrier specified in AllocatedCarrierScac.
        /// </remarks>
        public virtual string AssignedDriverRemoteId { get; set; }

        /// <summary>
        /// Readonly - The number of vehicles on this job
        /// </summary>
        public virtual int VehicleCount
        {
            get
            {
                return Vehicles == null
                    ? 0
                    : Vehicles.Count;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Job"/> class.
        /// </summary>
        public Job()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this job.
        /// </summary>
        protected virtual void InitObjects()
        {
            Customer = new ContactDetails();
            Pickup = new EndPoint();
            Dropoff = new EndPoint();
            Vehicles = new List<Vehicle>();
            Documents = new List<Document>();
        }

        /// <summary>
        /// Returns a serial representation of the job in the specified format and schema.
        /// </summary>
        /// <param name="format">Format to serialize to.</param>
        /// <param name="schema">Schema to serialize to.</param>
        /// <returns>The serialized object.</returns>
        public string ToString(MessageFormat format, WebHookSchema schema)
        {
            switch (schema)
            {
                case WebHookSchema.Cdn:
                    return Serialization.Serialize(this, format);
                case WebHookSchema.Fenkell05:
                    return new Delivery(this).ToString();
                default:
                    throw new ArgumentException(string.Format("Schema {0} is not a valid WebHookSchema", schema), "schema");
            };
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network Job job entities.
    /// </summary>
    [CollectionDataContract]
    public class Jobs : ApiEntityCollectionBase<Job, Jobs>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Jobs"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Jobs() { } 

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Jobs"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public Jobs(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Jobs"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="jobs">The collection of jobs whose elements are copied to the new collection.</param>
        public Jobs(IEnumerable<Job> jobs) : base(jobs) { }
    }
}