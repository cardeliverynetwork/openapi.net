using System;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network EndPoint entity.
    /// </summary>
    public class Tranship : IApiEntity
    {
        /// <summary>
        /// Mandatory - The Destination for this Tranship
        /// </summary>
        public virtual ContactDetails Destination { get; set; }

        /// <summary>
        /// Mandatory - The Number for this Tranship
        /// </summary>
        public virtual int TranshipNumber { get; set; }

        /// <summary>
        /// Mandatory - The requested date for this EndPoint.
        /// </summary>
        public virtual DateTime? RequestedDate { get; set; }

        /// <summary>
        /// Readonly - The date the job is scheduled by the carrier
        /// </summary>
        public virtual DateTime? ScheduledDate { get; set; }

        /// <summary>
        /// Readonly - The vinYARD gate release code generated at this endpoint
        /// </summary>
        public virtual string GateOutCode { get; set; }

        /// <summary>
        /// Readonly - The driver estimated ETA at this endpoint
        /// </summary>
        public virtual DateTime? Eta { get; set; }

        /// <summary>
        /// Mandatory - When true, indicates that RequestedDate is an exact date.  Pickup/Delivery must be ON this date.
        /// When false, indicates that RequestedDate is not exact.  Pickup/Delivery should be FROM/UP TO this date
        /// </summary>
        public virtual bool RequestedDateIsExact { get; set; }

        /// <summary>
        /// ReadOnly - The URL of the proof document associated with this EndPoint ()
        /// </summary>
        public virtual string ProofDocUrl { get; set; }

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
        /// Optional (20) - An id representing the trip this tranship is part of
        /// </summary>
        public virtual string TripId { get; set; }

        /// <summary>
        /// Optional - The RemoteId of the assigned driver
        /// </summary>
        /// <remarks>
        /// The assigned driver must be of the carrier specified in AllocatedCarrierScac.
        /// </remarks>
        public virtual string AssignedDriverRemoteId { get; set; }

        /// <summary>
        /// Optional - The RemoteId of the assigned truck
        /// </summary>
        /// <remarks>
        /// The assigned truck must be of the carrier specified in AllocatedCarrierScac.
        /// </remarks>
        public virtual string AssignedTruckRemoteId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Tranship"/> class.
        /// </summary>
        public Tranship()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this EndPoint.
        /// </summary>
        protected virtual void InitObjects()
        {
            Destination = new ContactDetails();
        }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return ToString(Types.MessageFormat.Json);
        }

        /// <summary>
        /// Returns a serial representation of the object in the specified format.
        /// </summary>
        /// <param name="format">Format to serialize to.</param>
        /// <returns>The serialized object.</returns>
        public string ToString(Types.MessageFormat format)
        {
            return Serialization.Serialize(this, format);
        }
    }
}
