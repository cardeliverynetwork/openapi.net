using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CdnLink.Pull
{
    public class CdnReceivedFtpFile
    {
        public static string ConnectionString { get; set; }

        public int Id { get; private set; }

        public string Filename { get; private set; }

        public string JsonMessage { get; private set; }

        public int Status { get; private set; }

        public static List<CdnReceivedFtpFile> GetAllQueued()
        {
            // Get all received files at status 60 (Queued) 
            var sql = "SELECT f.Id, f.Filename, f.JsonMessage, r.Status FROM CdnReceives r JOIN CdnReceivedFtpFiles f ON f.Id = r.FtpFileId WHERE r.Status = 60";

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var receivedFiles = new List<CdnReceivedFtpFile>();
                    while (reader.Read())
                    {
                        var receivedFile = new CdnReceivedFtpFile
                        {
                            Id = (int)reader["Id"],
                            Filename = reader["Filename"].ToString(),
                            JsonMessage = reader["JsonMessage"].ToString(),
                            Status = (int)reader["Status"]
                        };
                        receivedFiles.Add(receivedFile);
                    }
                    return receivedFiles;
                }
            }
        }

        public void SetAsClientProcessed()
        {
            // Set update to status 80 (ClientProcessed)
            this.Status = 80;

            var sql = "UPDATE CdnReceives SET Status = @Status, SuccessDate = @SuccessDate WHERE FtpFileId = @FtpFileId";

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add("@FtpFileId", SqlDbType.Int).Value = this.Id;
                command.Parameters.Add("@Status", SqlDbType.Int).Value = this.Status;
                command.Parameters.Add("@SuccessDate", SqlDbType.DateTime).Value = DateTime.Now;
                command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Sets the update as errored
        /// </summary>
        /// <param name="errorCode">An error code for this error</param>
        /// <param name="errorMessage">Error message</param>
        public void SetAsError(string errorCode, string errorMessage)
        {
            // Set this update to status 70 (Error) and set an error code and message
            this.Status = 70;

            var sql = "UPDATE CdnReceives SET Status = @Status, FailedDate = @FailedDate, ErrorCode = @ErrorCode, ErrorMessage = @ErrorMessage WHERE FtpFileId = @FtpFileId";

            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();
                command.Parameters.Add("@FtpFileId", SqlDbType.Int).Value = this.Id;
                command.Parameters.Add("@Status", SqlDbType.Int).Value = this.Status;
                command.Parameters.Add("@FailedDate", SqlDbType.DateTime).Value = DateTime.Now;
                command.Parameters.Add("@ErrorCode", SqlDbType.NVarChar).Value = errorCode;
                command.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar).Value = errorMessage;
                command.ExecuteNonQuery();
            }
        }
    }
}
