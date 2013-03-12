
namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Vehicle entity. You must specify a VIN or Registration. 
    /// </summary>
    /// <remarks> 
	/// If you do not want to specify an actual VIN you should specify a placeholder (1,2,3 etc) 
	/// of less than 6 characters and the system will force the capture of a valid VIN
    /// </remarks>	
    public class Vehicle : ApiEntityBase<Vehicle>
    {
        /// <summary>
        /// Optional (10) - The vehicle registration number or year manufactured.
        /// </summary>
        public virtual string Registration { get; set; }

        /// <summary>
        /// Optional (17) - The vehicle identification number. A valid VIN is 6-17 characters.
        /// </summary>
        public virtual string Vin { get; set; }

        /// <summary>
        /// Optional (20) - The vehicle make/manufacturer.
        /// </summary>
        public virtual string Make { get; set; }

        /// <summary>
        /// Optional (20) - The vehicle model.
        /// </summary>
        public virtual string Model { get; set; }

        /// <summary>
        /// Optional (50) - The vehicle variant, type or sub-model.
        /// </summary>
        public virtual string Variant { get; set; }

        /// <summary>
        /// Optional (50) - The vehicle's locatoin or bay number
        /// </summary>
        public virtual string Location { get; set; }

        /// <summary>
        /// Optional (50) - A movement number specific to this vehicle
        /// </summary>
        public virtual string MovementNumber { get; set; }

        /// <summary>
        /// Optional (255) - Notes relating to this vehicle.
        /// </summary>
        public virtual string Notes { get; set; }
    }
}
