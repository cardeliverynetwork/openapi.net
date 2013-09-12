
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// An enumeration that specifies the authority of an fleet entity
    /// </summary>
    public enum FleetJobContext
    {
        /// <summary>
        /// The original owner of the job
        /// </summary>
        Originator = 0,

        /// <summary>
        /// The entity that carries out the job
        /// </summary>
        Carrier = 1,
    }
}
