using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Alert
    /// </summary>
    public class Alert : ApiEntityBase<Alert>
    {
        /// <summary>
        /// The code for this alert
        /// </summary>
        public virtual int AlertCode { get; set; }

        /// <summary>
        /// A description of the alert
        /// </summary>
        public virtual string Description { get; set; }
    }

    /// <summary>
    /// A collection of Car Delivery Network Alert entities.
    /// </summary>
    [CollectionDataContract]
    public class Alerts : ApiEntityCollectionBase<Alert, Alerts>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Alerts"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Alerts() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Alerts"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Alerts(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Alerts"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="alerts">The collection of Alerts whose elements are copied to the new collection.</param>
        public Alerts(IEnumerable<Alert> alerts) : base(alerts) { }
    }
}
