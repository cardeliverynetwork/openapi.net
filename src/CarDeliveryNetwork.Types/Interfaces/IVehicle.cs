
namespace CarDeliveryNetwork.Types.Interfaces
{
    /// <summary>
    /// Interface describing a Car Delivery Network vehicle.
    /// </summary>
    public interface IVehicle
    {
        /// <summary>
        /// The vehicle registration number or year.
        /// </summary>
        string Registration { get; set; }

        /// <summary>
        /// The vehicle identification number.
        /// </summary>
        string Vin { get; set; }

        /// <summary>
        /// The vehicle make/manufacturer.
        /// </summary>
        string Make { get; set; }

        /// <summary>
        /// The vehicle model.
        /// </summary>
        string Model { get; set; }

        /// <summary>
        /// The vehicle variant, type or sub-model.
        /// </summary>
        string Variant { get; set; }

        /// <summary>
        /// Notes relating to this vehicle.
        /// </summary>
        string Notes { get; set; }

        /// <summary>
        /// The SONO number
        /// </summary>
        string Sono { get; set; }

        /// <summary>
        ///  The OEM Id
        /// </summary>
        string OemId { get; set; }
    }
}
