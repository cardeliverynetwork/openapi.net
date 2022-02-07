using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data.Glovis
{
    /// <summary>
    /// 
    /// </summary>
    public class MtmsExceptionReportBody
    {
        /// <summary>
        /// 
        /// </summary>
        public string USER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EVT_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EVT_DT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EVT_TM { get; set; }

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
        public string VIN { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CONTENTS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<MtmsDamageItem> DAMAGE_LIST { get; set; }
    }
}
