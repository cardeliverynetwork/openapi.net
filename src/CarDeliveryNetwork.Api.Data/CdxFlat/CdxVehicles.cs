using CarDeliveryNetwork.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDeliveryNetwork.Api.Data.CdxFlat
{
    /// <summary>
    /// 
    /// </summary>
    public class CdxVehicles
    {
        private Job _job;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="job"></param>
        public CdxVehicles(Job job)
        {
            _job = job;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forEvent"></param>
        /// <param name="eventDateTime"></param>
        /// <returns></returns>
        public string ToString(WebHookEvent forEvent, DateTime eventDateTime)
        {
            return null;
        }
    }
}
