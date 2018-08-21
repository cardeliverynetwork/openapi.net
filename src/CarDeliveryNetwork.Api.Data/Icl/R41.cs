using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data.Icl
{
    /// <summary>
    /// Icl R41 document type
    /// </summary>
    public class R41
    {
        private string _loadId;
        private string _senderId;
        private string _receiverId;
        private string _destinationCode;
        private DateTime _generatedTime;
        private DateTime _statusDateTime;
        private readonly short _serialNumber;        

        private string SenderId
        {
            get { return _senderId.PadRight(2); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = string.Empty;
                }
                _senderId = value.Length > 2 ? value.Substring(0, 2) : value;
            }
        }

        private string ReceiverId
        {
            get { return _receiverId.PadRight(2); }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = string.Empty;
                }
                _receiverId = value.Length > 2 ? value.Substring(0, 2) : value;
            }
        }
        private string TransmissionId { get { return "R41"; } }
        private string TransmissionDate { get { return _generatedTime.ToString("MMddyy"); } }
        private string TransmissionTime { get { return _generatedTime.ToString("HHmm"); } }
        private string TotalRecordCount { get { return (Vehicles.Count + 2).ToString("D6"); } }//Header + Vehicles + Footer
        private string SerialNumber { get { return _serialNumber.ToString("D4"); } } 

        private string LoadId
        {
            get
            {
                return _loadId.PadRight(15);
            }
            set
            {
                var loadIdWithoutSubParts = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Split('.').First();
                _loadId = (loadIdWithoutSubParts.Length > 15
                    ? loadIdWithoutSubParts.Substring(0, 15)
                    : loadIdWithoutSubParts);
            }
        }
        private string StatusDate { get { return _statusDateTime.ToString("MMddyy"); } }
        private string StatusTime { get { return _statusDateTime.ToString("HHmm"); } }
        private string StatusCode { get { return "D09"; } }

        private string DestinationCode
        {
            get
            {
                return _destinationCode.PadRight(9);
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    value = string.Empty;
                }
                _destinationCode = value.Length > 9 ? value.Substring(0, 9) : value;
            }
        }
        private List<Vehicle> Vehicles { get; set; }

        /// <summary>
        /// Name generated for output file
        /// </summary>
        public string FileName
        {
            get { return string.Format("{0}{1}{2}.{3}", SenderId, ReceiverId, SerialNumber, TransmissionId); }
        }


        /// <summary>
        /// Generate R41 from job, sequential serial number, sender identifier and receiver identifier
        /// </summary>
        /// <param name="job"></param>
        /// <param name="sequenceNumber"></param>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        public R41(Job job, short sequenceNumber, string senderId, string receiverId)
        {
            var cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
            //Hacking dates as ICL require Carrier time. Fudging with CST

            LoadId = job.LoadId;
            DestinationCode = job.Dropoff.Destination.QuickCode;
            _generatedTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, cstZone);
            _statusDateTime = TimeZoneInfo.ConvertTimeFromUtc(job.Dropoff.Signoff.Time ?? DateTime.UtcNow, cstZone);

            Vehicles = job.Vehicles.Where(v => v.Status == VehicleStatus.Delivered && !string.IsNullOrWhiteSpace(v.Vin)).ToList();

            SenderId = senderId;
            ReceiverId = receiverId;
            _serialNumber = sequenceNumber;
        }

        /// <summary>
        /// Output R41 as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Vehicles.Count == 0 || string.IsNullOrWhiteSpace(SenderId) || string.IsNullOrWhiteSpace(LoadId) || string.IsNullOrWhiteSpace(DestinationCode))
            {
                return string.Empty;
            }


            var stringBuilder = new StringBuilder();

            //Header
            stringBuilder.AppendFormat("{0}{1}{2}{3}{4}{5}\n", SenderId, TransmissionId, TransmissionDate, TransmissionTime, TotalRecordCount, FileName);

            foreach (var vehicle in Vehicles)
            {
                var vehicleVin = (vehicle.Vin.Length > 17) ? vehicle.Vin.Substring(0, 17) : vehicle.Vin;
                var damageIndicator = (vehicle.DamageAtDropoff.Any() || vehicle.DamageAtPickup.Any()) ? 'Y' : 'N';
                stringBuilder.AppendFormat("{0}{1,-17}{2}{3}{4}                   {5,-9}         {6}                                 \n", LoadId, vehicleVin, StatusDate, StatusTime, StatusCode,DestinationCode, damageIndicator);
            }

            //Footer
            stringBuilder.Append("EOF");

            return stringBuilder.ToString();
        }
    }
}
