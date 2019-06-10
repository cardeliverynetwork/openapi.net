﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Api.Data.Fenkell;
using CarDeliveryNetwork.Api.Data.Ford;
using CarDeliveryNetwork.Api.Data.Icl;
using CarDeliveryNetwork.Api.Data.TmwV1;
using CarDeliveryNetwork.Types;
using CarDeliveryNetwork.Types.Interfaces;
using CarDeliveryNetwork.Api.Data.CdxFlat;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Job entity.
    /// </summary>
    public class Job : ApiEntityBase<Job>, IImportable
    {
        /// <summary>
        /// Readonly - The time in UTC that the job was created
        /// </summary>
        public virtual DateTime CreatedDate { get; set; }

        /// <summary>
        /// Optional (40) - A unique identifier for this job, generated by the client system.
        /// </summary>
        /// <remarks>
        /// LoadId is an optional, client system generated, unique Id by which the job can be
        /// referred to on Car Delivery Network.  Once the job is created, LoadId cannot be changed.
        /// </remarks>
        public virtual string LoadId { get; set; }

        /// <summary>
        /// Readonly - A unique identifier for the CdnExchange this job is part of
        /// </summary>
        public virtual int? CdxExchangeId { get; set; }

        /// <summary>
        /// Optional (API2) - A suffix to apply to the generated job number
        /// </summary>
        /// <remarks>
        /// JobNumberSuffix is an optional.  Appended to the end of the generated job number on 
        /// job ceation.
        /// </remarks>
        public virtual string JobNumberSuffix { get; set; }

        /// <summary>
        /// Readonly (25) - The job's associated Request Reference Id from vinDISPATCH, if applicable
        /// </summary>
        /// <remarks>
        /// RequestReferenceId is generated by Car Delivery Network's vinDISPATCH system.
        /// Setting this property will have no effect on the underlying job.
        /// </remarks>
        public virtual string RequestReferenceId { get; set; }

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
        /// Optional - The estimated travel time of the job.
        /// </summary>
        public virtual int TravelTimeMinutes { get; set; }

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
        /// Optional - Payment term for this job.
        /// </summary>
        public virtual int PaymentTerm { get; set; }

        /// <summary>
        /// Mandatory - The customer details for this job.
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// Mandatory - The pick-up details for this job.
        /// </summary>
        public virtual EndPoint Pickup { get; set; }

        /// <summary>
        /// Mandatory - The drop-off details for this job.
        /// </summary>
        public virtual EndPoint Dropoff { get; set; }

        /// <summary>
        /// Optional - A list of tranships associated with this job.
        /// </summary>
        public virtual List<Tranship> Tranships { get; set; }

        /// <summary>
        /// Mandatory - A collection of vehicles associated with this job.
        /// </summary>
        public virtual List<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Readonly - A list of history records associated with this job
        /// </summary>
        public virtual List<JobStatusHistoryItem> History { get; set; }

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
        /// Optional - The CDN Id of the shipper to whom this job belongs
        /// </summary>
        /// <remarks>
        /// (Overrides ShipperScac) Specifying ShipperId during job creation will create the job on behalf of the
        /// specified shipper.  The job will be automatically allocated to the carrier creating the job.  
        /// If ShipperId is set, AllocatedCarrierId is ignored.
        /// </remarks>
        public virtual int ShipperId { get; set; }

        /// <summary>
        /// Optional - The SCAC of the entity contracted by the shipper.
        /// </summary>
        /// <remarks>
        /// To be specified when a different carrier will be carrying out the job.  Must be specified 
        /// in conjunction with a ShipperScac and AllocatedCarrierScac.  Only specifiable in API calls
        /// made BY the contracted carrier.
        /// </remarks>
        public virtual string ContractedCarrierScac { get; set; }

        /// <summary>
        /// Optional - The CDN Id of the entity contracted by the shipper.
        /// </summary>
        /// <remarks>
        /// (Overrides ContractedCarrierId) To be specified when a different carrier will be carrying out the job.  Must be specified 
        /// in conjunction with a ShipperId and AllocatedCarrierId.  Only specifiable in API calls
        /// made BY the contracted carrier.
        /// </remarks>
        public virtual int ContractedCarrierId { get; set; }

        /// <summary>
        /// Optional - The SCAC of the allocated carrier
        /// </summary>
        /// <remarks>
        /// Specifying AllocatedCarrierScac during job creation will ignore the Status field and attempt to 
        /// allocate the job directly to this carrier.  Status will be set to 'Allocated'
        /// </remarks>
        public virtual string AllocatedCarrierScac { get; set; }

        /// <summary>
        /// Optional - The CDN Id of the allocated carrier
        /// </summary>
        /// <remarks>
        /// (Overrides AllocatedCarrierScac) Specifying AllocatedCarrierId during job creation will ignore the Status field and attempt to 
        /// allocate the job directly to this carrier.  Status will be set to 'Allocated'
        /// </remarks>
        public virtual int AllocatedCarrierId { get; set; }

        /// <summary>
        /// Optional - The RemoteId of the assigned driver
        /// </summary>
        /// <remarks>
        /// The assigned driver must be of the carrier specified in AllocatedCarrierScac.
        /// </remarks>
        public virtual string AssignedDriverRemoteId { get; set; }

        /// <summary>
        /// ReadOnly - The Name of the assigned driver
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        public virtual string AssignedDriverName { get; set; }

        /// <summary>
        /// Optional - The Id of the assigned driver
        /// </summary>
        /// <remarks>
        /// The assigned driver must be of the carrier specified in AllocatedCarrierScac.
        /// </remarks>
        public virtual int? AssignedDriverId { get; set; }

        /// <summary>
        /// Optional - The RemoteId of the assigned truck
        /// </summary>
        /// <remarks>
        /// The assigned truck must be of the carrier specified in AllocatedCarrierScac.
        /// </remarks>
        public virtual string AssignedTruckRemoteId { get; set; }

        /// <summary>
        /// Readonly - The Id of the device used by the assigned driver
        /// </summary>
        public virtual string AssignedAppId { get; set; }



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
        /// Readonly - The number of vehicles collected
        /// </summary>
        public virtual int VehiclesCollected
        {
            get
            {
                return Vehicles == null
                    ? 0
                    : Vehicles.Count(v => v.Status == VehicleStatus.PickedUp || v.Status == VehicleStatus.Delivered);
            }
        }

        /// <summary>
        /// Readonly - The number of vehicles delievered
        /// </summary>
        public virtual int VehiclesDelivered
        {
            get
            {
                return Vehicles == null
                    ? 0
                    : Vehicles.Count(v => v.Status == VehicleStatus.Delivered);
            }
        }

        /// <summary>
        /// Does the job originate from the vinDispatch marketplace
        /// </summary>
        public virtual bool IsVinDispatchJob { get; set; }


        /// <summary>
        /// Is Job Archived
        /// </summary>
        public virtual bool IsArchived { get; set; }

        /// <summary>
        /// String to display after importing this object
        /// </summary>
        public string ImportDisplayString
        {
            get { return string.Format("{0}:{1}", JobNumber, LoadId); }
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
            CreatedDate = DateTime.UtcNow;
            Customer = new Customer();
            Pickup = new EndPoint();
            Dropoff = new EndPoint();
            Vehicles = new List<Vehicle>();
            History = new List<JobStatusHistoryItem>();
            Documents = new List<Document>();
        }

        /// <summary>
        /// Returns a serial representation of the job in the specified format and schema.
        /// </summary>
        /// <param name="format">Format to serialize to.</param>
        /// <param name="schema">Schema to serialize to.</param>
        /// <param name="forEvent">The WebHookEvent that this message represents.</param>
        /// <param name="timeStamp">Time in UTC that this message was created</param>
        /// <param name="hookId">The id of the hook this that will send this data</param>
        /// <param name="receiverId">Receiver identifier for ICL R41 schemas</param>
        /// <param name="fileName">Filename generated for Pod Url and ICL R41 schemas</param>
        /// <param name="deviceTime">Time in UTC that the associated message was created on the device</param>
        /// <param name="sequenceNumber">Sequential output id for ICL R41</param>
        /// <param name="senderId">Sender identifier for ICL R41</param>
        /// <returns>The serialized object.</returns>
        public string ToHookString(
            MessageFormat format, 
            WebHookSchema schema, 
            WebHookEvent forEvent, 
            DateTime timeStamp, 
            int hookId, 
            short sequenceNumber,
            string senderId,
            string receiverId,
            out string fileName,
            DateTime? deviceTime = null)
        {
            fileName = string.Empty;
           
            switch (schema)
            {
                case WebHookSchema.Cdn:
                    return Serialization.Serialize(this, format);
                case WebHookSchema.Fenkell02:
                    return new Delivery(this, true).ToString();
                case WebHookSchema.Fenkell05:
                    return new Delivery(this, false).ToString();
                case WebHookSchema.TmwV1:
                    return new Stop(this).ToString(forEvent, timeStamp, hookId.ToString(), deviceTime);
                case WebHookSchema.PodUrl:
                {
                    var prefix = (forEvent == WebHookEvent.PickupStop ? "C" : "D");
                    var customerReference = !string.IsNullOrWhiteSpace(this.CustomerReference)
                        ? this.CustomerReference.Trim()
                        : string.Empty;
                    var firstVehicle = this.Vehicles.FirstOrDefault();
                    var firstVehicleVin = firstVehicle != null && !string.IsNullOrWhiteSpace(firstVehicle.Registration)
                        ? firstVehicle.Registration.Trim()
                        : string.Empty;

                    fileName = string.Format("{0}_{1}_{2}.pdf", prefix, customerReference, firstVehicleVin);
                    switch (forEvent)
                    {
                        case WebHookEvent.PickupStop:
                            return this.Pickup.ProofDocUrl + "&m=download";
                        case WebHookEvent.DropoffStop:
                            return this.Dropoff.ProofDocUrl + "&m=download";
                        default:
                            return null;
                    }
                }
                case WebHookSchema.IclR41:
                    var r41 = new R41(this, sequenceNumber, senderId, receiverId);
                    fileName = r41.FileName;
                    return r41.ToString();

                case WebHookSchema.CdxVechicles:
                    throw new ArgumentException(string.Format("Schema {0} is a per shipment schema", schema), "schema");

                case WebHookSchema.Ford:
                    throw new ArgumentException(string.Format("Schema {0} is a per vehicle schema", schema), "schema");
                default:
                    throw new ArgumentException(string.Format("Schema {0} is not a valid WebHookSchema", schema), "schema");
            }
        }

        /// <summary>
        /// Returns a serial representation of the job in the specified format and schema.
        /// </summary>
        /// <param name="vehicleIndex">Index of the vehicle on this job to process</param>
        /// <param name="schema">Schema to serialize to.</param>
        /// <param name="forEvent">The WebHookEvent that this message represents.</param>
        /// <param name="timeStamp">Time in UTC that this message was created</param>
        /// <param name="hookId">The id of the hook this that will send this data</param>
        /// <param name="contractedCarrier">The carrier fleet</param>
        /// <param name="fileName">Filename generated for Pod Url and ICL R41 schemas</param>
        /// <param name="deviceTime">Time in UTC that the associated message was created on the device</param>
        /// <returns>The serialized object.</returns>
        public string ToVehicleHookString(
            int vehicleIndex,
            WebHookSchema schema,
            WebHookEvent forEvent,
            DateTime timeStamp,
            int hookId,
            Fleet contractedCarrier,
            out string fileName,
            DateTime? deviceTime)
        {
            fileName = string.Empty;

            switch (schema)
            {
                case WebHookSchema.Cdn:
                case WebHookSchema.Fenkell02:
                case WebHookSchema.Fenkell05:
                case WebHookSchema.TmwV1:
                case WebHookSchema.PodUrl:
                case WebHookSchema.IclR41:
                    throw new ArgumentException(string.Format("Schema {0} is a per job schema", schema), "schema");

                case WebHookSchema.CdxVechicles:
                case WebHookSchema.CdxStatus:
                    throw new ArgumentException(string.Format("Schema {0} is a per shipment schema", schema), "schema");

                case WebHookSchema.Ford:
                    return new Otr214(this, contractedCarrier).ToString(vehicleIndex, forEvent, timeStamp, hookId.ToString(), deviceTime, false, out fileName);

                default:
                    throw new ArgumentException(string.Format("Schema {0} is not a valid WebHookSchema", schema), "schema");
            }
        }

        /// <summary>
        /// Returns a serial representation of the job in the specified format and schema.
        /// </summary>
        /// <param name="shipment">Shipment to process</param>
        /// <param name="vehicles">List of vehicles from the specified shipment to process</param>
        /// <param name="schema">Schema to serialize to.</param>
        /// <param name="forEvent">The WebHookEvent that this message represents.</param>
        /// <param name="timeStamp">Time in UTC that this message was created</param>
        /// <param name="position"></param>
        /// <param name="fileName">Filename generated for Pod Url and ICL R41 schemas</param>
        /// <param name="deviceTime">Time in UTC that the associated message was created on the device</param>
        /// <returns>The serialized object.</returns>
        public string ToShipmentHookString(
            CdxShipment shipment,
            List<Vehicle> vehicles,
            WebHookSchema schema,
            WebHookEvent forEvent,
            DateTime timeStamp,
            Position position,
            out string fileName,
            DateTime? deviceTime)
        {
            fileName = string.Empty;

            switch (schema)
            {
                case WebHookSchema.Cdn:
                case WebHookSchema.Fenkell02:
                case WebHookSchema.Fenkell05:
                case WebHookSchema.TmwV1:
                case WebHookSchema.PodUrl:
                case WebHookSchema.IclR41:
                    throw new ArgumentException(string.Format("Schema {0} is a per job schema", schema), "schema");

                case WebHookSchema.CdxVechicles:
                    throw new ArgumentException(string.Format("Schema {0} can't be fired from here", schema), "schema");

                case WebHookSchema.CdxStatus:
                    return forEvent == WebHookEvent.PickupStop || forEvent == WebHookEvent.DropoffStop
                        ? new CDXSTOP(shipment, this, vehicles).ToString(forEvent, timeStamp, position, out fileName)
                        : new CDXSTATUS(shipment, this, vehicles).ToString(forEvent, timeStamp, position, out fileName);

                case WebHookSchema.Ford:
                    throw new ArgumentException(string.Format("Schema {0} is a per vehicle schema", schema), "schema");

                default:
                    throw new ArgumentException(string.Format("Schema {0} is not a valid WebHookSchema", schema), "schema");
            }
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