using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdnLink
{
    public partial class CdnReceive
    {
        public enum ReceiveStatus
        {
            Processing = 50,
            Queued = 60,
            Error = 70
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
