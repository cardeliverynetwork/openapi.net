
namespace CarDeliveryNetwork.Api.Data
{
    public class Proof : IApiEntity
    {
        /// <summary>
        /// Gets or sets the job.
        /// </summary>
        /// <value>
        /// The job.
        /// </value>
        public virtual Job TheJob { get; set; }

        /// <summary>
        /// Gets or sets the shipper.
        /// </summary>
        /// <value>
        /// The shipper.
        /// </value>
        public virtual Fleet Shipper { get; set; }

        /// <summary>
        /// Gets or sets the first supplier.
        /// </summary>
        /// <value>
        /// The first supplier.
        /// </value>
        public virtual Fleet FirstSupplier { get; set; }

        /// <summary>
        /// Gets or sets the proof owner.
        /// </summary>
        /// <value>
        /// The proof owner.
        /// </value>
        public virtual Fleet ProofOwner { get; set; }

        /// <summary>
        /// Gets or sets the carrier.
        /// </summary>
        /// <value>
        /// The carrier.
        /// </value>
        public virtual Fleet Carrier { get; set; }

        /// <summary>
        /// Gets or sets the driver.
        /// </summary>
        /// <value>
        /// The driver.
        /// </value>
        public virtual string Driver { get; set; }

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
