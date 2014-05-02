namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of Car Delivery Network WebHook status'.
    /// </summary>
    public enum WebHookEvent
    {
        /// <summary>
        /// WebHook Event for a stop where vehicles were picked up
        /// </summary>
        PickupStop,

        /// <summary>
        /// WebHook Event for a stop where vehicles were dropped off
        /// </summary>
        DropoffStop = 10,

        /// <summary>
        /// WebHook Event for carrier claimed
        /// </summary>
        CarrierClaimApproved = 20
    }
}