using System.Collections.Generic;
using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;
using System;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// A Car Delivery Network Vehicle entity. You must specify a VIN or Registration. 
    /// </summary>
    /// <remarks> 
    /// If you do not want to specify an actual VIN you should specify a placeholder (1,2,3 etc) 
    /// of less than 6 characters and the system will force the capture of a valid VIN
    /// </remarks>	
    public class Vehicle : ApiEntityBase<Vehicle>
    {
        /// <summary>
        /// Optional (10) - The vehicle registration number or year manufactured.
        /// </summary>
        public virtual string Registration { get; set; }

        /// <summary>
        /// Optional (17) - The vehicle identification number. A valid VIN is 6-17 characters.
        /// </summary>
        public virtual string Vin { get; set; }

        /// <summary>
        /// Optional (50) - The vehicle make/manufacturer.
        /// </summary>
        public virtual string Make { get; set; }

        /// <summary>
        /// Optional (50) - The vehicle model.
        /// </summary>
        public virtual string Model { get; set; }

        /// <summary>
        /// Optional (50) - The vehicle variant, type or sub-model.
        /// </summary>
        public virtual string Variant { get; set; }

        /// <summary>
        /// Optional (50) - The vehicle's location or bay number
        /// </summary>
        public virtual string Location { get; set; }

        /// <summary>
        /// Optional (50) - A movement number specific to this Vehicle
        /// </summary>
        public virtual string MovementNumber { get; set; }

        /// <summary>
        /// Optional (255) - Notes relating to this Vehicle.
        /// </summary>
        public virtual string Notes { get; set; }

        /// <summary>
        /// Readonly - Name of person signing off this vehicle
        /// </summary>
        public virtual string SignedBy { get; set; }

        /// <summary>
        /// Readonly - SignoffComment
        /// </summary>
        public virtual string SignoffComment { get; set; }

        /// <summary>
        /// Readonly - Signature of person signing off this vehicle
        /// </summary>
        public virtual string Signature { get; set; }

        /// <summary>
        /// Readonly - Name of person damage claim signing off this vehicle
        /// </summary>
        public virtual string DamageClaimSignedBy { get; set; }

        /// <summary>
        /// Readonly - DamageClaimSignoffComment
        /// </summary>
        public virtual string DamageClaimSignoffComment { get; set; }

        /// <summary>
        /// Readonly - Signature of person damage claim signing off this vehicle
        /// </summary>
        public virtual Document DamageClaimSignature { get; set; }

        /// <summary>
        /// Readonly - Date Time of damage claim this vehicle
        /// </summary>
        public virtual DateTime? DamageClaimDate { get; set; }

        /// <summary>
        /// Readonly - The current status of the Vehicle
        /// </summary>
        public virtual VehicleStatus Status { get; set; }

        /// <summary>
        /// Readonly - The reason that the vehicle was either not pickup up or not delivered
        /// </summary>
        public virtual string NonCompletionReason { get; set; }

        /// <summary>
        /// Readonly - A collection of damage items recorded at pickup
        /// </summary>
        public virtual List<DamageItem> DamageAtPickup { get; set; }

        /// <summary>
        /// Readonly - A collection of EXTRA damage items recorded at dropoff
        /// </summary>
        public virtual List<DamageItem> DamageAtDropoff { get; set; }

        /// <summary>
        /// Readonly - A collection of EXTRA damage items recorded at delivery
        /// </summary>
        public virtual List<DamageItem> DamageClaims { get; set; }

        /// <summary>
        /// Readonly - A collection of EXTRA damage claim items recorded at delivery
        /// </summary>
        public virtual List<Document> DamageClaimDocuments { get; set; }

        /// <summary>
        /// Readonly - A collection of photos assocated with this vehicle
        /// </summary>
        public virtual List<Document> Photos { get; set; }

        /// <summary>
        /// Readonly - A collection of duty of care fields assocated with this vehicle
        /// </summary>
        public virtual Dictionary<string, string> DutyOfCare { get; set; }
        
        /// <summary>
        /// Readonly - A collection of paperwork fields assocated with this vehicle
        /// </summary>
        public virtual Dictionary<string, string> Paperwork { get; set; }

        /// <summary>
        /// Readonly - Indicates that the vehicle has been picked up (or delivered)
        /// </summary>
        public bool IsPickedUp
        {
            get { return Status == VehicleStatus.Delivered || Status == VehicleStatus.PickedUp; }   
        }

        /// <summary>
        /// Readonly - Indicates that the vehicle has been delivered
        /// </summary>
        public bool IsDelivered
        {
            get { return Status == VehicleStatus.Delivered; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Vehicle"/> class
        /// </summary>
        public Vehicle()
        {
            InitObjects();
        }

        /// <summary>
        /// Initializes the child objects associated with this Vehicle.
        /// </summary>
        protected virtual void InitObjects()
        {
            DamageAtPickup = new List<DamageItem>();
            DamageAtDropoff = new List<DamageItem>();
            Photos = new List<Document>();
            Paperwork = new Dictionary<string, string>();
            DutyOfCare = new Dictionary<string, string>();
        }
    }

    /// <summary>
    /// A collection of Car Delivery Network Vehicle Vehicle entities.
    /// </summary>
    [CollectionDataContract]
    public class Vehicles : ApiEntityCollectionBase<Vehicle, Vehicles>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Vehicles"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Vehicles() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Vehicles"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public Vehicles(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Vehicles"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="vehicles">The collection of Vehicles whose elements are copied to the new collection.</param>
        public Vehicles(IEnumerable<Vehicle> vehicles) : base(vehicles) { }
    }
}
