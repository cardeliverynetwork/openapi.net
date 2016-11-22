namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Possible fleet credit levels
    /// </summary>
    public enum CreditStateResult
    {
        /// <summary>
        /// Good level
        /// </summary>
        Good,
        /// <summary>
        /// Low level
        /// </summary>
        Warn,
        /// <summary>
        /// No credit
        /// </summary>
        Out,
    }
}
