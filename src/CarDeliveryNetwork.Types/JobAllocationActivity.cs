using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// Enum of types of job allocation.
    /// </summary>
    public enum JobAllocationActivity
    {
        /// <summary>
        /// Claimed by owner
        /// </summary>
        ClaimOwn,

        /// <summary>
        /// Direct Allocated.
        /// </summary>
        DirectAllocate,

        /// <summary>
        /// Claimed.
        /// </summary>
        Claim,

        /// <summary>
        /// Bid Placed.
        /// </summary>
        Bid

    }
}


