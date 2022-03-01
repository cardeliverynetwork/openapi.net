namespace CarDeliveryNetwork.Api.Data
{
    public class OpeningHours
    {
        /// <summary>
        /// (50) The day(s) these hours apply
        /// </summary>
        public string Day { get; set; }
        /// <summary>
        /// (10) Time open
        /// </summary>
        public string Open { get; set; }
        /// <summary>
        /// (10) Time close
        /// </summary>
        public string Close { get; set; }
        /// <summary>
        /// Are pickups/delivers allowed outside of these hours
        /// </summary>
        public bool AllowAfterHours { get; set; }
    }
}
