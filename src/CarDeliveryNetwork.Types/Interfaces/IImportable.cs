

namespace CarDeliveryNetwork.Types.Interfaces
{
    /// <summary>
    /// Interface describing a Car Delivery Network importable entity
    /// </summary>
    public interface IImportable
    {
        /// <summary>
        /// String that is displayed in the context of a just-imported IImportable
        /// </summary>
        string ImportDisplayString { get; }
    }
}
