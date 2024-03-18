namespace CarDeliveryNetwork.Api.Data.FreightVerify.Honda_Canada
{
    public class TruckDeparted : MilestoneBase
    {
        public TruckDeparted()
            : base() { }

        public TruckDeparted(Vehicle vehicle, Job job, Fleet contractedCarrier) : base(vehicle, job, contractedCarrier)
        {
            code = "TTOUT";
            ms1LocationCode = job?.Pickup?.Destination?.QuickCode;
        }
    }
}
