namespace CarDeliveryNetwork.Types
{
    public enum CdxShipmentStatus
    {
        ExchangeCreated = 0,
        ReceivedByDriver = 10,
        OnWayToPickup = 20,
        PickupOnHold = 30,
        AtPickUp = 40,
        PickedUp = 50,
        NotPickedUp = 60,
        OnWayToDeliver = 70,
        DeliveryOnHold = 80,
        Delivered = 90,
        NotDelivered = 100,
        Tracking = 110
    }
}
