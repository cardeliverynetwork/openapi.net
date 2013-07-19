using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CdnLink
{
    public partial class CdnReceives
    {
        public enum ReceiveStatus
        {
            Processing = 50,
            Queued = 60,
            Error = 70
        }
    }
}
