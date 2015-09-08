
namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Payment entity.
    /// </summary>
    public class Payment : ApiEntityBase<Payment>
    {
        /// <summary>
        /// InvoiceNumber
        /// </summary>
        public virtual string InvoiceNumber { get; set; }

        /// <summary>
        /// InvoiceNumber2
        /// </summary>
        public virtual string InvoiceNumber2 { get; set; }
    }
}
