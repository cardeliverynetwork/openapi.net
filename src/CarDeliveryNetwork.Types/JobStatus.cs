
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of Car Delivery Network jobs status'.
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// Quote
        /// </summary>
        Quote = 0,

        /// <summary>
        /// Created
        /// </summary>
        Created = 1,

        /// <summary>
        /// Assigned
        /// </summary>
        Assigned = 2,

        /// <summary>
        /// Reassigned
        /// </summary>
        Reassigned = 3,

        /// <summary>
        /// CancelPending
        /// </summary>
        CancelPending = 4,

        /// <summary>
        /// ReassignPending
        /// </summary>
        ReassignPending = 5,

        /// <summary>
        /// Emailed
        /// </summary>
        Emailed = 6,

        /// <summary>
        /// Sent
        /// </summary>
        Sent = 7,

        /// <summary>
        /// Received
        /// </summary>
        Received = 8,

        /// <summary>
        /// OnHoldCollection
        /// </summary>
        OnHoldCollection = 11,

        /// <summary>
        /// OnWayToCollect
        /// </summary>
        OnWayToCollect = 12,

        /// <summary>
        /// AtCollection
        /// </summary>
        AtCollection = 13,

        /// <summary>
        /// Collected
        /// </summary>
        Collected = 14,

        /// <summary>
        /// OnWayToDeliver
        /// </summary>
        OnWayToDeliver = 16,

        /// <summary>
        /// OnHoldDelivery
        /// </summary>
        OnHoldDelivery = 17,

        /// <summary>
        /// AtDelivery
        /// </summary>
        AtDelivery = 18,

        /// <summary>
        /// Delivered
        /// </summary>
        Delivered = 19,

        /// <summary>
        /// Closed
        /// </summary>
        Closed = 21,

        /// <summary>
        /// NonComplete
        /// </summary>
        NonComplete = 22,

        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled = 23,

        /// <summary>
        /// Complete
        /// </summary>
        Complete = 24,

        /// <summary>
        /// CompleteConfirmed
        /// </summary>
        CompleteConfirmed = 1024,

        /// <summary>
        /// NavigateToDelivery
        /// </summary>
        NavigateToDelivery = 25,

        /// <summary>
        /// NavigateToCollect
        /// </summary>
        NavigateToCollect = 26,

        /// <summary>
        /// Booked
        /// </summary>
        Booked = 27,

        /// <summary>
        /// Allocated
        /// </summary>
        Allocated = 28,

        /// <summary>
        /// Approved
        /// </summary>
        Approved = 29,

        /// <summary>
        /// Invoiced
        /// </summary>
        Invoiced = 30,

        /// <summary>
        /// Transhipping
        /// </summary>
        Transhipping = 31,

        /// <summary>
        /// Transhipped
        /// </summary>
        Transhipped = 32,

        /// <summary>
        /// AlertRaised
        /// </summary>
        AlertRaised = 33,

        /// <summary>
        /// AlertCleared
        /// </summary>
        AlertCleared = 34,

        /// <summary>
        /// Split
        /// </summary>
        Split = 35,

        /// <summary>
        /// Opportunity
        /// </summary>
        Opportunity = 36,

        /// <summary>
        /// Draft
        /// </summary>
        Draft = 38,

        /// <summary>
        /// JobNoteAdded
        /// </summary>
        JobNoteAdded = 39,

        /// <summary>
        /// TradeExchange
        /// </summary>
        TradeExchange = 40
    };
}