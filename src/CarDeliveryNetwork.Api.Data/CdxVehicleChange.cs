using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDeliveryNetwork.Api.Data
{

    /// <summary>
    /// A CDX vehicle for a CDXChange file
    /// </summary>
    public class CdxVehicleChange : CdxVehicle
    {
        private bool _isAdded;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAdded"></param>
        public CdxVehicleChange(bool isAdded)
        {
            _isAdded = isAdded;
        }

        /// <summary>
        /// Is this vehicle to be added or removed
        /// </summary>
        public string Update
        {
            get
            {
                return _isAdded ? "ADD" : "REMOVE";
            }
        } 
    }
}
