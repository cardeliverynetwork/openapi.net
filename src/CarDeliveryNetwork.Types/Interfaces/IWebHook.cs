
namespace CarDeliveryNetwork.Types.Interfaces
{
    public interface IWebHook
    {
        int Id { get; set; }
        string Url { get; set; }
        string DataFormat { get; set; }
        string HttpMethod { get; set; }
        string Headers { get; set; }
        string RequestBody { get; set; }
    }
}
