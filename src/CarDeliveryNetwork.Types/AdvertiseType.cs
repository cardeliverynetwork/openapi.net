namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of Car Delivery Network advertise type
    /// </summary>
    public enum AdvertiseType
    {
        /// <summary>
        /// Request carriers to provide quotes
        /// </summary>
        Quote = 0,

        /// <summary>
        /// Allow carriers to claim at a fixed price
        /// </summary>
        ClaimNow = 1,

        /// <summary>
        /// Request quotes and allow carriers to claim at a fixed price
        /// </summary>
        Both = 2
    }
}
