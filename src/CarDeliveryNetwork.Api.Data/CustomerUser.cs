using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// CustomerUser
    /// </summary>
    public class CustomerUser : ApiEntityBase<CustomerUser>
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        /// ApiKey
        /// </summary>
        public virtual Guid ApiKey { get; set; }

        /// <summary>
        /// The fleet to which this CustomerUser belongs
        /// </summary>
        public virtual Fleet HomeFleet { get; set; }

        /// <summary>
        /// The Customer to which this CustomerUser belongs
        /// </summary>
        public virtual ContactDetails HomeCustomer { get; set; }
       
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.CustomerUser"/> class.
        /// </summary>
        public CustomerUser()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this CustomerUser.
        /// </summary>
        protected virtual void InitObjects()
        {
            HomeFleet = new Fleet();
            HomeCustomer = new ContactDetails();
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network CustomerUser entities.
    /// </summary>
    [CollectionDataContract]
    public class CustomerUsers : ApiEntityCollectionBase<CustomerUser, CustomerUsers>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.CustomerUsers"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public CustomerUsers() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.CustomerUsers"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public CustomerUsers(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.CustomerUsers"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="CustomerUsers">The collection of CustomerUsers whose elements are copied to the new collection.</param>
        public CustomerUsers(IEnumerable<CustomerUser> CustomerUsers) : base(CustomerUsers) { }
    }
}
