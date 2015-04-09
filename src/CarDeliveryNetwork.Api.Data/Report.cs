using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Class to hold CDN report data
    /// </summary>
    public class Report
    {
        /// <summary>
        /// Table format
        /// </summary>
        public List<List<object>> Table { get; set; }

        /// <summary>
        /// Object forma
        /// </summary>
        public string Data { get; set; }
    }
}
