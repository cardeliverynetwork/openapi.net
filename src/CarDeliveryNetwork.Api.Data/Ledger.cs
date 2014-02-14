using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class LedgerEntry : IApiEntity
    {
        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public virtual int Year { get; set; }

        /// <summary>
        /// Gets or sets the month.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public virtual int Month { get; set; }

        /// <summary>
        /// Gets or sets the name of the month.
        /// </summary>
        /// <value>
        /// The name of the month.
        /// </value>
        public virtual string MonthName { get; set; }


        /// <summary>
        /// Gets or sets the sales.
        /// </summary>
        /// <value>
        /// The sales.
        /// </value>
        public virtual decimal Sales { get; set; }

        /// <summary>
        /// Gets or sets the purchases.
        /// </summary>
        /// <value>
        /// The purchases.
        /// </value>
        public virtual decimal Purchases{ get; set; }

        /// <summary>
        /// Gets or sets the margin.
        /// </summary>
        /// <value>
        /// The margin.
        /// </value>
        public virtual decimal Margin { get; set; }

        /// <summary>
        /// Gets or sets the jobs.
        /// </summary>
        /// <value>
        /// The jobs.
        /// </value>
        public virtual int Jobs { get; set; }

        /// <summary>
        /// Gets or sets the vehicles.
        /// </summary>
        /// <value>
        /// The vehicles.
        /// </value>
        public virtual int Vehicles { get; set; }

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
    /// A collection of Car Delivery Network Document entities.
    /// </summary>
    [CollectionDataContract]
    public class Ledger : ApiEntityCollectionBase<LedgerEntry, Ledger>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Ledger" /> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Ledger() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Ledger" /> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Ledger(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Ledger" /> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="Ledger">The collection of LedgerEntries whose elements are copied to the new collection.</param>
        public Ledger(IEnumerable<LedgerEntry> ledger) : base(ledger) { }
    }
}
