namespace CarDeliveryNetwork.Api.Data.FreightVerify.Honda_Canada
{
    public class TruckDeparted : MilestoneBase
    {
        public TruckDeparted()
            : base() { }

        public TruckDeparted(Vehicle vehicle, Job job, Fleet contractedCarrier, string senderId) : base(vehicle, job, contractedCarrier, senderId)
        {
            code = "TTOUT";
            ms1LocationCode = job?.Pickup?.Destination?.QuickCode;
        }
    }
}
