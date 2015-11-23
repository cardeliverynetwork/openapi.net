using System.Collections.Generic;
using System.Linq;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data.Fenkell
{
    /// <summary>
    /// Class representing a Fenkell02 pickup
    /// </summary>
    public class Pickup
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
        /// Gets or sets a value indicating whether the pickup is Subject To Inspection by dealer
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
        /// Gets or sets the pickup receipt.
        /// </summary>
        public HostedDocument PickupReceipt { get; set; }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        public List<Vehicle> Vehicle { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pickup"/> class.
        /// </summary>
        public Pickup() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pickup"/> class.
        /// </summary>
        /// <param name="job">The API job from which to contruct this Pickup</param>
        public Pickup(Job job)
        {
            ReferenceId = string.IsNullOrWhiteSpace(job.LoadId)
                ? string.Format("{0}", job.JobNumber)
                : string.Format("{0}:{1}", job.JobNumber, job.LoadId);

            InspectionType = "02";
            SubjectToInspection = job.Pickup.Signoff == null || job.Pickup.Signoff.NotSignedReasons != null;
            Comment = job.Notes;
            // CarrierComment = 
            Carrier = new Carrier(job);
            Shipment = new Shipment(job);

            PickupReceipt = new HostedDocument
            {
                Title = "Proof of Pickup",
                URL = job.Pickup.ProofDocUrl,
                ReferenceId = ReferenceId
            };

            // Throw away non collected vehicles, or where VIN 
            // is not populated or has single digit from device default
            Vehicle = job.Vehicles.Where(v => v.Status == VehicleStatus.PickedUp && (!string.IsNullOrWhiteSpace(v.Vin) && v.Vin.Length > 1))
                                  .Select(v => new Vehicle(v, true /*With Pickup Damage*/))
                                  .ToList();
        }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return Serialization.Serialize(new PickupRootObject { Pickup = this }, MessageFormat.Json);
        }
    }

    /// <summary>
    /// A root object for JSON serialization
    /// </summary>
    public class PickupRootObject
    {
        /// <summary>
        /// Gets or sets the pickup.
        /// </summary>
        public Pickup Pickup { get; set; }
    }
}