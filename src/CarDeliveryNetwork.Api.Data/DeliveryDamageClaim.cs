using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Damage Claim entity
    /// </summary>
    public class DamageClaim : ApiEntityBase<Bid>
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
        /// A Car Delivery Network DeliveryDamageClaim.Area entity
        /// </summary>
        public class DamageArea : DamageProperty { }

        /// <summary>
        /// A Car Delivery Network DeliveryDamageClaim.Type entity
        /// </summary>
        public class DamageType : DamageProperty { }

        /// <summary>
        /// A Car Delivery Network DeliveryDamageClaim.Severity entity
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
        /// Documents
        /// </summary>
        public virtual List<Document> Documents { get; set; }
    }

    /// <summary>
    /// A collection of Car Delivery Network DeliveryDamageClaim entities.
    /// </summary>
    [CollectionDataContract]
    public class DamageClaims : ApiEntityCollectionBase<DamageClaim, DamageClaim>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.DamageItems"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public DamageClaims() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.DamageItems"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public DamageClaims(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.DamageItems"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="damageItems">The collection of DamageItems whose elements are copied to the new collection.</param>
        public DamageClaims(IEnumerable<DamageClaim> damageItems) : base(damageItems) { }
    }
}
