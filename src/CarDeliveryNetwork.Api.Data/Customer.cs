namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Customer entity.
    /// </summary>
    /// <remarks>
    /// If you only specify the Id or QuickCode then the contact must be already present
    /// To create a new contact you must specify the OrganisationName, City, StateRegion
    /// If you specify a contact that is already in the database all fields present will
    /// updated the the fields in the Job contact.
    /// The more address detail provided for a contact the more accurate a map location will be. 
    /// The system will use google maps and the data provided to pin point a contact on a map.
    /// </remarks>
    public class Customer : ContactDetails
    {

        /// <summary>
        /// Optional (255) - A unique identifier with which to refer to this contact.
        /// </summary>
        /// <remarks>
        /// QuickCode is an optional, client system generated, unique Id by which the resource can be 
        /// referred to on Car Delivery Network.  Once the resource is created, QuickCode cannot be changed 
        /// </remarks>
        public virtual int? DrivenRateCardId { get; set; }

        /// <summary>
        /// Optional (255) - A unique identifier with which to refer to this contact.
        /// </summary>
        /// <remarks>
        /// QuickCode is an optional, client system generated, unique Id by which the resource can be 
        /// referred to on Car Delivery Network.  Once the resource is created, QuickCode cannot be changed 
        /// </remarks>
        public virtual int? TransportedRateCardId { get; set; }
    }

    
}
