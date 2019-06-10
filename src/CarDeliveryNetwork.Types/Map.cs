
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Maps Types to types
    /// </summary>
    public class Map
    {
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
