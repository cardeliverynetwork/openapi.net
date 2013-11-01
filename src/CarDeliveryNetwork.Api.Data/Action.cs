
namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Describes an action to be carried out on a resource.
    /// </summary>
    public class Action : IApiEntity
    {
        /// <summary>
        /// Required: The name of the action to carry out on the associated resource
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Required for certain actions: The Id of a resource associated with the specified action
        /// </summary>
        public string AssociatedId { get; set; }

        /// <summary>
        /// Required for certain actions: A note associated with the specified action
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return ToString(Types.MessageFormat.Json);
        }

        /// <summary>
        /// Returns a serial representation of the object in the specified format.
        /// </summary>
        /// <param name="format">Format to serialize to.</param>
        /// <returns>The serialized object.</returns>
        public string ToString(Types.MessageFormat format)
        {
            return Serialization.Serialize(this, format);
        }
    }
}
