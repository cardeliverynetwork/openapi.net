using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Defines methods for all Car Delivery Network data entities.
    /// </summary>
    public interface IApiEntity
    {
        /// <summary>
        /// Returns a serial representation of the object in the specified format.
        /// </summary>
        /// <param name="format">Format to serialize to.</param>
        /// <returns>The serialized object.</returns>
        string ToString(MessageFormat format);
    }
}
