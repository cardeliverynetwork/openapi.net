using System.Collections.Generic;
using System.Linq;
using CarDeliveryNetwork.Types;
using ApiData = CarDeliveryNetwork.Api.Data;

namespace CarDeliveryNetwork.Api.Data.Fenkell05
{
    /// <summary>
    /// 
    /// </summary>
    public class Delivery
    {
        /// <summary>
        /// Gets or sets the Message reference id
        /// </summary>
        public virtual string ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the inspection type code
        /// </summary>
        public virtual string InspectionType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the delivery is Subject To Inspection by dealer
        /// </summary>
        public virtual bool SubjectToInspection { get; set; }

        /// <summary>
        /// Gets or sets the Dealer comment
        /// </summary>
        public virtual string Comment { get; set; }

        /// <summary>
        /// Gets or sets the Carrier comment
        /// </summary>
        public virtual string CarrierComment { get; set; }

        /// <summary>
        /// Gets or sets the contracted carrier.
        /// </summary>
        public virtual Carrier Carrier { get; set; }

        /// <summary>
        /// Gets or sets the shipment.
        /// </summary>
        public virtual Shipment Shipment { get; set; }

        /// <summary>
        /// Gets or sets the delivery receipt.
        /// </summary>
        public virtual HostedDocument DeliveryReceipt { get; set; }

        /// <summary>
        /// Gets or sets the vehicle.
        /// </summary>
        public virtual List<Vehicle> Vehicle { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Delivery"/> class.
        /// </summary>
        public Delivery() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Delivery"/> class.
        /// </summary>
        /// <param name="job"></param>
        public Delivery(ApiData.Job job)
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
    /// 
    /// </summary>
    public class Carrier
    {
        /// <summary>
        /// Gets or sets the SCAC or code assigned by OEM
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the driver.
        /// </summary>
        public virtual string DriverName { get; set; }

        /// <summary>
        /// Gets or sets the truck number.
        /// </summary>
        public virtual string TruckNumber { get; set; }

        /// <summary>
        /// Gets or sets the trailer number.
        /// </summary>
        public virtual string TrailerNumber { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Carrier"/> class.
        /// </summary>
        public Carrier() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Carrier"/> class.
        /// </summary>
        /// <param name="job">The job.</param>
        public Carrier(ApiData.Job job)
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

    /// <summary>
    /// 
    /// </summary>
    public class Shipment
    {
        /// <summary>
        /// Gets or sets the Origin location code assigned by OEM
        /// </summary>
        public virtual string OriginCode { get; set; }

        /// <summary>
        /// Gets or sets the destination Destination code assigned by OEM
        /// </summary>
        public virtual string DestinationCode { get; set; }

        /// <summary>
        /// Gets or sets the Departure date and time.
        /// </summary>
        public virtual string DepartureDateTime { get; set; }

        /// <summary>
        /// Gets or sets the Delivery date and time
        /// </summary>
        public virtual string DeliveryDateTime { get; set; }

        /// <summary>
        /// Gets or sets the Special delivery instructions
        /// </summary>
        public virtual string SpecialInstructions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shipment"/> class.
        /// </summary>
        public Shipment() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shipment"/> class.
        /// </summary>
        /// <param name="job">The job.</param>
        public Shipment(ApiData.Job job)
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

    /// <summary>
    /// 
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Gets or sets the VIN.
        /// </summary>
        public virtual string VIN { get; set; }

        /// <summary>
        /// Gets or sets the damage.
        /// </summary>
        public virtual List<Damage> Damage { get; set; }

        /// <summary>
        /// Gets or sets the photos.
        /// </summary>
        public virtual List<HostedDocument> Photo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        public Vehicle() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="vehicle">The vehicle.</param>
        public Vehicle(ApiData.Vehicle vehicle)
        {
            VIN = vehicle.Vin;
            Damage = vehicle.DamageAtDropoff.Select(d => new Damage(d)).ToList();
            Photo = vehicle.Photos.Where(p => !p.Url.Contains("CollectionDamage"))
                                  .Select(p => new HostedDocument(p))
                                  .ToList();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Damage
    {
        /// <summary>
        /// Gets or sets the Damage area code.
        /// </summary>
        public virtual string AreaCode { get; set; }

        /// <summary>
        /// Gets or sets the Damage type code.
        /// </summary>
        public virtual string TypeCode { get; set; }

        /// <summary>
        /// Gets or sets theDamage severity code.
        /// </summary>
        public virtual string SeverityCode { get; set; }

        /// <summary>
        /// Gets or sets the Damage comment
        /// </summary>
        public virtual string Comment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Damage"/> class.
        /// </summary>
        public Damage() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Damage"/> class.
        /// </summary>
        /// <param name="damage">The damage.</param>
        public Damage(ApiData.DamageItem damage)
        {
            AreaCode = damage.Area.Code;
            TypeCode = damage.Type.Code;
            SeverityCode = damage.Severity.Code;
            Comment = damage.Description;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HostedDocument
    {
        /// <summary>
        /// Gets or sets the reference id.
        /// </summary>
        public virtual string ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public virtual string URL { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostedDocument"/> class.
        /// </summary>
        public HostedDocument() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostedDocument"/> class.
        /// </summary>
        /// <param name="document">The document.</param>
        public HostedDocument(ApiData.Document document)
        {
            // ReferenceId = document.Filename;
            URL = document.Url;
            Title = string.IsNullOrWhiteSpace(document.Comment)
                ? document.Title
                : string.Format("{0}: {1}", document.Title, document.Comment);
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