using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// User
    /// </summary>
    public class User : ApiEntityBase<User>
    {
        /// <summary>
        /// First Name
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Surname
        /// </summary>
        public virtual string Surname { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// The fleet to which this user belongs
        /// </summary>
        public virtual Fleet HomeFleet { get; set; }

        /// <summary>
        /// The user's full name
        /// </summary>
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, Surname); }
            set { }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.User"/> class.
        /// </summary>
        public User()
        {
            InitObjects();
        }

        public virtual bool GetIsPopulated()
        {
            return !string.IsNullOrWhiteSpace(FirstName) || !string.IsNullOrWhiteSpace(Surname) || !string.IsNullOrWhiteSpace(Password);
        }

        public virtual bool GetIsValid()
        {
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(Surname) && !string.IsNullOrWhiteSpace(Password);
        }

        /// <summary>
        /// Initializes the child objects associated with this user.
        /// </summary>
        protected virtual void InitObjects()
        {
            HomeFleet = new Fleet();
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network User entities.
    /// </summary>
    [CollectionDataContract]
    public class Users : ApiEntityCollectionBase<User, Users>, IApiEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Users"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Users() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Users"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Users(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Users"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="users">The collection of users whose elements are copied to the new collection.</param>
        public Users(IEnumerable<User> users) : base(users) { }
    }
}
