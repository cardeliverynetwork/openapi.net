using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using CarDeliveryNetwork.Types.Interfaces;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network ContactDetails entity.
    /// </summary>
    /// <remarks>
    /// If you only specify the Id or QuickCode then the contact must be already present
    /// To create a new contact you must specify the OrganisationName, City, StateRegion
    /// If you specify a contact that is already in the database all fields present will
    /// updated the the fields in the Job contact.
    /// The more address detail provided for a contact the more accurate a map location will be. 
    /// The system will use google maps and the data provided to pin point a contact on a map.
    /// </remarks>
    public class ContactDetails : ApiEntityBase<ContactDetails>, IContactDetails
    {
        /// <summary>
        /// Optional (255) - A unique identifier with which to refer to this contact.
        /// </summary>
        /// <remarks>
        /// QuickCode is an optional, client system generated, unique Id by which the resource can be 
        /// referred to on Car Delivery Network.  Once the resource is created, QuickCode cannot be changed 
        /// </remarks>
        public virtual string QuickCode { get; set; }

        /// <summary>
        /// Optional (100) - The name of the person associated with this contact.
        /// </summary>
        public virtual string Contact { get; set; }

        /// <summary>
        /// Optional (100) - The organization name associated with this contact.
        /// </summary>
        public virtual string OrganisationName { get; set; }

        /// <summary>
        /// Optional (300) - The first line(s) of the address, e.g. Building name/number and street name.
        /// </summary>
        public virtual string AddressLines { get; set; }

        /// <summary>
        /// Optional (300) - The city.
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Optional (255) - The state or region.
        /// </summary>
        public virtual string StateRegion { get; set; }

        /// <summary>
        /// Optional (10) - The Zip or postal code.
        /// </summary>
        public virtual string ZipPostCode { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public virtual double Latitude { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>7
        public virtual double Longitude { get; set; }

        /// <summary>
        /// Readonly - BestGeoLookupString
        /// </summary>
        public virtual string BestGeoLookupString { get; set; }
        
        /// <summary>
        /// Optional (ntext) - The email address associated with this contact.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Optional (30) - The fax number associated with this contact.
        /// </summary>
        public virtual string Fax { get; set; }

        /// <summary>
        /// Optional (30) - The fixed/land-line phone number associated with this contact.
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        /// Optional (30) - The mobile phone number associated with this contact.
        /// </summary>
        public virtual string MobilePhone { get; set; }

        /// <summary>
        /// Optional (30) - An alternative phone number associated with this contact.
        /// </summary>
        public virtual string OtherPhone { get; set; }

        /// <summary>
        /// Optional (ntext) - Notes relating to this contact.
        /// </summary>
        public virtual string Notes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.ContactDetails"/> class.
        /// </summary>
        public ContactDetails() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.ContactDetails"/> class
        /// with fields copied from the specified contact
        /// </summary>
        /// <param name="c">The contact to copy into this new instance</param>
        public ContactDetails(IContactDetails c)
        {
            QuickCode = c.QuickCode;
            Contact = c.Contact;
            OrganisationName = c.OrganisationName;
            AddressLines = c.AddressLines;
            City = c.City;
            StateRegion = c.StateRegion;
            ZipPostCode = c.ZipPostCode;
            Email = c.Email;
            Fax = c.Fax;
            Phone = c.Phone;
            MobilePhone = c.MobilePhone;
            OtherPhone = c.OtherPhone;
            Notes = c.Notes;
        }

        /// <summary>
        /// Gets a web friendly address string 
        /// </summary>
        /// <param name="includeQuickCode">Indicates that the QuickCode field should be included</param>
        /// <param name="includeContact">Indicates that the Contact field should be included</param>
        /// <param name="includeOrganisation">Indicates that the Organisation field should be included</param>
        /// <param name="includeAddressLines">Indicates that the AddressLines field should be included</param>
        /// <param name="includeCity">Indicates that the City field should be included</param>
        /// <param name="includeStateRegion">Indicates that the StateRegion field should be included</param>
        /// <param name="includeZipPostcode">Indicates that the ZipPostCode field should be included</param>
        /// <param name="includePhones">Indicates that the various Phone number fields should be included</param>
        /// <param name="includeFax">Indicates that the Fax field should be included</param>
        /// <param name="includeEmail">Indicates that the Email field should be included</param>
        /// <param name="includeNotes">Indicates that the Notes field should be included</param>
        /// <param name="isUSFormat">Indicates that the address should be in US format</param>
        /// <returns>A web friendly address string</returns>
        public string ToWebString(
            bool includeQuickCode = false,
            bool includeContact = true,
            bool includeOrganisation = true,
            bool includeAddressLines = true,
            bool includeCity = true,
            bool includeStateRegion = true,
            bool includeZipPostcode = true,
            bool includePhones = true,
            bool includeFax = true,
            bool includeEmail = true,
            bool includeNotes = true,
            bool isUSFormat = true)
        {
            var result = new StringBuilder();
            if (includeQuickCode && !string.IsNullOrWhiteSpace(QuickCode))
                result.AppendFormat("{0}<br />", QuickCode);
            if (includeContact && !string.IsNullOrWhiteSpace(Contact))
                result.AppendFormat("{0}<br />", Contact);
            if (includeOrganisation && !string.IsNullOrWhiteSpace(OrganisationName))
                result.AppendFormat("{0}<br />", OrganisationName);
            if (includeAddressLines && !string.IsNullOrWhiteSpace(AddressLines))
                result.AppendFormat("{0}<br />", AddressLines);

            if (isUSFormat && !string.IsNullOrWhiteSpace(City) && !string.IsNullOrWhiteSpace(StateRegion) && !string.IsNullOrWhiteSpace(ZipPostCode))
            {
                result.AppendFormat("{0}, {1} {2}<br />", City, StateRegion, ZipPostCode);
            }
            else
            {
                if (includeCity && !string.IsNullOrWhiteSpace(City))
                    result.AppendFormat("{0}<br />", City);
                if (includeStateRegion && !string.IsNullOrWhiteSpace(StateRegion))
                    result.AppendFormat("{0}<br />", StateRegion);
                if (includeZipPostcode && !string.IsNullOrWhiteSpace(ZipPostCode))
                    result.AppendFormat("{0}<br />", ZipPostCode);
            }

            if (includePhones && !string.IsNullOrWhiteSpace(Phone))
                result.AppendFormat("{0}<br />", Phone);
            if (includePhones && !string.IsNullOrWhiteSpace(OtherPhone))
                result.AppendFormat("{0}<br />", OtherPhone);
            if (includePhones && !string.IsNullOrWhiteSpace(MobilePhone))
                result.AppendFormat("{0}<br />", MobilePhone);
            if (includeFax && !string.IsNullOrWhiteSpace(Fax))
                result.AppendFormat("Fax: {0}<br />", Fax);
            if (includeEmail && !string.IsNullOrWhiteSpace(Email))
                result.AppendFormat("{0}<br />", Email);
            if (includeNotes && !string.IsNullOrWhiteSpace(Notes))
                result.AppendFormat("<br />{0}<br />", Notes);
            return result.ToString();
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network ContactDetails entities.
    /// </summary>
    [CollectionDataContract]
    public class ContactDetailss : ApiEntityCollectionBase<ContactDetails, ContactDetailss>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.ContactDetailss"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public ContactDetailss() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.ContactDetailss"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public ContactDetailss(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.ContactDetailss"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="devices">The collection of devices whose elements are copied to the new collection.</param>
        public ContactDetailss(IEnumerable<ContactDetails> devices) : base(devices) { }
    }
}
