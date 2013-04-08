using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network AccountCode entity.
    /// </summary>
    public class AccountCode : ApiEntityBase<AccountCode>
    {
        /// <summary>
        /// Readonly - A system identifier for this account code.
        /// </summary>
        public virtual string Id2 { get; set; }

        /// <summary>
        ///  Mandatory (20) - The account code corresponding to the fleet's accounting system account code.
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Optional (255) - A description for this account code.
        /// </summary>
        public virtual string Description { get; set; }
    }

    /// <summary>
    /// A collection of Car Delivery Network AccountCode entities.
    /// </summary>
    [CollectionDataContract]
    public class AccountCodes : ApiEntityCollectionBase<AccountCode, AccountCodes>, IApiEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.AccountCodes"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public AccountCodes() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.AccountCodes"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public AccountCodes(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.AccountCodes"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="AccountCodes">The collection of AccountCodes whose elements are copied to the new collection.</param>
        public AccountCodes(IEnumerable<AccountCode> AccountCodes) : base(AccountCodes) { }
    }
}
