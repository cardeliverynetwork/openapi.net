using CarDeliveryNetwork.Types;
using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data.Glovis
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

    /// <summary>
    /// 
    /// </summary>
    public class MtmsExceptionReport
    {
        /// <summary>
        /// 
        /// </summary>
        public MtmsHead HEAD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MtmsExceptionReportBody BODY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public MtmsExceptionReport() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="vehicle"></param>
        /// <param name="isPickup"></param>
        /// <param name="carrierUuid"></param>
        /// <param name="carrierKey"></param>
        public MtmsExceptionReport(Job job, Vehicle vehicle, bool isPickup, string carrierUuid, string carrierKey)
        {
            var timestamp = job.StatusDeviceTime.HasValue
                ? job.StatusDeviceTime.Value
                : System.DateTime.UtcNow;

            HEAD = new MtmsHead
            {
                DEVICE_UUID = carrierUuid,
                SECURE_KEY = carrierKey,
                REQ_SID = "158984645841701251263641",
                GEO_LAT = "0",
                GEO_LNG = "0",
                TIME_ZONE = "UTC"
            };

            BODY = new MtmsExceptionReportBody
            {
                USER_ID = job.AssignedDriverRemoteId,
                EVT_TYPE = "R",
                EVT_DT = timestamp.ToString("yyyMMdd"),
                EVT_TM = timestamp.ToString("HHmmss"),
                LOC_CD = "PH",
                TRUK_NO = job.AssignedTruckRemoteId,
                VIN = vehicle.Vin,
                CONTENTS = "damage report",
                DAMAGE_LIST = new List<MtmsDamageItem>()
            };

            var damageList = isPickup
                ? vehicle.DamageAtPickup
                : vehicle.DamageAtDropoff;

            foreach (var d in damageList)
            {
                BODY.DAMAGE_LIST.Add(new MtmsDamageItem
                {
                    DAMAGE_CD = string.Format("{0}{1}{2}", d.Area.Code, d.Type.Code, d.Severity.Code)
                });
            }
        }

        /// <summary>
        /// Returns a serial representation of the object in JSON format.
        /// </summary>
        /// <returns>The serialized object.</returns>
        public override string ToString()
        {
            return Serialization.Serialize(new MtmsExceptionReportRootObject { data = this }, MessageFormat.Json);
        }
    }

    public class MtmsExceptionReportRootObject
    {
        /// <summary>
        /// 
        /// </summary>
        public MtmsExceptionReport data { get; set; }
    }
}

