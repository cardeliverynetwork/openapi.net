using System;

namespace CarDeliveryNetwork.Types.Interfaces
{
    /// <summary>
    /// An interface for contact details
    /// </summary>
    public interface IContactDetails
    {
        /// <summary>
        /// A unique identifier with which to refer to this contact.
        /// </summary>
        string QuickCode { get; set; }

        /// <summary>
        /// The location code for this location
        /// </summary>
        string LocationCode { get; }

        /// <summary>
        ///  The name of the person associated with this contact.
        /// </summary>
        string Contact { get; }

        /// <summary>
        /// The gender of the person associated with this contact.
        /// </summary>
        Gender Gender { get; }

        /// <summary>
        /// The age of the person associated with this contact.
        /// </summary>
        DateTime? DateOfBirth { get; }

        /// <summary>
        /// The organization name associated with this contact.
        /// </summary>
        string OrganisationName { get; }

        /// <summary>
        /// The first line(s) of the address, e.g. Building name/number and street name.
        /// </summary>
        string AddressLines { get; }

        /// <summary>
        /// The city.
        /// </summary>
        string City { get; }

        /// <summary>
        /// The state or region.
        /// </summary>
        string StateRegion { get; }

        /// <summary>
        /// The Zip or postal code.
        /// </summary>
        string ZipPostCode { get; }

        /// <summary>
        /// The email address associated with this contact.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// The fax number associated with this contact.
        /// </summary>
        string Fax { get; }

        /// <summary>
        /// The fixed/land-line phone number associated with this contact.
        /// </summary>
        string Phone { get; }

        /// <summary>
        /// The mobile phone number associated with this contact.
        /// </summary>
        string MobilePhone { get; }

        /// <summary>
        /// An alternative phone number associated with this contact.
        /// </summary>
        string OtherPhone { get; }

        /// <summary>
        /// Notes relating to this contact.
        /// </summary>
        string Notes { get; set; }
    }
}
