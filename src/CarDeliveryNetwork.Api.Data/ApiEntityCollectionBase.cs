using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A collection of Car Delivery Network <see cref="CarDeliveryNetwork.Api.Data.Job"/> entities.
    /// </summary>
    [CollectionDataContract]
    public class ApiEntityCollectionBase<T, C> : List<T>, IApiEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.ApiEntityCollectionBase&lt;T, C&gt;"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public ApiEntityCollectionBase() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.ApiEntityCollectionBase&lt;T, C&gt;"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public ApiEntityCollectionBase(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.ApiEntityCollectionBase&lt;T, C&gt;"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="entities">The collection of jobs whose elements are copied to the new collection.</param>
        public ApiEntityCollectionBase(IEnumerable<T> entities) : base(entities) { }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return this.ToString(MessageFormat.Json);
        }

        /// <summary>
        /// Returns a serial representation of the object in the specified format.
        /// </summary>
        /// <param name="format">Format to serialize to.</param>
        /// <returns>The serialized object.</returns>
        public string ToString(MessageFormat format)
        {
            return Serialization.Serialize(this, format);
        }

        /// <summary>
        /// Reads the JSON document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The JSON serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static C FromString(string serializedObject)
        {
            return FromString(serializedObject, MessageFormat.Json);
        }

        /// <summary>
        /// Reads the JSON or XML document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The serialized object.</param>
        /// <param name="format">The format of the serialized object.</param>
        /// <returns>The deserialized object.</returns>
        public static C FromString(string serializedObject, MessageFormat format)
        {
            return Serialization.Deserialise<C>(serializedObject, format);
        }
    }
}
