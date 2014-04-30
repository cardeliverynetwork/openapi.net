
namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// An enumeration that specifies the type of a Car Delivery Network document.
    /// </summary>
    public enum DocumentType
    {
        Other = 0,
        Expenses = 1,
        CollectionSignoff = 2,
        CollectionDamage = 3,
        DeliverySignoff = 4,
        DeliveryDamage = 5,
        Insurance = 6,
        Terms = 7,
        DOT = 8,
        W9 = 9,
        Authority = 10,
        FuelReceipt = 11,
        ShippingNote = 12,
        UserPhoto = 13,
        UserSignature = 14,
        OdoPhoto = 15,
        SaferStat = 16,
        AdditionalInsurance = 17,
        EquipmentList = 18,
    }
}
