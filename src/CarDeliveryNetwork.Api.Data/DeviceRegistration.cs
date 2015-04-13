using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// DeviceRegistration
    /// </summary>
    public class DeviceRegistration : IApiEntity
    {
        /// <summary>
        /// The CMAC for this registration
        /// </summary>
        public virtual string Cmac { get; set; }

        /// <summary>
        /// Gets or sets the device.
        /// </summary>
        public virtual Device TheDevice { get; set; }

        /// <summary>
        /// Indicates that a test job should be sent to the new device
        /// </summary>
        public virtual bool SendTestJob { get; set; }

        /// <summary>
        /// Readonly - Gets the vinDISPATCH API URL for this device registration
        /// </summary>
        public virtual string ApiUrl { get; set; }

        /// <summary>
        /// Readonly - Gets the vinDISPATCH2 API URL for this device registration
        /// </summary>
        public virtual string Api2Url { get; set; }

        /// <summary>
        /// Readonly - Gets the JSON Forwarder URL for this device registration
        /// </summary>
        public virtual string JsonForwarderUrl { get; set; }

        /// <summary>
        /// Readonly - Gets the vinYARD API URL for this device registration
        /// </summary>
        public virtual string VinYardApiUrl { get; set; }

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
        public static DeviceRegistration FromString(string serializedObject)
        {
            return Serialization.Deserialise<DeviceRegistration>(serializedObject, MessageFormat.Json);
        }

        /// <summary>
        /// Reads the JSON or XML document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="format">The format of the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static DeviceRegistration FromString(string serializedObject, MessageFormat format)
        {
            return Serialization.Deserialise<DeviceRegistration>(serializedObject, format);
        }
    }
}
