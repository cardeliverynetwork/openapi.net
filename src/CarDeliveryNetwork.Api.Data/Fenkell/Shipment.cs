namespace CarDeliveryNetwork.Api.Data.Fenkell
{
    /// <summary>
    /// 
    /// </summary>
    public class Shipment
    {
        /// <summary>
        /// Gets or sets the Origin location code assigned by OEM
        /// </summary>
        public string OriginCode { get; set; }

        /// <summary>
        /// Gets or sets the destination Destination code assigned by OEM
        /// </summary>
        public string DestinationCode { get; set; }

        /// <summary>
        /// Gets or sets the Departure date and time.
        /// </summary>
        public string DepartureDateTime { get; set; }

        /// <summary>
        /// Gets or sets the Delivery date and time
        /// </summary>
        public string DeliveryDateTime { get; set; }

        /// <summary>
        /// Gets or sets the Special delivery instructions
        /// </summary>
        public string SpecialInstructions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shipment"/> class.
        /// </summary>
        public Shipment() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shipment"/> class.
        /// </summary>
        /// <param name="job">The job.</param>
        public Shipment(Job job)
        {
            OriginCode = job.Pickup.Destination.QuickCode;
            DestinationCode = job.Dropoff.Destination.QuickCode;

            if (job.Pickup.Signoff != null && job.Pickup.Signoff.Time.HasValue)
                DepartureDateTime = job.Pickup.Signoff.Time.Value.ToString("o");
            if (job.Dropoff.Signoff != null && job.Dropoff.Signoff.Time.HasValue)
                DeliveryDateTime = job.Dropoff.Signoff.Time.Value.ToString("o");

            SpecialInstructions = job.Customer.Notes;
        }
    }
}
