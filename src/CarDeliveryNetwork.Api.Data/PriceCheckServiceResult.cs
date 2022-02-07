using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network PriceCheckServiceResult entity.
    /// </summary>
    public class PriceCheckServiceResult : ApiEntityBase<PriceCheckServiceResult>
    {
        /// <summary>
        /// Service type
        /// </summary>
        public virtual ServiceType Service { get; set; }

        /// <summary>
        /// LowPrice
        /// </summary>
        public virtual decimal LowPrice { get; set; }

        /// <summary>
        /// MiddlePrice
        /// </summary>
        public virtual decimal MiddlePrice { get; set; }

        /// <summary>
        /// HighPrice
        /// </summary>
        public virtual decimal HighPrice { get; set; }
    }
}