using System;

namespace CdnLink
{
    public partial class CdnSend
    {
        /// <summary>
        /// Enum indicating the status of a Send to CDN action
        /// </summary>
        public enum SendStatus
        {
            /// <summary>
            /// The send is queued, waiting to be processed by CdnLink
            /// </summary>
            Queued = 10,
            
            /// <summary>
            /// The send is being processed by CdnLink
            /// </summary>
            Processing = 20,
            
            /// <summary>
            /// The send is sent
            /// </summary>
            Sent = 30,

            /// <summary>
            /// An error occurred when processing a send
            /// </summary>
            Error = 40
        }

        public void SetAsError(string message, string code = null)
        {
            FailedDate = DateTime.Now;
            Status = (int)SendStatus.Error;
            ErrorMessage = message;
            ErrorCode = code;
        }
    }
}
