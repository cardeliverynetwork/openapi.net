
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Maps Types to types
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Maps a WebHookEvent to a JobStatus
        /// </summary>
        /// <param name="webHookEvent"></param>
        /// <returns></returns>
        public static JobStatus WebHookEventToJobStatus(WebHookEvent webHookEvent)
        {
            switch (webHookEvent)
            {
                case WebHookEvent.PickupStop:
                    return JobStatus.Collected;
                case WebHookEvent.PickupDamageRecorded:
                    return JobStatus.Collected;
                case WebHookEvent.DropoffStop:
                    return JobStatus.Complete;
                case WebHookEvent.CarrierClaimApproved:
                    return JobStatus.Complete;
                case WebHookEvent.AssignedToDriver:
                    return JobStatus.Assigned;
                case WebHookEvent.OnWayPickup:
                    return JobStatus.OnWayToCollect;
                case WebHookEvent.OnWayDeliver:
                    return JobStatus.OnWayToDeliver;
                case WebHookEvent.AtPickup:
                    return JobStatus.AtCollection;
                case WebHookEvent.AtDelivery:
                    return JobStatus.AtDelivery;
                case WebHookEvent.JobCreated:
                    return JobStatus.Created;

                default:
                    return JobStatus.Quote;
            }

        }

        /// <summary>
        /// Maps a WebHookEvent to a CdxShipmentStatus
        /// </summary>
        /// <param name="webHookEvent"></param>
        /// <returns></returns>
        public static CdxShipmentStatus WebHookEventToCdxJobStatus(WebHookEvent webHookEvent)
        {
            switch (webHookEvent)
            {
                case WebHookEvent.OnWayPickup:
                    return CdxShipmentStatus.OnWayToPickup;
                case WebHookEvent.AtPickup:
                    return CdxShipmentStatus.AtPickUp;
                case WebHookEvent.PickupStop:
                    return CdxShipmentStatus.PickedUp;;
                case WebHookEvent.OnWayDeliver:
                    return CdxShipmentStatus.OnWayToDeliver;
                case WebHookEvent.AtDelivery:
                    return CdxShipmentStatus.AtDelivery;
                case WebHookEvent.DropoffStop:
                    return CdxShipmentStatus.Delivered;
               
                case WebHookEvent.CdxShipmentCreated:
                default:
                    return CdxShipmentStatus.ExchangeCreated;
            }
        }
    }
}
