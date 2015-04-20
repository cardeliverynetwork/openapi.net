using System;
using CarDeliveryNetwork.Api.Data;

namespace CdnLink.Pull
{
    /// <summary>
    /// Example app for pulling data from CdnLink into an instance of CarDeliveryNetwork.Api.Data.Job
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CdnReceivedFtpFile.ConnectionString = "Data Source=localhost;Initial Catalog=CdnLink;uid=CdnLinkUsr;pwd=CdnLinkPasswd";

            // Get the list of updates to process
            var queuedUpdates = CdnReceivedFtpFile.GetAllQueued();

            foreach (var update in queuedUpdates)
            {
                try
                {
                    // Deserialize the file contents into a job (load) object
                    var job = Job.FromString(update.JsonMessage);


                    //
                    // PUT YOUR CODE HERE TO PROCESS THE JOB UPDATE
                    //
                    // For CarDeliveryNetwork.Api.Data.Job docs, see:
                    // http://docs.cardeliverynetwork.com/?topic=html/T_CarDeliveryNetwork_Api_Data_Job.htm
                    //


                    // Mark the update as processed by the client
                    update.SetAsClientProcessed();
                }
                catch (Exception ex)
                {
                    // Mark this update as errored during processing
                    update.SetAsError("ABC123", ex.Message);

                    // May want to re-throw here to exit, or continue processing other updates
                    
                    // throw;
                }
            }
        }
    }
}
