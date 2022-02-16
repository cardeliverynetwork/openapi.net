using CarDeliveryNetwork.Types;
using System.Collections.Generic;
using System.Linq;

namespace CarDeliveryNetwork.Api.Data.Glovis
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    /// <summary>
    /// 
    /// </summary>
    public class MtmsLoadUnload
    {
        /// <summary>
        /// 
        /// </summary>
        public MtmsHead HEAD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MtmsLoadUnloadBody BODY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MtmsLoadUnload() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="isPickup"></param>
        /// <param name="userId"></param>
        /// <param name="carrierUuid"></param>
        /// <param name="carrierKey"></param>
        /// <param name="position"></param>
        public MtmsLoadUnload(Job job, bool isPickup, string userId, string carrierUuid, string carrierKey, Position position)
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

            BODY = new MtmsLoadUnloadBody
            {
                USER_ID = userId,
                EVT_TYPE = isPickup ? "L" : "U",
                EVT_DT = timestamp.ToString("yyyMMdd"),
                EVT_TM = timestamp.ToString("HHmmss"),
                LOC_TYPE = "PORT",
                LOC_CD = "PH",
                TRUK_NO = job.AssignedTruckRemoteId,
                VIN_LIST = new List<MtmsLoadUnloadVehicle>()
            };

            foreach (var v in job.Vehicles)
            {
                var hasDamage =
                    (v.DamageAtPickup != null && v.DamageAtPickup.Count > 0) ||
                    (v.DamageAtDropoff != null && v.DamageAtDropoff.Count > 0);

                BODY.VIN_LIST.Add(new MtmsLoadUnloadVehicle
                {
                    VIN = v.Vin,
                    MODL_CD = v.Model,
                    DAMG_YN = hasDamage ? "Y" : "N"
                });
            }
        }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return Serialization.Serialize(new MtmsLoadUnloadRootObject { data = this }, MessageFormat.Json);
        }
    }

    public class MtmsLoadUnloadRootObject
    {
        /// <summary>
        /// 
        /// </summary>
        public MtmsLoadUnload data { get; set; }
    }
}

