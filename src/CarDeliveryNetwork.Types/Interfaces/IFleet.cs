

namespace CarDeliveryNetwork.Types.Interfaces
{
    /// <summary>
    /// Interface describing a Car Delivery Network user.
    /// </summary>
    public interface IFleet : IOwnable
    {
        /// <summary>
        /// Id
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// SCAC
        /// </summary>
        string Scac { get; set; }

        /// <summary>
        /// DisplayName
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// CMAC
        /// </summary>
        string Cmac { get; }

        /// <summary>
        /// Fleet can do transported work
        /// </summary>
        bool CanTransport { get; }

        /// <summary>
        /// Fleet can do driven work
        /// </summary>
        bool CanDrive { get; }

        /// <summary>
        /// Fleet is primarily a broker
        /// </summary>
        bool IsBroker { get; }

        /// <summary>
        /// The fleet's contact details
        /// </summary>
        IContactDetails Contact { get; }

        /// <summary>
        /// MenuProfile
        /// </summary>
        FleetMenuProfile MenuProfile { get; set; }
    }
}
