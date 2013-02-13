
namespace CarDeliveryNetwork.Types.Interfaces
{
    /// <summary>
    /// Interface describing a Car Delivery Network ownable entity
    /// </summary>
    public interface IOwnable
    {
        /// <summary>
        /// The owner of this IOwnable object
        /// </summary>
        IFleet Owner { get; }
    }
}
