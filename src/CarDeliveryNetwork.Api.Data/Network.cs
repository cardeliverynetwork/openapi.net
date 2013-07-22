using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Carrier Network entity.
    /// </summary>
    public class Network : ApiEntityBase<Network>
    {
        /// <summary>
        /// The network name
        /// </summary>
        public virtual string Name { get; set; }
    }

    /// <summary>
    /// A collection of Car Delivery Network Network entities.
    /// </summary>
    [CollectionDataContract]
    public class Networks : ApiEntityCollectionBase<Network, Networks>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Networks"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Networks() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Networks"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public Networks(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Networks"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="networks">The collection of networks whose elements are copied to the new collection.</param>
        public Networks(IEnumerable<Network> networks) : base(networks) { }
    }
}
