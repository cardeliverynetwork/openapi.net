﻿
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// An enumeration that specifies the type of a Car Delivery Network document.
    /// </summary>
    public enum DocumentType
    {
        /// <summary>
        /// The other
        /// </summary>
        Other = 0,
        /// <summary>
        /// The expenses
        /// </summary>
        Expenses = 1,
        /// <summary>
        /// The collection signoff
        /// </summary>
        CollectionSignoff = 2,
        /// <summary>
        /// The collection damage
        /// </summary>
        CollectionDamage = 3,
        /// <summary>
        /// The delivery signoff
        /// </summary>
        DeliverySignoff = 4,
        /// <summary>
        /// The delivery damage
        /// </summary>
        DeliveryDamage = 5,
        /// <summary>
        /// The insurance
        /// </summary>
        Insurance = 6,
        /// <summary>
        /// The terms
        /// </summary>
        Terms = 7,
        /// <summary>
        /// The DOT
        /// </summary>
        Dot = 8,
        /// <summary>
        /// The w9
        /// </summary>
        W9 = 9,
        /// <summary>
        /// The authority
        /// </summary>
        Authority = 10,
        /// <summary>
        /// The fuel receipt
        /// </summary>
        FuelReceipt = 11,
        /// <summary>
        /// The shipping note
        /// </summary>
        ShippingNote = 12,
        /// <summary>
        /// The user photo
        /// </summary>
        UserPhoto = 13,
        /// <summary>
        /// The user signature
        /// </summary>
        UserSignature = 14,
        /// <summary>
        /// The odo photo
        /// </summary>
        OdoPhoto = 15,
        /// <summary>
        /// The safer stat
        /// </summary>
        SaferStat = 16,
        /// <summary>
        /// The additional insurance
        /// </summary>
        AdditionalInsurance = 17,
        /// <summary>
        /// The equipment list
        /// </summary>
        EquipmentList = 18,

        /// <summary>
        /// CSV Vehicle Improt
        /// </summary>
        VehicleImport = 19,

        /// <summary>
        /// Dealer damage claim
        /// </summary>
        DeliveryClaimDamage = 20,

        /// <summary>
        /// Dealer delivery claim document
        /// </summary>
        DeliveryClaimDocument = 21,

        /// <summary>
        /// Dealer delivery claim photos
        /// </summary>
        DeliveryClaimPhoto = 22,

        /// <summary>
        /// Photo of users driving licence
        /// </summary>
        LicencePhoto = 23,

        /// <summary>
        /// Photo/Scan of a pickup relase form
        /// </summary>
        PickupReleaseForm = 24,

        /// <summary>
        /// A pre-pickup photo of a vehicle
        /// </summary>
        PrePickupPhoto = 25,

        /// <summary>
        /// A photo of damage sustained pre-pickup
        /// </summary>
        PrePickupDamagePhoto = 26,

        /// <summary>
        /// Documents to be added to the destination
        /// </summary>
        DestinationDocument = 27
    }
}
