using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Journey entity.
    /// </summary>
    public class Journey : ApiEntityBase<Journey>
    {
        /// <summary>
        /// Part
        /// </summary>
        public virtual int Part { get; set; }

        /// <summary>
        /// AssignedToUser
        /// </summary>
        public virtual User AssignedToUser { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public virtual JobStatus Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Journey"/> class
        /// </summary>
        public Journey()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this Journey.
        /// </summary>
        protected virtual void InitObjects()
        {
            AssignedToUser = new User();
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network Journey entities.
    /// </summary>
    [CollectionDataContract]
    public class Journeys : ApiEntityCollectionBase<Journey, Journeys>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Journeys"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Journeys() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Journeys"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Journeys(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Journeys"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="journeys">The collection of Journeys whose elements are copied to the new collection.</param>
        public Journeys(IEnumerable<Journey> journeys) : base(journeys) { }
    }
}
