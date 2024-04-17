namespace CarDeliveryNetwork.Api.Data.FreightVerify.Honda_Canada
{
    public class TruckDelivered : MilestoneBase
    {
        public TruckDelivered()
        { }

        public TruckDelivered(Vehicle vehicle, Job job, Fleet contractedCarrier, string senderId) : base(vehicle, job, contractedCarrier, senderId)
        {
            code = "TDELV";
            ms1LocationCode = job?.Dropoff?.Destination?.QuickCode;
        }
            
    }
}
