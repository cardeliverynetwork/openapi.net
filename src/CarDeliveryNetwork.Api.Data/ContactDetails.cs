
namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network ContactDetails entity.
    /// </summary>
	/// <remarks>
    /// If you only specify the Id or RemoteId then the contact must be already present
	/// To create a new contact you must specify the OrganisationName, City, StateRegion
	/// If you specify a contact that is already in the database all fields present will
	/// updated the the fields in the Job contact
	/// The more address detail provided for a contact the more accurate a map location will be. 
	/// The system will use google maps and the data provided to pin point a contact on a map 
    /// </remarks>
    public class ContactDetails : ApiEntityBase<ContactDetails>
    {
        /// <summary>
        /// Optional (255) - A unique identifier with which to refer to this contact.
        /// </summary>
        /// <remarks>
        /// QuickCode is an optional, client system generated, unique Id by which the resource can be 
        /// referred to on Car Delivery Network.  Once the resource is created, QuickCode cannot be changed 
        /// </remarks>
        public string QuickCode { get; set; }

        /// <summary>
        /// Optional (100) - The name of the person associated with this contact.
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        /// Optional (100) - The organization name associated with this contact.
        /// </summary>
        public string OrganisationName { get; set; }

        /// <summary>
        /// Optional (300) - The first line(s) of the address, e.g. Building name/number and street name.
        /// </summary>
        public string AddressLines { get; set; }

        /// <summary>
        /// Optional (300) - The city.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Optional (255) - The state or region.
        /// </summary>
        public string StateRegion { get; set; }

        /// <summary>
        /// Optional (10) - The Zip or postal code.
        /// </summary>
        public string ZipPostCode { get; set; }

        /// <summary>
        /// Optional (ntext) - The email address associated with this contact.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Optional (30) - The fax number associated with this contact.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// Optional (30) - The fixed/land-line phone number associated with this contact.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Optional (30) - The mobile phone number associated with this contact.
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Optional (30) - An alternative phone number associated with this contact.
        /// </summary>
        public string OtherPhone { get; set; }

        /// <summary>
        /// Optional (ntext) - Notes relating to this contact.
        /// </summary>
        public string Notes { get; set; }
    }
}
