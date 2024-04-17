
namespace CarDeliveryNetwork.Api.Data.FreightVerify.Honda_Canada
{
    public class CarrierReceipt : MilestoneBase
    {
        public CarrierReceipt()
        { }

        public CarrierReceipt(Vehicle vehicle, Job job, Fleet contractedCarrier, string senderId) : base(vehicle, job, contractedCarrier, senderId)
        {
            code = "ACPT";
            ms1LocationCode = job?.Pickup?.Destination?.QuickCode;
        }

    }
}
