using System;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// Describes an export file
    /// </summary>
    public class Export
    {
        /// <summary>
        /// The type of things we're exporting
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// File format for the export
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// A list of Ids of the things we're exporting
        /// </summary>
        public string Ids { get; set; }

        /// <summary>
        /// Whether to indicate that the exported thing has been exported
        /// </summary>
        public bool MarkAsExported { get; set; }

        /// <summary>
        /// UTC Time of export
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// The Url of the export file
        /// </summary>
        public string Url { get; set; }
    }
}
