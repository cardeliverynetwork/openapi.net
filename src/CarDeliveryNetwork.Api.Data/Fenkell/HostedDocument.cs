namespace CarDeliveryNetwork.Api.Data.Fenkell
{
    /// <summary>
    /// 
    /// </summary>
    public class HostedDocument
    {
        /// <summary>
        /// Gets or sets the reference id.
        /// </summary>
        public string ReferenceId { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostedDocument"/> class.
        /// </summary>
        public HostedDocument() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostedDocument"/> class.
        /// </summary>
        /// <param name="document">The document.</param>
        public HostedDocument(Document document)
        {
            // ReferenceId = document.Filename;
            URL = document.Url;
            Title = string.IsNullOrWhiteSpace(document.Comment)
                ? document.Title
                : string.Format("{0}: {1}", document.Title, document.Comment);
        }
    }
}
