
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// The recency of the associated heartbeat
    /// </summary>
    public enum HeartbeatRecency
    {
        /// <summary>
        /// Within 1 hour
        /// </summary>
        Hrs1,

        /// <summary>
        /// Within 6 hours
        /// </summary>
        Hrs6,

        /// <summary>
        /// Within 24 hours
        /// </summary>
        Hrs24,

        /// <summary>
        /// More than 24 hours
        /// </summary>
        Hrs24Plus,

        /// <summary>
        /// Never heard from
        /// </summary>
        Never
    }
}