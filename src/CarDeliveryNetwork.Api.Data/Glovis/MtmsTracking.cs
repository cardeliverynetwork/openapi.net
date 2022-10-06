using CarDeliveryNetwork.Types;
using System.Collections.Generic;
using System.Linq;

namespace CarDeliveryNetwork.Api.Data.Glovis
{
    /// <summary>
    /// 
    /// </summary>
    public class MtmsTracking
    {
        /// <summary>
        /// 
        /// </summary>
        public MtmsHead HEAD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MtmsTrackingBody BODY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MtmsTracking() { }

        /// <summary>
        /// 
        /// </summary>
        public MtmsTracking(Job job, string userId, string carrierUuid, string carrierKey, Position position)
        {
            var timestamp = job.StatusDeviceTime.HasValue
                ? job.StatusDeviceTime.Value
                : System.DateTime.UtcNow;

            HEAD = new MtmsHead
            {
                DEVICE_UUID = carrierUuid,
                SECURE_KEY = carrierKey,
                REQ_SID = "158984645841701251263641",
                GEO_LAT = position == null ? "0" : position.Latitude.ToString(),
                GEO_LNG = position == null ? "0" : position.Longitude.ToString(),
                TIME_ZONE = "UTC"
            };

            BODY = new MtmsTrackingBody
            {
                USER_ID = userId,
                LOC_CD = "PH",
                TRUK_NO = job.AssignedTruckRemoteId,
                VIN_LIST = new List<MtmsTrackingVehicle>()
            };

            foreach (var v in job.Vehicles.Where(v => v.IsPickedUp))
            {
                BODY.VIN_LIST.Add(new MtmsTrackingVehicle
                {
                    VIN = v.Vin,
                    LOAD_LOC_CD = "PH"
                });
            }
        }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return Serialization.Serialize(new MtmsTrackingRootObject { data = this }, MessageFormat.Json);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class MtmsTrackingRootObject
    {
        /// <summary>
        /// 
        /// </summary>
        public MtmsTracking data { get; set; }
    }
}
