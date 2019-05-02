using System;
using System.Collections.Generic;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    public class CdxMessage : ApiEntityBase<CdxMessage>
    {
        protected static readonly Type[] KnownTypes = new Type[]
        {
            typeof(Vehicle),
            typeof(ContactDetails),
            typeof(DamageItem)
        };

        public CdxShipment Shipment { get; set; }
        public List<CdxVehicle> CdxVehicles { get; set; }

        public override string ToString()
        {
            return Serialization.Serialize(this, MessageFormat.Xml, KnownTypes);
        }
    }
}
