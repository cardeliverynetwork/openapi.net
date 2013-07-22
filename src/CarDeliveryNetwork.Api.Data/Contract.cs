using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Contract
    /// </summary>
    public class Contract : ApiEntityBase<Contract>
    {
        /// <summary>
        /// SequenceNumber
        /// </summary>
        public virtual int SequenceNumber { get; set; }

        /// <summary>
        /// PriceToPay
        /// </summary>
        public virtual int PriceToPay { get; set; }

        /// <summary>
        /// InvoiceNumber
        /// </summary>
        public virtual string InvoiceNumber { get; set; }

        /// <summary>
        /// CustomerFee
        /// </summary>
        public virtual int CustomerFee { get; set; }

        /// <summary>
        /// SupplierFee
        /// </summary>
        public virtual int SupplierFee { get; set; }

        /// <summary>
        /// The fleet subcontracting/brokering the job
        /// </summary>
        public virtual Fleet CustomerFleet { get; set; }

        /// <summary>
        /// The subcontractor; fleet that will own or sub-contract the job
        /// </summary>
        public virtual Fleet SupplierFleet { get; set; }
    }

    /// <summary>
    /// A collection of Car Delivery Network Contract entities.
    /// </summary>
    [CollectionDataContract]
    public class Contracts : ApiEntityCollectionBase<Contract, Contracts>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Contracts"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Contracts() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Contracts"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Contracts(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Contracts"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="contracts">The collection of contracts whose elements are copied to the new collection.</param>
        public Contracts(IEnumerable<Contract> contracts) : base(contracts) { }
    }
}
