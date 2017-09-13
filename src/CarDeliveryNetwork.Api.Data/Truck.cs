using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Truck
    /// </summary>
    public class Truck : ApiEntityBase<Truck>
    {
        /// <summary>
        /// Name of the truck
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// VIN
        /// </summary>
        public virtual string Vin { get; set; }

        /// <summary>
        /// BluetoothId, normally the MAC address of the truck's ELD
        /// </summary>
        public virtual string BluetoothId { get; set; }

        /// <summary>
        /// Bluetooth name of the truck's ELD
        /// </summary>
        public virtual string BluetoothName { get; set; }

        /// <summary>
        /// ELD device code
        /// </summary>
        public virtual string DeviceCode { get; set; }

        /// <summary>
        /// ELD Serial Number
        /// </summary>
        public virtual int? SerialNumber { get; set; }

        /// <summary>
        /// ELD DCF Name
        /// </summary>
        public virtual string DcfName { get; set; }

        /// <summary>
        /// ELD Hardware Version
        /// </summary>
        public virtual int? HardwareVersion { get; set; }

        /// <summary>
        /// ELD Software Version
        /// </summary>
        public virtual int? SoftwareVersion { get; set; }

        /// <summary>
        /// ELD DDF Version
        /// </summary>
        public virtual int? DdfVersion { get; set; }

        /// <summary>
        /// ELD GDS Version
        /// </summary>
        public virtual int? GdsVersion { get; set; }

        /// <summary>
        /// ELD Firmware version
        /// </summary>
        public virtual int? FirmwareVersion { get; set; }

        /// <summary>
        /// ELD Product Profile
        /// </summary>
        public virtual string ProductProfile { get; set; }

        /// <summary>
        /// Last time record was updated
        /// </summary>
        public virtual DateTime? UpdatedDate { get; set; }
    }

    /// <summary>
    /// A collection of Car Delivery Network Truck entities.
    /// </summary>
    [CollectionDataContract]
    public class Trucks : ApiEntityCollectionBase<Truck, Trucks>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Trucks"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public Trucks() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Trucks"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store</param>
        public Trucks(int capacity) : base(capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CarDeliveryNetwork.Api.Data.Trucks"/> class
        /// that contains elements copied from the specified collection and has sufficient
        /// capacity to accommodate the number of elements copied.
        /// </summary>
        /// <param name="devices">The collection of devices whose elements are copied to the new collection.</param>
        public Trucks(IEnumerable<Truck> devices) : base(devices) { }
    }
}
