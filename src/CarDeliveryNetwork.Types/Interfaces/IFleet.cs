

namespace CarDeliveryNetwork.Types.Interfaces
{
    /// <summary>
    /// Interface describing a Car Delivery Network user.
    /// </summary>
    public interface IFleet
    {
        /// <summary>
        /// Id
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// DisplayName
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// MenuProfile
        /// </summary>
        FleetMenuProfile MenuProfile { get; set; }
    }
}
