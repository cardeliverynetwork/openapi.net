﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Damage Item entity
    /// </summary>
    public class DamageItem : ApiEntityBase<DamageItem>
    {
        /// <summary>
        /// DamageProperty
        /// </summary>
        public abstract class DamageProperty : IApiEntity
        {
            /// <summary>
            /// Code
            /// </summary>
            public virtual string Code { get; set; }

            /// <summary>
            /// Description
            /// </summary>
            public virtual string Description { get; set; }

            /// <summary>
            /// Returns a serial representation of the object in JSON format.
            /// </summary>
            /// <returns>The serialized object.</returns>
            public override string ToString()
            {
                return ToString(Types.MessageFormat.Json);
            }

            /// <summary>
            /// Returns a serial representation of the object in the specified format.
            /// </summary>
            /// <param name="format">Format to serialize to.</param>
            /// <returns>The serialized object.</returns>
            public string ToString(Types.MessageFormat format)
            {
                return Serialization.Serialize(this, format);
            }
        }

        /// <summary>
        /// A Car Delivery Network DamageItem.Area entity
        /// </summary>
        public class DamageArea : DamageProperty { }

        /// <summary>
        /// A Car Delivery Network DamageItem.Type entity
        /// </summary>
        public class DamageType : DamageProperty { }

        /// <summary>
        /// A Car Delivery Network DamageItem.Severity entity
        /// </summary>
        public class DamageSeverity : DamageProperty { }

        /// <summary>
        /// Damage Area
        /// </summary>
        public virtual DamageArea Area { get; set; }

        /// <summary>
        /// Damage Type
        /// </summary>
        public virtual DamageType Type { get; set; }

        /// <summary>
        /// Damage Severity
        /// </summary>
        public virtual DamageSeverity Severity { get; set; }

        /// <summary>
        /// Damage Description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// URLs of damage photos
        /// </summary>
        public virtual List<string> PhotoURL { get; set; }

        public string AiagCode => $"{Area?.Code}{Type?.Code}{Severity?.Code}";
    }

    /// <summary>
    /// A collection of Car Delivery Network DamageItem entities.
    /// </summary>
    [CollectionDataContract]
    public class DamageItems : ApiEntityCollectionBase<DamageItem, DamageItems>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.DamageItems"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public DamageItems() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.DamageItems"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public DamageItems(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.DamageItems"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="damageItems">The collection of DamageItems whose elements are copied to the new collection.</param>
        public DamageItems(IEnumerable<DamageItem> damageItems) : base(damageItems) { }
    }
}
