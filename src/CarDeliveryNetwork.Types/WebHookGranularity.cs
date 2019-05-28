namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of Car Delivery Network WebHook granularity'.
    /// </summary>
    public enum WebHookGranularity
    {
        /// <summary>
        /// Hook is sent on a per job basis
        /// </summary>
        Job,

        /// <summary>
        /// Hook is sent on a per vehicle basis
        /// </summary>
        Vehicle = 10,

        /// <summary>
        /// Hook is sent on a per shipment basis
        /// </summary>
        Shipment = 20,
    }
}