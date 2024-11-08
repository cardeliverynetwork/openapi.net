﻿using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

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
        /// <param name="knownTypes">An System.Collections.Generic.IEnumerable`1 of System.Type that contains the types that may be present in the object graph</param>
        /// <returns>The serialised message</returns>
        public static string Serialize(object o, MessageFormat format, IEnumerable<Type> knownTypes = null)
        {
            var serializer = format == MessageFormat.Json
                ? new DataContractJsonSerializer(o.GetType(), knownTypes) as XmlObjectSerializer
                : new DataContractSerializer(o.GetType(), knownTypes) as XmlObjectSerializer;

            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                serializer.WriteObject(stream, o);
                stream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Serialised the specified object in the specified format
        /// </summary>
        /// <param name="o">The object to serialise</param>
        /// <param name="settings">Settings for the serialiser to use</param>
        /// <returns>The serialised message</returns>
        public static string SerializeAsJson(object o, DataContractJsonSerializerSettings settings = null)
        {
            var serializer = new DataContractJsonSerializer(o.GetType(), settings) as XmlObjectSerializer;

            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                serializer.WriteObject(stream, o);
                stream.Position = 0;
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Deserialises the specified serialised object into instance of T
        /// </summary>
        /// <typeparam name="T">Type to deserialise to</typeparam>
        /// <param name="serialisedObject">Serialised object</param>
        /// <param name="format">Format that the serialised object</param>
        /// <param name="knownTypes">An System.Collections.Generic.IEnumerable`1 of System.Type that contains the types that may be present in the object graph</param>
        /// <param name="output">on success, the deserialised object</param>
        /// <returns>true, on successful deserialisation</returns>
        public static bool TryDeserialise<T>(
            string serialisedObject, 
            MessageFormat format, 
            IEnumerable<Type> knownTypes, 
            out T output) where T : class
        {
            try
            {
                output = Deserialise<T>(serialisedObject, format, knownTypes);
            }
            catch (Exception)
            {

                output = null;
            }

            return output != null;
        }

        /// <summary>
        /// Deserialises the specified serialised object into instance of T
        /// </summary>
        /// <typeparam name="T">Type to deserialise to</typeparam>
        /// <param name="serialisedObject">Serialised object</param>
        /// <param name="format">Format that the serialised object</param>
        /// <param name="knownTypes">An System.Collections.Generic.IEnumerable`1 of System.Type that contains the types that may be present in the object graph</param>
        /// <returns>An instance of type T</returns>
        public static T Deserialise<T>(string serialisedObject, MessageFormat format, IEnumerable<Type> knownTypes = null)
        {
            // Horrible hack in lieu of a serialiser that can do Camel and Pascal
            serialisedObject = serialisedObject.Replace("\"id\":", "\"Id\":")
                                               .Replace("\"jobNumber\":", "\"JobNumber\":")
                                               .Replace("\"allocatedCarrierId\":", "\"AllocatedCarrierId\":")
                                               .Replace("\"isVinDispatchJob\":", "\"IsVinDispatchJob\":")
                                               .Replace("\"scac\":", "\"Scac\":");

            var serializer = format == MessageFormat.Json
                ? new DataContractJsonSerializer(typeof(T), knownTypes) as XmlObjectSerializer
                : new DataContractSerializer(typeof(T), knownTypes) as XmlObjectSerializer;

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serialisedObject)))
                return (T)serializer.ReadObject(stream);
        }
    }
}
