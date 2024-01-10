using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace CarDeliveryNetwork.Api.Data.BNSF
{
    public class PreOutGate
    {
        public PreOutGate()
        {
            vin = new string[0];
        }

        public PreOutGate(Job apiJob, string driverLicenseNumber, string yardQuickCode)
        {
            carrierScac = apiJob.AllocatedCarrierScac;
            driverLicense = string.IsNullOrWhiteSpace(driverLicenseNumber) ? "" : driverLicenseNumber;
            outGateLocation = yardQuickCode;
            gatePassCode = string.IsNullOrWhiteSpace(apiJob.LoadId) ? Guid.NewGuid().ToString().Substring(0, 6).ToUpper() : apiJob.LoadId;
            loadNumber = string.IsNullOrWhiteSpace(apiJob.LoadId) ? "" : apiJob.LoadId;
            truckNumber = string.IsNullOrWhiteSpace(apiJob.AssignedTruckRemoteId) ? "" : apiJob.AssignedTruckRemoteId;
            vin = apiJob.Vehicles.Select(v => v.Vin).ToArray();
        }

        public string carrierScac { get; set; }
        public string driverLicense { get; set; }
        public string outGateLocation { get; set; }
        public string gatePassCode { get; set; }
        public string loadNumber { get; set; }

        //must have setter for serialise to work
        public string trailerNumber { get { return ""; } set { } }
        public string truckNumber { get; set; }
        public string[] vin { get; set; }


        public class PreOutGateRoot
        {
            public PreOutGate preGateRequestData { get; set; }
        }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss.fff'Z'")
            };

            return Serialization.SerializeAsJson(new PreOutGateRoot { preGateRequestData = this }, settings);
        }
    }
}
