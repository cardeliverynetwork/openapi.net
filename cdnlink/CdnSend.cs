using System;

namespace CdnLink
{
    public partial class CdnSend
    {
        public enum SendStatus
        {
            Queued = 10,
            Processing = 20,
            Sent = 30,
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
