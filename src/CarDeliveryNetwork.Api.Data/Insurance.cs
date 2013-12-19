using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Insurance entity
    /// </summary>
    public class Insurance : IApiEntity
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Expiry
        /// </summary>
        public DateTime? Expiry { get; set; }

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

    /// <summary>
    /// A collection of Car Delivery Network Insurance entities.
    /// </summary>
    [CollectionDataContract]
    public class Insurances : ApiEntityCollectionBase<Insurance, Insurances>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Insurances"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Insurances() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Insurances"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Insurances(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Insurances"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="insurances">The collection of Insurances whose elements are copied to the new collection.</param>
        public Insurances(IEnumerable<Insurance> insurances) : base(insurances) { }
    }
}
