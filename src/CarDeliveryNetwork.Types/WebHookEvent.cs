﻿using System;

namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of Car Delivery Network WebHook status'.
    /// </summary>
    [Flags] // Not really flags - forces serialisation to int in ServiceStack DTOs
    public enum WebHookEvent
    {
        /// <summary>
        /// WebHook Event for a stop where vehicles were picked up
        /// </summary>
        PickupStop,

        /// <summary>
        /// WebHook Event for when damage was recorded at pickup
        /// </summary>
        PickupDamageRecorded = 5,

        /// <summary>
        /// WebHook Event for a stop where vehicles were dropped off
        /// </summary>
        DropoffStop = 10,

        /// <summary>
        /// WebHook Event for carrier claimed
        /// </summary>
        CarrierClaimApproved = 20,

        /// <summary>
        /// WebHook Event for driver logged in to device
        /// </summary>
        DriverLogin = 30,

        /// <summary>
        /// WebHook Event for load allocated to carrier
        /// </summary>
        AllocatedToCarrier = 35,

        /// <summary>
        /// WebHook Event for load assigned to driver
        /// </summary>
        AssignedToDriver = 40,

        /// <summary>
        /// WebHook Event for on way to Pickup
        /// </summary>
        OnWayPickup = 50,

        /// <summary>
        /// WebHook Event for on way to Delivery
        /// </summary>
        OnWayDeliver = 60,

        /// <summary>
        /// WebHook Event for at Pickup
        /// </summary>
        AtPickup = 70,

        /// <summary>
        /// WebHook Event for at Delivery
        /// </summary>
        AtDelivery = 80,

        /// <summary>
        /// WebHook Event for job creation
        /// </summary>
        JobCreated = 90,

        /// <summary>
        /// WebHook Event for CDx Shipment creation
        /// </summary>
        CdxShipmentCreated = 100,

        /// <summary>
        /// WebHook Event for Cdx Shipment changes
        /// </summary>
        CdxShipmentChanged = 110,

        /// <summary>
        /// WebHook Event for vinDISPATCH bid received event 
        /// </summary>
        BidReceived = 1000,

        /// <summary>
        /// WebHook Event for vinDISPATCH bid request claimed 
        /// subject to contract event
        /// </summary>
        ClaimedSubjectToContract = 1010
    }
}