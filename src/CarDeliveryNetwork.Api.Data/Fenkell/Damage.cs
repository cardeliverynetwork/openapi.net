namespace CarDeliveryNetwork.Api.Data.Fenkell
{
    /// <summary>
    /// 
    /// </summary>
    public class Damage
    {
        /// <summary>
        /// Gets or sets the Damage area code.
        /// </summary>
        public string AreaCode { get; set; }

        /// <summary>
        /// Gets or sets the Damage type code.
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// Gets or sets theDamage severity code.
        /// </summary>
        public string SeverityCode { get; set; }

        /// <summary>
        /// Gets or sets the Damage comment
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Damage"/> class.
        /// </summary>
        public Damage() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Damage"/> class.
        /// </summary>
        /// <param name="damage">The damage.</param>
        public Damage(DamageItem damage)
        {
            AreaCode = damage.Area.Code;
            TypeCode = damage.Type.Code;
            SeverityCode = damage.Severity.Code;
            Comment = damage.Description;
        }
    }
}
