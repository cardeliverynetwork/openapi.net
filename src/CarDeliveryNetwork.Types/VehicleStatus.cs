
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of Car Delivery Network vehicle status'.
    /// </summary>
    public enum VehicleStatus
    {
        /// <summary>
        /// Vehicle is pre-loading.  The job has not started yet 
        /// or the driver has not yet collected the vehicle
        /// </summary>
        PrePickup = 0,

        /// <summary>
        /// Vehicle is picked up
        /// </summary>
        PickedUp = 10,

        /// <summary>
        /// Vehicle was not picked up.  There was a problem that 
        /// prevented the vehicle from being picked up
        /// </summary>
        NotPickedUp = 20,

        /// <summary>
        /// Vehicle is delivered
        /// </summary>
        Delivered = 30,

        /// <summary>
        /// (Currently unused) Vehicle was not delivered.  There was a problem that 
        /// prevented the vehicle from being delivered
        /// </summary>
        NotDelivered = 40,
    };
}