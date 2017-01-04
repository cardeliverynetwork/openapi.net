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
        private string _destinationCode;
        private DateTime _generatedTime;
        private DateTime _statusDateTime;
        private readonly short _serialNumber;

        private string SenderId {get; }
        private string ReceiverId { get { return "SC"; } }
        private string TransmissionId { get { return "R41"; } }
        private string TransmissionDate { get { return _generatedTime.ToString("MMddyy"); } }
        private string TransmissionTime {  get { return _generatedTime.ToString("HHmm"); } }
        private string TotalRecordCount {  get { return (Vehicles.Count+2).ToString("D6"); } }
        private string SerialNumber { get { return _serialNumber.ToString("D4"); } }
        private string LoadId { get { return _loadId.PadRight(15); } set { _loadId = value.Split('.').First(); } }
        private string StatusDate { get { return _statusDateTime.ToString("MMddyy"); } }
        private string StatusTime { get { return _statusDateTime.ToString("HHmm"); } }
        private string StatusCode { get { return "D09"; } }
        private string DestinationCode { get { return _destinationCode.PadRight(9); } set { _destinationCode = value; } }
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

            Vehicles = job.Vehicles.Where(v => v.Status == VehicleStatus.Delivered).ToList();

            SenderId = senderId.Substring(0,2);
            _serialNumber = sequenceNumber;
        }

        /// <summary>
        /// Output R41 as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            //Header
            stringBuilder.AppendLine($"{SenderId}{TransmissionId}{TransmissionDate}{TransmissionTime}{TotalRecordCount}{FileName}");

            foreach (var vehicle in Vehicles)
            {
                stringBuilder.AppendLine($"{LoadId}{vehicle.Vin}{StatusDate}{StatusTime}{StatusCode}{"",19}{DestinationCode}{"",43}");
            }

            //Footer
            stringBuilder.Append("EOF");

            return stringBuilder.ToString();
        }
    }
}
