using System;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network WebHook entity.
    /// </summary>
    public class WebHook : ApiEntityBase<WebHook>
    {
        /// <summary>
        /// Date the hook was first sent
        /// </summary>
        public virtual DateTime? FirstSent { get; set; }

        /// <summary>
        /// Date the current status was applied
        /// </summary>
        public virtual DateTime? StatusTime { get; set; }

        /// <summary>
        /// The current status
        /// </summary>
        public virtual WebHookStatus Status { get; set; }

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
        /// Schama of this hook's data
        /// </summary>
        public virtual WebHookSchema DataSchema { get; set; }

        /// <summary>
        /// DataFormat 'json' or 'xml'
        /// </summary>
        public virtual string DataFormat { get; set; }

        /// <summary>
        /// RequestBody
        /// </summary>
        public virtual string RequestBody { get; set; }

        /// <summary>
        /// LastError
        /// </summary>
        public virtual string LastError { get; set; }
    }
}
