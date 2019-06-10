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
        AtDelivery = 80,
        DeliveryOnHold = 90,
        Delivered = 100,
        NotDelivered = 110,
        Tracking = 120
    }
}
