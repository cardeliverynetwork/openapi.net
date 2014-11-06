
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of alert types
    /// </summary>
    public enum AlertType
    {
        /// <summary>
        /// Adhoc
        /// </summary>
        Adhoc,

        /// <summary>
        /// Vehicle added
        /// </summary>
        VehicleAdded,

        /// <summary>
        /// Vehicle was not collected
        /// </summary>
        VehicleNotCollected,

        /// <summary>
        /// Additional damage was recorded between collection and deliverty
        /// </summary>
        AdditionalDamage,

        /// <summary>
        /// The broker changed some job detail
        /// </summary>
        BrokerChangeJobDetail,

        /// <summary>
        /// A question
        /// </summary>
        Question,

        /// <summary>
        /// An answer
        /// </summary>
        Answer,

        /// <summary>
        /// The job changed
        /// </summary>
        JobChanged,

        /// <summary>
        /// The broker added vehicle
        /// </summary>
        BrokerAddVehicle,

        /// <summary>
        /// A new job opportunity
        /// </summary>
        JobOpportunity,

        /// <summary>
        /// New Instant Quote Request
        /// </summary>
        InstantQuote,

        /// <summary>
        /// Job was allocted to you
        /// </summary>
        AllocatedJob,

        /// <summary>
        /// You have the winning bid
        /// </summary>
        WinningBid,

        /// <summary>
        /// The customer added a job
        /// </summary>
        CustomerAddedJob,

        /// <summary>
        /// The customer booked a job
        /// </summary>
        CustomerJobBooked,

        /// <summary>
        /// Dates were overridden
        /// </summary>
        DatesOverridden,

        /// <summary>
        /// Out of credit
        /// </summary>
        OutOfCredit,

        /// <summary>
        /// Expenses were declined
        /// </summary>
        ExpensesDeclined,

        /// <summary>
        /// Expenses were approved
        /// </summary>
        ExpensesApproved,

        /// <summary>
        /// (unused) The job was requested to be cancelled
        /// </summary>
        CancelRequested,

        /// <summary>
        /// The job was cancelled
        /// </summary>
        Canceled
    }
}