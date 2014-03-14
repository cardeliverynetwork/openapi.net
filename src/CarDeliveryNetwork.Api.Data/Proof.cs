
using CarDeliveryNetwork.Types;
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
        /// The Url to this Proof
        /// </summary>
        public virtual string Url { get; set;}

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

        /// <summary>
        /// Reads the JSON document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The JSON serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static Proof FromString(string serializedObject)
        {
            return Serialization.Deserialise<Proof>(serializedObject, MessageFormat.Json);
        }

        /// <summary>
        /// Reads the JSON or XML document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="format">The format of the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static Proof FromString(string serializedObject, MessageFormat format)
        {
            return Serialization.Deserialise<Proof>(serializedObject, format);
        }
    }
}
