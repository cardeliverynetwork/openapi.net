
namespace CarDeliveryNetwork.Api.Data.FreightVerify.Honda_Canada
{
    public class CarrierReceipt : MilestoneBase
    {
        public CarrierReceipt()
        { }

        public CarrierReceipt(Vehicle vehicle, Job job, Fleet contractedCarrier) : base(vehicle, job, contractedCarrier)
        {
            code = "ACPT";
            ms1LocationCode = job?.Pickup?.Destination?.QuickCode;
        }

    }
}
