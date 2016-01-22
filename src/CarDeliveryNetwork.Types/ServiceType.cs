
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// An enumeration that specifies the service type of a Car Delivery Network job.
    /// </summary>
    public enum ServiceType
    {
        /// <summary>
        /// A job where the vehicle is moved on a transporter.
        /// </summary>
        Transported,

        /// <summary>
        /// A job performed by a professional driver.
        /// </summary>
        Driven,

        /// <summary>
        /// A job that can either be driven or transported.
        /// </summary>
        Either,

        /// <summary>
        /// Service type will be decided based on other factors. SHOULD NEVER BE SAVED AT THIS TYPE.
        /// </summary>
        Auto
    }
}
