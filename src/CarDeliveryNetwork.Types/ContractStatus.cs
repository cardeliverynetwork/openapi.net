namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of Car Delivery Network contract status
    /// </summary>
    public enum ContractStatus
    {
        // Start State
        /// <summary>
        /// Open
        /// </summary>
        Open = 0,

        // Middling states
        /// <summary>
        /// Declined
        /// </summary>
        Declined = 20,

        /// <summary>
        /// Claimed
        /// </summary>
        Claimed = 30,

        // End States
        /// <summary>
        /// Approved
        /// </summary>
        Approved = 40
    }
}
