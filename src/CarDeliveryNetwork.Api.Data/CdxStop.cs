namespace CarDeliveryNetwork.Api.Data
{
    public class CdxStop : CdxMessage
    {
        public int StopType { get; set; }
        public short NumberOfVehicles { get; set; }
        public string ProofDocUrl {get; set; }
        public string EmailList { get; set; }
        public int SignatureType { get; set; }
        public string SignedBy { get; set; }
        public string NotSignedReasons { get; set; }
        public string SignOffComment { get; set; }
    }
}
