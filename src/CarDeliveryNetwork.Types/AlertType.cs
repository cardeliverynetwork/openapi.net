
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of alert types
    /// </summary>
    public enum AlertType
    {
        Adhoc,
        VehicleAdded,
        VehicleNotCollected,
        AdditionalDamage,
        BrokerChangeJobDetail,
        Question,
        Answer,
        JobChanged,
        BrokerAddVehicle,
        JobOpportunity,
        InstantQuote,
        AllocatedJob,
        WinningBid,
        CustomerAddedJob,
        CustomerJobBooked,
        DatesOverridden,
        OutOfCredit,
        ExpensesDeclined,
        ExpensesApproved
    }
}