using System;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network EndPoint entity.
    /// </summary>
    public class EndPoint : IApiEntity
    {
        /// <summary>
        /// Mandatory - The Destination for this EndPoint
        /// </summary>
        public virtual ContactDetails Destination { get; set; }

        /// <summary>
        /// ReadOnly - The signature collected at this EndPoint
        /// </summary>
        public virtual Signature Signoff { get; set; }

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
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.EndPoint"/> class.
        /// </summary>
        public EndPoint()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this EndPoint.
        /// </summary>
        protected virtual void InitObjects()
        {
            Destination = new ContactDetails();
            Signoff = new Signature();
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
