using CarDeliveryNetwork.Api.Data;

namespace CdnLink
{
    public partial class CdnReceivedDocument
    {
        public CdnReceivedDocument(Document doc)
            : this()
        {
            Comment = doc.Comment;
            Title = doc.Title;
            Url = doc.Url;
            Type = (int)doc.Type;
            FriendlyType = doc.FriendlyType;
        }
    }
}
