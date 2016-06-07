
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// CDN Data schemes
    /// </summary>
    public enum WebHookSchema
    {
        /// <summary>
        /// The CDN Schema
        /// </summary>
        Cdn = 0,

        /// <summary>
        /// The Fenkell02 Schema
        /// </summary>
        Fenkell02 = 5,

        /// <summary>
        /// The Fenkell05 Schema
        /// </summary>
        Fenkell05 = 10,

        /// <summary>
        /// The TMW V1 Schema
        /// </summary>
        TmwV1 = 20,

        /// <summary>
        /// Email
        /// </summary>
        Email = 30,

        /// <summary>
        /// PoD URL
        /// </summary>
        PodUrl = 40
    }
}
