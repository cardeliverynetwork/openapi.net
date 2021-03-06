﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network JobHistoryItem entity.
    /// </summary>
    public class JobStatusHistoryItem : IApiEntity
    {
        /// <summary>
        /// Changed
        /// </summary>
        public virtual DateTime Changed { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public virtual JobStatus Status { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public virtual string StatusFriendly { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        public virtual string Notes { get; set; }

        /// <summary>
        /// Gets or sets the position at status.
        /// </summary>
        public virtual Position PositionAtStatus { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.JobStatusHistoryItem"/> class
        /// </summary>
        public JobStatusHistoryItem()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this JobStatusHistoryItem.
        /// </summary>
        protected virtual void InitObjects()
        {
            PositionAtStatus = new Position();
        }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return ToString(MessageFormat.Json);
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
    }

    /// <summary>
    /// A collection of Car Delivery Network JobHistoryItem entities.
    /// </summary>
    [CollectionDataContract]
    public class JobStatusHistoryItems : ApiEntityCollectionBase<JobStatusHistoryItem, JobStatusHistoryItems>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.JobStatusHistoryItems"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public JobStatusHistoryItems() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.JobStatusHistoryItems"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public JobStatusHistoryItems(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.JobStatusHistoryItems"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="jobHistoryItems">The collection of JobHistoryItems whose elements are copied to the new collection.</param>
        public JobStatusHistoryItems(IEnumerable<JobStatusHistoryItem> jobHistoryItems) : base(jobHistoryItems) { }
    }
}
