using System;

namespace CdnLink
{
    public partial class CdnReceive
    {
        /// <summary>
        /// Enum indicating the status of a Receive from to FTP action
        /// </summary>
        public enum ReceiveStatus
        {
            /// <summary>
            /// The receive is being processed by CdnLink
            /// </summary>
            Processing = 50,

            /// <summary>
            /// The receive is queued, waiting to be processed by the calling application
            /// </summary>
            Queued = 60,

            /// <summary>
            /// An error occurred whilst processing a receive
            /// </summary>
            Error = 70,

            /// <summary>
            /// The client system has processed this record
            /// </summary>
            ClientProcessed = 80
        }

        public void SetAsError(string message, string code = null)
        {
            FailedDate = DateTime.Now;
            Status = (int)ReceiveStatus.Error;
            ErrorMessage = message;
            ErrorCode = code;
        }
    }
}
