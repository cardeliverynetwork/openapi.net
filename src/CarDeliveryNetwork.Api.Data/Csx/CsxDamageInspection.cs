using CarDeliveryNetwork.Types;
using System;

namespace CarDeliveryNetwork.Api.Data.Csx
{
    public class DamageInspection
    {

        public string source { get; set; }
        public string haulawayTransactionId { get; set; }
        public string rampID { get; set; }
        public string terminalName { get; set; }
        public DateTime inspectionDatetime { get; set; }
        public Driver driver { get; set; }
        public Vehicle[] vehicles { get; set; }

        public DamageInspection(Job job, Data.Vehicle vehicle, string damagePhotoBase64, string userId, string carrierUuid, string carrierKey) 
        {
            source = job.AllocatedCarrierScac;
            haulawayTransactionId = job.JobNumber;
            rampID = job.Pickup?.Destination?.QuickCode;
            terminalName = job.Pickup?.Destination?.OrganizationName;
            inspectionDatetime = DateTime.UtcNow;

            driver = new Driver
            {
                companySCAC = job.AllocatedCarrierScac,
                companyName = job.Pickup?.Destination?.OrganizationName,
                driverName = job.AssignedDriverName
            };

            vehicles = new Vehicle[] 
            { 
                new Vehicle 
                { 
                    vin = vehicle.Vin,
                    previousDamage = "NO"
                } 
            };
        }


        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return Serialization.Serialize(new DamageInspectionRoot {  inspection = this}, MessageFormat.Json);
        }
    }


    public class Driver
    {
        public string companySCAC { get; set; }
        public string companyName { get; set; }
        public string driverName { get; set; }
        public string emailAddress { get; set; }
    }

    public class Vehicle
    {
        public string vin { get; set; }
        public Railcar railCar { get; set; }
        public string mfrsCode { get; set; }
        public string previousDamage { get; set; }
        public string haulawayComments { get; set; }
        public string verificationReminder { get; set; }
        public string inspectionType { get; set; }
        public Damage[] damages { get; set; }
        public string[] images { get; set; }
    }

    public class Railcar
    {
        public string carInitial { get; set; }
        public string carNumber { get; set; }
    }

    public class Damage
    {
        public string damageType { get; set; }
        public string damageItem { get; set; }
        public string damageSeverity { get; set; }
        public string damageExcInd { get; set; }
    }

    public class DamageInspectionRoot
    {
        public DamageInspection inspection { get; set; }
    }

}
