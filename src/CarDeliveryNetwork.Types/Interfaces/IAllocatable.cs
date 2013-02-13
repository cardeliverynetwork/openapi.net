
namespace CarDeliveryNetwork.Types.Interfaces
{
    /// <summary>
    /// Describes a Car Delivery Network entity that a job can be allocated to (Fleet, Network, etc.)
    /// </summary>
    public interface IAllocatable
    {
        /// <summary>
        /// The name of this allocatable entity
        /// </summary>
        string Name { get; }
    }
}
