using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CarDeliveryNetwork.Api.Data.FreightVerify.Honda_Canada
{
    public abstract class MilestoneBase
    {
        public string vin { get; set; }
        public string code { get; set; }
        public DateTime statusUpdateTs { get; set; }

        //references
        protected string senderId { get; set; }
        protected string senderName { get; set; }
        protected string receiverCode => "HONDAIT";
        protected string scac { get; set; }
        protected string userRole => "Truck";
        protected string shipmentId { get; set; }
        protected string laneType { get; set; }
        protected string shipmentOriginCode { get; set; }
        protected string shipmentDestinationCode { get; set; }
        protected string assetId { get; set; } //Id of the truck

        protected string ms1LocationCode { get; set; }

        public List<ReferenceItem> references => GetReferenceItems();

        public MilestoneBase()
        {
        }

        public MilestoneBase(Vehicle vehicle, Job job, Fleet contractedCarrier)
        {
            vin = vehicle.Vin;
            statusUpdateTs = DateTime.UtcNow;
            senderName = contractedCarrier.Name;
            scac = contractedCarrier.Scac;

            //If QC is 9 chars assume it's an SPLC and this is a ramp to ramp job
            if (job?.Dropoff?.Destination?.QuickCode != null && job.Dropoff.Destination.QuickCode.Length == 9)
            {
                laneType = "C";
            }
            else
            {//Otherwise it's a dealer
                laneType = "D";
            }

            shipmentOriginCode = job?.Pickup?.Destination?.QuickCode;
            shipmentDestinationCode = job?.Dropoff?.Destination?.QuickCode;
            assetId = job.AssignedTruckRemoteId;
        }

        protected virtual List<ReferenceItem> GetReferenceItems()
        {
            return new List<ReferenceItem>
            {
                new ReferenceItem("senderId", senderId),
                new ReferenceItem("senderName", senderName),
                new ReferenceItem("receiverCode", receiverCode),
                new ReferenceItem("scac", scac),
                new ReferenceItem("ms1LocationCode", ms1LocationCode),
                new ReferenceItem("userRole", userRole),
                new ReferenceItem("shipmentId", shipmentId),
                new ReferenceItem("laneType", laneType),
                new ReferenceItem("shipmentOriginCode", shipmentOriginCode),
                new ReferenceItem("shipmentDestinationCode", shipmentDestinationCode),
                new ReferenceItem("assetId", assetId)
            };

        }
        public override string ToString()
        {
            var settings = new JsonSerializerSettings
            {
                DateFormatString = "yyyy-MM-dd'T'HH:mm:ss'Z'"
            };

            return JsonConvert.SerializeObject(this, settings);

        }
    }
}
