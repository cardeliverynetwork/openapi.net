using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Bid entity.
    /// </summary>
    public class Bid : ApiEntityBase<Bid>
    {
        /// <summary>
        /// BidNumber
        /// </summary>
        public virtual string BidNumber { get; set; }

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
        public virtual DateTime CollectionDate { get; set; }

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
        /// IsArchived
        /// </summary>
        public virtual bool IsArchived { get; set; }

        /// <summary>
        /// IsWinner
        /// </summary>
        public virtual bool IsWinner { get; set; }

        /// <summary>
        /// IsFuelInclusive
        /// </summary>
        public virtual bool IsFuelInclusive { get; set; }

        /// <summary>
        /// IsLinkJob
        /// </summary>
        public virtual bool IsLinkJob { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Fleet
        /// </summary>
        public virtual Fleet Fleet { get; set; }

        /// <summary>
        /// AcceptUrl
        /// </summary>
        public virtual string AcceptUrl { get; set; }

        /// <summary>
        /// IsExpired
        /// </summary>
        public bool IsExpired
        {
            get { return DateTime.UtcNow > ValidUntil; }
            set { /* for serialisation only */ }
        }

        /// <summary>
        /// ServiceOfferedFormat
        /// </summary>
        public string ServiceOfferedFormat
        {
            get { return ServiceOffered.ToString(); }
            set { /* for serialisation only */ }
        }

        /// <summary>
        /// IsExpired
        /// </summary>
        public string AmountFormat
        {
            get { return string.Format("{0:C}", Amount / 100); }
            set { /* for serialisation only */ }
        }

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
            User = new User();
            Fleet = new Fleet();
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network Bid entities.
    /// </summary>
    [CollectionDataContract]
    public class Bids : ApiEntityCollectionBase<Bid, Bids>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Bids"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Bids() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Bids"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Bids(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Bids"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="bids">The collection of Bids whose elements are copied to the new collection.</param>
        public Bids(IEnumerable<Bid> bids) : base(bids) { }
    }
}
