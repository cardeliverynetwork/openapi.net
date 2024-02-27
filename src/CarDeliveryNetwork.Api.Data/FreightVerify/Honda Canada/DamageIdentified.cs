using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data.FreightVerify.Honda_Canada
{
    public class DamageIdentified : MilestoneBase
    {
        protected string aiagCode {  get; set; }

        public DamageIdentified()
        { }

        public DamageIdentified(Vehicle vehicle, DamageItem damage, ContactDetails location, Job job, Fleet contractedCarrier) : base(vehicle, job, contractedCarrier)
        {
            code = "A9";
            ms1LocationCode = location?.QuickCode;
            aiagCode = damage.AiagCode;
        }

        protected override List<ReferenceItem> GetReferenceItems()
        {
            var references = base.GetReferenceItems() ?? new List<ReferenceItem>();
            references.Add(new ReferenceItem("aiagCode", aiagCode));

            return references;
        }
    }
}
