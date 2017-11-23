using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using CarDeliveryNetwork.Types;
using Newtonsoft.Json;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Helper class for object serialisation and deserialisation
    /// </summary>
    public static class Serialization
    {
        /// <summary>
        /// Serialised the specified object in the specified format
        /// </summary>
        /// <param name="o">The object to serialise</param>
        /// <param name="format">The format to serialise into</param>
        /// <returns>The serialised message</returns>
        public static string Serialize(object o, MessageFormat format)
        {
            if (format == MessageFormat.Json)
            {
                return JsonConvert.SerializeObject(o,
                    new JsonSerializerSettings() {DateFormatHandling = DateFormatHandling.MicrosoftDateFormat});
            }
            else
            {
                var serializer = new DataContractSerializer(o.GetType()) as XmlObjectSerializer;

                using (var stream = new MemoryStream())
                using (var reader = new StreamReader(stream))
                {
                    serializer.WriteObject(stream, o);
                    stream.Position = 0;
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Deserialises the specified serialised object into instance of T
        /// </summary>
        /// <typeparam name="T">Type to deserialise to</typeparam>
        /// <param name="serialisedObject">Serialised object</param>
        /// <param name="format">Format that the serialised object</param>
        /// <returns>An instance of type T</returns>
        public static T Deserialise<T>(string serialisedObject, MessageFormat format)
        {
            if (format == MessageFormat.Json)
            {
                return JsonConvert.DeserializeObject<T>(serialisedObject,
                    new JsonSerializerSettings() {DateFormatHandling = DateFormatHandling.MicrosoftDateFormat});
            }
            else
            {
                var serializer = new DataContractSerializer(typeof(T)) as XmlObjectSerializer;
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serialisedObject)))
                    return (T)serializer.ReadObject(stream);
            }
        }
    }
}
