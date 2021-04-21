using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Approval status
    /// </summary>
    public enum ApprovalStatus
    {
        /// <summary>
        /// Pending approval
        /// </summary>
        Pending = 10,

        /// <summary>
        /// Approved
        /// </summary>
        Approved = 20,

        /// <summary>
        /// Refused
        /// </summary>
        Refused = 30,
    }
}
