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

        private string ReceiverId => "SC";
        private string TransmissionId => "R41";
        private string TransmissionDate => _generatedTime.ToString("MMddyy");
        private string TransmissionTime => _generatedTime.ToString("HHmm");
        private string TotalRecordCount => (Vehicles.Count + 2).ToString("D6");//Header + Vehicles + Footer
        private string SerialNumber => _serialNumber.ToString("D4");

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
        private List<Vehicle> Vehicles { get; }

        /// <summary>
        /// Name generated for output file
        /// </summary>
        public string FileName
        {
            get { return $"{SenderId}{ReceiverId}{SerialNumber}.{TransmissionId}"; }
        }


        /// <summary>
        /// Generate R41 from job, sequential serial number and sender identifier
        /// </summary>
        /// <param name="job"></param>
        /// <param name="sequenceNumber"></param>
        /// <param name="senderId"></param>
        public R41(Job job, short sequenceNumber, string senderId)
        {
            LoadId = job.LoadId;
            DestinationCode = job.Dropoff.Destination.QuickCode;
            _generatedTime = DateTime.UtcNow;
            _statusDateTime = job.Dropoff.Signoff.Time ?? DateTime.UtcNow;

            Vehicles = job.Vehicles.Where(v => v.Status == VehicleStatus.Delivered && !string.IsNullOrWhiteSpace(v.Vin)).ToList();

            SenderId = senderId;
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
            stringBuilder.AppendLine($"{SenderId}{TransmissionId}{TransmissionDate}{TransmissionTime}{TotalRecordCount}{FileName}");

            foreach (var vehicleVin in Vehicles.Select(vehicle => vehicle.Vin.Length > 17 ? vehicle.Vin.Substring(0, 17) : vehicle.Vin))
            {
                stringBuilder.AppendLine($"{LoadId}{vehicleVin,-17}{StatusDate}{StatusTime}{StatusCode}{"",19}{DestinationCode}{"",43}");
            }

            //Footer
            stringBuilder.Append("EOF");

            return stringBuilder.ToString();
        }
    }
}
