namespace CarDeliveryNetwork.Api.Data
{
    public class OpeningHours
    {
        /// <summary>
        /// The day(s) these hours apply
        /// </summary>
        public string Day { get; set; }
        /// <summary>
        /// Time open
        /// </summary>
        public string Open { get; set; }
        /// <summary>
        /// Time close
        /// </summary>
        public string Close { get; set; }
        /// <summary>
        /// Are pickups/delivers allowed outside of these hours
        /// </summary>
        public bool AllowAfterHours { get; set; }
    }
}
