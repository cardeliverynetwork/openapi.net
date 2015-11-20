using System.Collections.Generic;
using System.Linq;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data.Fenkell
{
    /// <summary>
    /// Class representing a Fenkell05 delivery
    /// </summary>
    public class Delivery
    {
        /// <summary>
        /// Gets or sets the Message reference id
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the inspection type code
        /// </summary>
        public string InspectionType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the delivery is Subject To Inspection by dealer
        /// </summary>
        public bool SubjectToInspection { get; set; }

        /// <summary>
        /// Gets or sets the Dealer comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the Carrier comment
        /// </summary>
        public string CarrierComment { get; set; }

        /// <summary>
        /// Gets or sets the contracted carrier.
        /// </summary>
        public Carrier Carrier { get; set; }

        /// <summary>
        /// Gets or sets the shipment.
        /// </summary>
        public Shipment Shipment { get; set; }

        /// <summary>
        /// Gets or sets the delivery receipt.
        /// </summary>
        public HostedDocument DeliveryReceipt { get; set; }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        public List<Vehicle> Vehicle { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Delivery"/> class.
        /// </summary>
        public Delivery() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Delivery"/> class.
        /// </summary>
        /// <param name="job">The API job from which to contruct this Delivery</param>
        public Delivery(Job job)
        {
            ReferenceId = string.IsNullOrWhiteSpace(job.LoadId)
                ? string.Format("{0}", job.JobNumber)
                : string.Format("{0}:{1}", job.JobNumber, job.LoadId);

            InspectionType = "05";
            SubjectToInspection = job.Dropoff.Signoff == null || job.Dropoff.Signoff.NotSignedReasons != null;
            Comment = job.Notes;
            // CarrierComment = 
            Carrier = new Carrier(job);
            Shipment = new Shipment(job);

            DeliveryReceipt = new HostedDocument
            {
                Title = "Proof of Delivery",
                URL = job.Dropoff.ProofDocUrl,
                ReferenceId = ReferenceId
            };

            // Throw away non delivered vehicles, or where VIN 
            // is not populated or has single digit from device default
            Vehicle = job.Vehicles.Where(v => v.Status == VehicleStatus.Delivered && (!string.IsNullOrWhiteSpace(v.Vin) && v.Vin.Length > 1))
                                  .Select(v => new Vehicle(v))
                                  .ToList();
        }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return Serialization.Serialize(new DeliveryRootObject { Delivery = this }, MessageFormat.Json);
        }
    }

    /// <summary>
    /// A root object for JSON serialization
    /// </summary>
    public class DeliveryRootObject
    {
        /// <summary>
        /// Gets or sets the delivery.
        /// </summary>
        public Delivery Delivery { get; set; }
    }
}