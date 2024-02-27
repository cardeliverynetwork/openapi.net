namespace CarDeliveryNetwork.Api.Data.FreightVerify.Honda_Canada
{
    public class ReferenceItem
    {
        public ReferenceItem(string qualifier, string value)
        {
            this.qualifier = qualifier;
            this.value = value;
        }

        public string qualifier { get; }
        public string value { get; set; }
    }
}
