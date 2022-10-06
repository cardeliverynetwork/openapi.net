using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data.Glovis
{
    /// <summary>
    /// 
    /// </summary>
    public class MtmsTrackingBody
    {
        /// <summary>
        /// 
        /// </summary>
        public string USER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LOC_CD { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TRUK_NO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<MtmsTrackingVehicle> VIN_LIST { get; set; }
    }
}
