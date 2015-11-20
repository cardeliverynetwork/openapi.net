namespace CarDeliveryNetwork.Api.Data.Fenkell
{
    /// <summary>
    /// 
    /// </summary>
    public class Carrier
    {
        /// <summary>
        /// Gets or sets the SCAC or code assigned by OEM
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the driver.
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// Gets or sets the truck number.
        /// </summary>
        public string TruckNumber { get; set; }

        /// <summary>
        /// Gets or sets the trailer number.
        /// </summary>
        public string TrailerNumber { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Carrier"/> class.
        /// </summary>
        public Carrier() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Carrier"/> class.
        /// </summary>
        /// <param name="job">The job.</param>
        public Carrier(Job job)
        {
            // Fix for Harbour jobs - make them look like Hansens for Fenkell
            Code = job.ContractedCarrierScac == "HRBR"
                ? "HATA"
                : job.ContractedCarrierScac;

            DriverName = job.AssignedDriverRemoteId;
            TrailerNumber = job.AssignedAppId;
            TruckNumber = job.AssignedTruckRemoteId;
        }
    }
}
