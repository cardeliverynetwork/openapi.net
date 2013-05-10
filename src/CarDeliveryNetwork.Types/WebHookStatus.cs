namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of Car Delivery Network WebHook status'.
    /// </summary>
    public enum WebHookStatus
    {
        /// <summary>
        /// Hook is sent and awaiting a response
        /// </summary>
        Sent,

        /// <summary>
        /// Hook is retrying
        /// </summary>
        Retrying = 10,

        /// <summary>
        /// Hook succeeded
        /// </summary>
        Succeeded = 20,

        /// <summary>
        /// Hook failed
        /// </summary>
        Failed = 30,
    }
}