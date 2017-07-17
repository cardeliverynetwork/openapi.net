using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Alert
    /// </summary>
    public class Alert : ApiEntityBase<Alert>
    {
        /// <summary>cd
        /// The code for this alert
        /// </summary>
        public virtual int AlertCode { get; set; }

        /// <summary>
        /// A description of the alert
        /// </summary>
        public virtual string MessageBody { get; set; }

        /// <summary>
        /// An associated JobId of the alert
        /// </summary>
        public virtual int AssociatedJobId { get; set; }

        /// <summary>
        /// An associated Fleet Id of the alert
        /// </summary>
        public virtual int? FleetId { get; set; }

        /// <summary>
        /// A vindispatch alert Type 
        /// </summary>
        public virtual int Type { get; set; }

        /// <summary>
        /// A date of alert creation
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// A fleet from whom alert is created
        /// </summary>
        public virtual string FleetFrom { get; set; }

        /// <summary>
        /// An user from whom alert is  created
        /// </summary>
        public virtual string UserFrom { get; set; }


        /// <summary>
        /// An fleet to whom alert is  sent
        /// </summary>
        public virtual string FleetTo { get; set; }

        /// <summary>
        /// An user to whom alert is  sent
        /// </summary>
        public virtual string UserTo { get; set; }
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
        public Alerts()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Alerts"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Alerts(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Alerts"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="alerts">The collection of Alerts whose elements are copied to the new collection.</param>
        public Alerts(IEnumerable<Alert> alerts) : base(alerts)
        {
        }
    }
}
