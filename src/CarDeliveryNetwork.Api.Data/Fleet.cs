using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Fleet entity.
    /// </summary>
    public class Fleet : ApiEntityBase<Fleet>
    {
        /// <summary>
        /// The fleet's name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The fleet's SCAC
        /// </summary>
        public virtual string Scac { get; set; }

        /// <summary>
        /// The URL of this fleet's logo
        /// </summary>
        public virtual string LogoUrl { get; set; }

        /// <summary>
        /// The URL of this fleet's supplier terms
        /// </summary>
        public virtual string SupplierTermsUrl { get; set; }

        /// <summary>
        /// The URL of this fleet's customer terms
        /// </summary>
        public virtual string CustomerTermsUrl { get; set; }

        /// <summary>
        /// The fleet's contact details
        /// </summary>
        public virtual ContactDetails Contact { get; set; }

        /// <summary>
        /// Readonly - A collection of insurances assocated with this fleet
        /// </summary>
        public virtual List<Insurance> Insurances { get; set; }

        /// <summary>
        /// Readonly - A collection of documents assocated with this fleet
        /// </summary>
        public virtual List<Document> Documents { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Fleet"/> class.
        /// </summary>
        public Fleet()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this job.
        /// </summary>
        protected virtual void InitObjects()
        {
            Contact = new ContactDetails();
            Insurances = new List<Insurance>();
            Documents = new List<Document>();
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network Fleet entities.
    /// </summary>
    [CollectionDataContract]
    public class Fleets : ApiEntityCollectionBase<Fleet, Fleets>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Fleets"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Fleets() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Fleets"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public Fleets(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Fleets"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="fleets">The collection of fleets whose elements are copied to the new collection.</param>
        public Fleets(IEnumerable<Fleet> fleets) : base(fleets) { }
    }
}
