using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    public class CdxStatus : CdxMessage
    {
        /// <summary>
        /// Reads the JSON or XML document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="format">The format of the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public new static CdxStatus FromString(string serializedObject, MessageFormat format)
        {
            return Serialization.Deserialise<CdxStatus>(serializedObject, format, KnownTypes);
        }
    }
}
