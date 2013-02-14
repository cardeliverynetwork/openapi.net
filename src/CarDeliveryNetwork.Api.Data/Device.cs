﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Device entity.
    /// </summary>
    public class Device : ApiEntityBase<Device>, IApiEntity
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Alias
        /// </summary>
        public virtual string Alias { get; set; }

        /// <summary>
        /// LoggedInUser
        /// </summary>
        public virtual string LoggedInUser { get; set; }

        /// <summary>
        /// LastSeen
        /// </summary>
        public virtual DateTime? LastSeen { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public virtual double? Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public virtual double? Longitude { get; set; }
    }

    /// <summary>
    /// A collection of Car Delivery Network Device entities.
    /// </summary>
    [CollectionDataContract]
    public class Devices : ApiEntityCollectionBase<Device, Devices>, IApiEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Devices"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Devices() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Devices"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Devices(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Devices"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="devices">The collection of devices whose elements are copied to the new collection.</param>
        public Devices(List<Device> devices) : base(devices) { }
    }
}