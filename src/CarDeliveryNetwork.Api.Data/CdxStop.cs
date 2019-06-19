using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    public class CdxStop : CdxMessage
    {
        public EndPointType StopType { get; set; }
        public short NumberOfVehicles { get; set; }
        public string ProofDocUrl {get; set; }
        public string EmailList { get; set; }
        public int SignatureType { get; set; }
        public string SignedBy { get; set; }
        public string NotSignedReasons { get; set; }
        public string SignOffComment { get; set; }
        public string Signature { get; set; }

        /// <summary>
        /// Reads the JSON or XML document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="format">The format of the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public new static CdxStop FromString(string serializedObject, MessageFormat format)
        {
            return Serialization.Deserialise<CdxStop>(serializedObject, format, KnownTypes);
        }

    }
}
