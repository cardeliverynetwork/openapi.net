using System.Runtime.Serialization;
using CarDeliveryNetwork.Types;

namespace CarDeliveryNetwork.Api.Data
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ApiError
    {
        /// <summary>
        /// 
        /// </summary>
        //[DataMember(Name = "responseStatus")]
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Reads the JSON document and returns the deserialized object.
        /// </summary>
        /// <param name="serializedObject">The JSON serialized object.</param>
        /// <param name="format">Format to deserialize from.</param>
        /// <returns>The deserialized object.</returns>
        public static ApiError FromString(string serializedObject, MessageFormat format = MessageFormat.Json)
        {
            return Serialization.Deserialise<ApiError>(serializedObject, format);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class ResponseStatus
    {
        /// <summary>
        /// 
        /// </summary>
       // [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
       // [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //[DataMember(Name = "stackTrace")]
        public string StackTrace { get; set; }
    }
}

//{
//"responseStatus":{
//"errorCode": "BadRequest",
//"message": "LoadId '' was used before on job of Id 1815",
//"stackTrace": "[Jobs: 5/17/2017 9:07:43 PM]:\n[REQUEST: [{createdDate:2017-05-17T21:07:31.339Z,loadId:TestLoad2,status:Quote,jobInitiator:Chris Wallis,customerReference:REF123,notes:These are the notes,serviceRequired:Transported,mileage:0,sellPrice:0,buyPrice:0,driverPay:0,customer:{quickCode:COMPANY1,contact:John Smith,organisationName:The Company,addressLines:82a Wellington Street,city:Thame,stateRegion:Oxfordshire,zipPostCode:OX9 3BN,latitude:0,longitude:0,email:jsmith@example.com,phone:123 123 123,notes:The Customer Notes,importDisplayString:\"COMPANY1, The Company, Thame, OX9 3BN\",id:0},pickup:{destination:{quickCode:WHVCS,contact:Paul Pie,organisationName:We Have Cars Ltd.,addressLines:\"4 Well Dean\r\nCastlefields\",city:Prudhoe,stateRegion:Northumberland,zipPostCode:NE42 5QQ,latitude:0,longitude:0,phone:456 456 456,notes:These are the pickup notes,importDisplayString:\"WHVCS, We Have Cars Ltd., Prudhoe, NE42 5QQ\",id:0},signoff:{},requestedDate:2017-05-18,requestedDateIsExact:True},dropoff:{destination:{quickCode:WNDCS,contact:Peter Piper,organisationName:We Need Cars Ltd.,addressLines:1 Yeomans Court,city:Hemel Hempstead,stateRegion:Hertfordshire,zipPostCode:HP2 7GJ,latitude:0,longitude:0,phone:789 789 789,notes:These are the drop-off notes,importDisplayString:\"WNDCS, We Need Cars Ltd., Hemel Hempstead, HP2 7GJ\",id:0},signoff:{},requestedDate:2017-05-20,requestedDateIsExact:False},vehicles:[{registration:JJ9 876,vin:09876543210987654,make:Vauxhall,model:Corsa,status:PrePickup,damageAtPickup:[],damageAtDropoff:[],photos:[],dutyOfCare:{},paperwork:{},isPickedUp:False,isDelivered:False,id:0},{registration:GG1 123,vin:12345678901234567,make:Renault,model:5,variant:GTI,notes:Pocket rocket,status:PrePickup,damageAtPickup:[],damageAtDropoff:[],photos:[],dutyOfCare:{},paperwork:{},isPickedUp:False,isDelivered:False,id:0}],history:[],documents:[],shipperId:0,contractedCarrierId:0,allocatedCarrierId:0,vehicleCount:2,vehiclesCollected:0,vehiclesDelivered:0,isVinDispatchJob:False,importDisplayString:\":TestLoad2\",id:0}]]\nServiceStack.HttpError: LoadId '' was used before on job of Id 1815\r\n at VinDeliver.ServiceModel.Services.CreateJobService.Create(Jobs request, Int32 creatingUserId, Boolean isClaimJob)",
//"errors":[]
//}
//}