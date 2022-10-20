using System;

namespace CarDeliveryNetwork.Types
{
    /// <summary>
    /// CDN Data schemes
    /// </summary>
    [Flags] // Not really flags - forces serialisation to int in ServiceStack DTOs
    public enum WebHookSchema
    {
        /// <summary>
        /// The CDN Schema
        /// </summary>
        Cdn = 0,

        /// <summary>
        /// The Fenkell02 Schema
        /// </summary>
        Fenkell02 = 5,

        /// <summary>
        /// The Fenkell05 Schema
        /// </summary>
        Fenkell05 = 10,

        /// <summary>
        /// The TMW V1 Schema
        /// </summary>
        TmwV1 = 20,

        /// <summary>
        /// Email
        /// </summary>
        Email = 30,

        /// <summary>
        /// PoD URL
        /// </summary>
        PodUrl = 40,

        /// <summary>
        ///  
        /// </summary>
        IclR41 = 50,

        /// <summary>
        /// The Ford Schema
        /// </summary>
        Ford = 60,

        /// <summary>
        /// CdxVehicleExchange
        /// </summary>
        CdxVehicleExchange = 65,

        /// <summary>
        /// CdxVehicles
        /// </summary>
        CdxVehicles = 70,

        /// <summary>
        /// CdxStatus
        /// </summary>
        CdxStatus = 80,

        /// <summary>
        /// CdxVehicleChange
        /// </summary>
        CdxVehicleChange = 90,

        /// <summary>
        /// CdxChange
        /// </summary>
        CdxChange = 100,

        /// <summary>
        /// The Glovis Schema
        /// </summary>
        Glovis = 110,

        /// <summary>
        /// The Glovis Exception Report Schema
        /// </summary>
        GlovisExceptionReport = 120
    }
}
