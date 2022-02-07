using System.Collections.Generic;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network PriceCheck entity.
    /// </summary>
    public class PriceCheck : ApiEntityBase<PriceCheck>
    {
        /// <summary>
        /// 3rd party reference for this pricecheck
        /// </summary>
        public virtual string Reference { get; set; }

        /// <summary>
        /// CustomerId
        /// </summary>
        public virtual string CustomerId { get; set; }

        /// <summary>
        /// PickupCode (UK Postcode, US ZipCode, etc)
        /// </summary>
        public virtual string PickupCode { get; set; }

        /// <summary>
        /// DeliveryCode (UK Postcode, US ZipCode, etc)
        /// </summary>
        public virtual string DeliveryCode { get; set; }

        /// <summary>
        /// Service required
        /// </summary>
        public virtual ServiceType ServiceRequired { get; set; }

        /// <summary>
        /// Optional - A list of tranships associated with this job.
        /// </summary>
        public virtual List<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Readonly - Returned field estimated mileage
        /// </summary>
        public virtual int EstimatedMileage{ get; set; }

        /// <summary>
        /// Readonly - Returned field estimated journey time in seconds
        /// </summary>
        public virtual int EstimatedTimeSeconds { get; set; }

        /// <summary>
        /// Readonly - Returned list of prices
        /// </summary>
        public virtual List<PriceCheckServiceResult> Prices { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.PriceCheck"/> class
        /// </summary>
        public PriceCheck()
        {
        }
    }
}
