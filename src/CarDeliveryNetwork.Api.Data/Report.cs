using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDeliveryNetwork.Api.Data
{
    public class Report
    {
        public List<List<object>> Table { get; set; }

        public string Data { get; set; }
    }
}
