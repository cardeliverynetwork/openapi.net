
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Fleet entity.
    /// </summary>
    public class Fleet : ApiEntityBase<Fleet>
    {
        /// <summary>
        /// The fleet name
        /// </summary>
        public virtual string Name { get; set; }
    }


    /// <summary>
    /// A collection of Car Delivery Network Fleet entities.
    /// </summary>
    [CollectionDataContract]
    public class Fleets : ApiEntityCollectionBase<Fleet, Fleets>, IApiEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Fleets"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Fleets() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Fleets"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Fleets(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Fleets"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="fleets">The collection of fleets whose elements are copied to the new collection.</param>
        public Fleets(List<Fleet> fleets) : base(fleets) { }
    }
}
