﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types.Interfaces;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// User
    /// </summary>
    public class User : ApiEntityBase<User>, IImportable
    {
        /// <summary>
        /// Optional (40) - A unique identifier for this resource, generated by the client system.
        /// </summary>
        /// <remarks>
        /// RemoteId is an optional, client system generated, unique Id by which the resource can be
        /// referred to on Car Delivery Network.  Once the resource is created, RemoteId cannot be changed.
        /// </remarks>
        public virtual string RemoteId { get; set; }

        /// <summary>
        /// First Name
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Surname
        /// </summary>
        [Obsolete("Please use LastName")]
        public virtual string Surname { get { return LastName; } set { LastName = value; } }

        /// <summary>
        /// Username
        /// </summary>
        public virtual string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// MobilePhone
        /// </summary>
        public virtual string MobilePhone { get; set; }

        /// <summary>
        /// The fleet to which this user belongs
        /// </summary>
        public virtual Fleet HomeFleet { get; set; }

        /// <summary>
        /// Url of the user's photograph
        /// </summary>
        public virtual string Photo { get; set; }

        /// <summary>
        /// Url of the user's signature
        /// </summary>
        public virtual string Signature { get; set; }

        /// <summary>
        /// Gets the last known position of this user
        /// </summary>
        public virtual Position LastPosition { get; set; }

        /// <summary>
        /// Optional - A tag/group for this user
        /// </summary>
        public virtual string Tag { get; set; }

        /// <summary>
        /// The user's full name
        /// </summary>
        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
            set { }
        }

        public string ImportDisplayString
        {
            get { return FullName; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.User"/> class.
        /// </summary>
        public User()
        {
            InitObjects();
        }

        /// <summary>
        /// Gets whether the user is considered populated.
        /// </summary>
        /// <returns>True when populated.</returns>
        public virtual bool GetIsPopulated()
        {
            return !string.IsNullOrWhiteSpace(FirstName) || !string.IsNullOrWhiteSpace(LastName) || !string.IsNullOrWhiteSpace(Password);
        }

        /// <summary>
        /// Gets whether the user is considered valid.
        /// </summary>
        /// <returns>True when valid</returns>
        public virtual bool GetIsValid()
        {
            return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName) && !string.IsNullOrWhiteSpace(Password);
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
    public class Users : ApiEntityCollectionBase<User, Users>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Users"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Users() { }

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
