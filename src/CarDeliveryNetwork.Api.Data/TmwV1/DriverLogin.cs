using System;
using System.Text;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data.TmwV1
{
    /// <summary>
    /// Class representing a TMW v1 Driver Login
    /// </summary>
    public class DeiverLogin
    {
        private readonly User _driver;
        private readonly Device _device;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeiverLogin"/> class.
        /// </summary>
        public DeiverLogin() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeiverLogin"/> class.
        /// </summary>
        /// <param name="driver">The user from which to contruct this DeiverLogin</param>
        /// <param name="device">The device from which to contruct this DeiverLogin</param>
        public DeiverLogin(User driver, Device device)
        {
            _driver = driver;
            _device = device;
        }

        /// <summary>
        /// Returns a serial representation of the object.
        /// </summary>
        /// <param name="forEvent">The event for which to serialise this job</param>
        /// <param name="timeStamp">Time in UTC that this message was created</param>
        /// <param name="messageGuid">A unique identifier for this message</param>
        /// <returns>The serialized object.</returns>
        public string ToString(WebHookEvent forEvent, DateTime timeStamp, string messageGuid)
        {
            const string lineEndChar = "\r\n";

            var message = new StringBuilder();

            message.AppendFormat("ID=SN:{0}{1}", messageGuid, lineEndChar);
            message.AppendFormat("Subject=*Login Info*{0}", lineEndChar);
            message.AppendFormat("Preview=*Login Info*{0}", lineEndChar);
            message.AppendFormat("FormID=2-R{0}", lineEndChar);
            message.AppendFormat("DataType=Form{0}", lineEndChar);
            message.AppendFormat("FromName={0}{1}", _device.Name, lineEndChar);
            message.AppendFormat("CreateTime={0:yyyy-MM-dd HH:mm:ss}{1}", timeStamp, lineEndChar);
            message.AppendFormat("CreateTimeTZ=0{0}", lineEndChar);
            message.AppendFormat("ReplyMsgID=SN:0{0}", lineEndChar);
            message.AppendFormat("Priority=0{0}", lineEndChar);
            message.AppendFormat("MessageData:{0}", lineEndChar);

            message.AppendFormat("{0}{1}", _driver.RemoteId, lineEndChar);
            message.AppendFormat("{0}{1}", "Login", lineEndChar);
            message.AppendFormat("{0:yyyy-MM-dd HH:mm:ss}{1}", timeStamp, lineEndChar);

            return message.ToString();
        }
    }
}
