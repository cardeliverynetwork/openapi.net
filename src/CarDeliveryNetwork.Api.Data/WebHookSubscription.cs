using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network WebHookSubscription entity.
    /// </summary>
    public class WebHookSubscription : ApiEntityBase<WebHookSubscription>
    {
        /// <summary>
        /// HookEventType
        /// </summary>
        public virtual WebHookEvent HookEventType { get; set; }

        /// <summary>
        /// HttpMethod 
        /// </summary>
        public virtual string HttpMethod { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        /// Headers
        /// </summary>
        public virtual string Headers { get; set; }

        /// <summary>
        /// Schama of this hook subscription's data
        /// </summary>
        public virtual WebHookSchema DataSchema { get; set; }

        /// <summary>
        /// DataFormat 'json' or 'xml'
        /// </summary>
        public virtual string DataFormat { get; set; }

        /// <summary>
        /// Paramaters to be added to the URL used to fetch the data for this hook
        /// </summary>
        public virtual string FetchParams { get; set; }

        /// <summary>
        /// Identifier of the sender
        /// </summary>
        public virtual string SenderId { get; set; }

        /// <summary>
        /// Only applies to data where this is the shipper
        /// </summary>
        public virtual int? ApplicableShipperId { get; set; }

        /// <summary>
        /// Identifier of the receiver
        /// </summary>
        public virtual string ReceiverId { get; set; }
    }

    /// <summary>
    /// A collection of Car Delivery Network WebHookSubscription entities.
    /// </summary>
    [CollectionDataContract]
    public class WebHookSubscriptions : ApiEntityCollectionBase<WebHookSubscription, WebHookSubscriptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.WebHookSubscriptions"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public WebHookSubscriptions() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.WebHookSubscriptions"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public WebHookSubscriptions(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.WebHookSubscriptions"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="webHookSubscriptions">The collection of WebHookSubscriptions whose elements are copied to the new collection.</param>
        public WebHookSubscriptions(IEnumerable<WebHookSubscription> webHookSubscriptions) : base(webHookSubscriptions) { }
    }
}
