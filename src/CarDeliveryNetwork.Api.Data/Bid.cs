using System;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Bid entity.
    /// </summary>
    public class Bid : ApiEntityBase<Bid>
    {
        /// <summary>
        /// The id of the request associated with this bid
        /// </summary>
        public virtual int RequestId { get; set; }

        /// <summary>
        /// BidPlaced
        /// </summary>
        public virtual DateTime BidPlaced { get; set; }

        /// <summary>
        /// ValidUntil
        /// </summary>
        public virtual DateTime ValidUntil { get; set; }

        /// <summary>
        /// CollectionDate
        /// </summary>
        public virtual DateTime PickupDate { get; set; }

        /// <summary>
        /// DeliveryDate
        /// </summary>
        public virtual DateTime DeliveryDate { get; set; }

        /// <summary>
        /// ServiceOffered
        /// </summary>
        public virtual ServiceType ServiceOffered { get; set; }

        /// <summary>
        /// Amount
        /// </summary>
        public virtual int Amount { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public virtual string Notes { get; set; }

        /// <summary>
        /// IsClaimedSubjectToContract
        /// </summary>
        public virtual bool IsClaimedSubjectToContract { get; set; }

        /// <summary>
        /// Fleet
        /// </summary>
        public virtual Fleet Fleet { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Bid"/> class
        /// </summary>
        public Bid()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this Bid.
        /// </summary>
        protected virtual void InitObjects()
        {
            Fleet = new Fleet();
        }
    }
}
