namespace CarDeliveryNetwork.Api.Data.FreightVerify.Honda_Canada
{
    public class TruckDelivered : MilestoneBase
    {
        public TruckDelivered()
        { }

        public TruckDelivered(Vehicle vehicle, Job job, Fleet contractedCarrier) : base(vehicle, job, contractedCarrier)
        {
            code = "TDELV";
            ms1LocationCode = job?.Dropoff?.Destination?.QuickCode;
        }
            
    }
}
