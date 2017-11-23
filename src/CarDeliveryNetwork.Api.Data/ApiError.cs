using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ApiError
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "responseStatus")]
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Reads the JSON document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The JSON serialized object.</param>
        /// <param name="format">Format to deserialize from.</param>
        /// <returns>The deserialized object.</returns>
        public static ApiError FromString(string serializedObject, MessageFormat format = MessageFormat.Json)
        {
            return Serialization.Deserialise<ApiError>(serializedObject, format);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ResponseStatus
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "stackTrace")]
        public string StackTrace { get; set; }
    }
}