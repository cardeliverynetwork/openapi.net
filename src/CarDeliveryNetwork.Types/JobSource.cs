﻿namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// An enumeration that specifies where the job originated
    /// </summary>
    public enum JobSource
    {
        /// <summary>
        /// Job was created on go.cardeliverynetwork.com
        /// </summary>
        VinDeliverOffice,

        /// <summary>
        /// Job was created on go.vincarrier.com
        /// </summary>
        VinCarrierOffice,

        /// <summary>
        /// Job was created on the VinCarrier app
        /// </summary>
        VinCarrierApp,

        /// <summary>
        /// Job was created using CDN Link
        /// </summary>
        CdnLink,

        /// <summary>
        /// Job was created through Cdx
        /// </summary>
        Cdx,

        /// <summary>
        /// Job was imported using a Central Dispatch Bill Of Lading
        /// </summary>
        CentralDispatchBOL
    }
}