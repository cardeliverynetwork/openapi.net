using System.Collections.Generic;
using System.IO;
using System.Net;

namespace CdnLink
{
    /// <summary>
    /// 
    /// </summary>
    public class FtpBox
    {
        private string _host;
        private string _user;
        private string _pass;

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpBox"/> class.
        /// </summary>
        /// <param name="host">The ftp host.</param>
        /// <param name="directory">The ftp directory.</param>
        /// <param name="user">The ftp user.</param>
        /// <param name="pass">The ftp pass.</param>
        public FtpBox(string host, string directory, string user, string pass)
        {
            // Clean up the directory
            if (directory != null)
                directory = directory.Trim().Trim('/');

            // Clean up the host 
            host = host.Trim().Trim('/');
            
            // Append host and directory if we have one
            _host = string.IsNullOrWhiteSpace(directory)
                ? string.Format("{0}/", host)
                : string.Format("{0}/{1}/", host, directory);

            // Clean up the credentials
            _user = user.Trim();
            _pass = pass.Trim();
        }

        /// <summary>
        /// Gets the file list.
        /// </summary>
        /// <returns></returns>
        public List<string> GetFileList()
        {
            var request = GetFtpRequest(WebRequestMethods.Ftp.ListDirectory);
            var fileList = new List<string>();
            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var filename = reader.ReadLine();
                while (!string.IsNullOrWhiteSpace(filename))
                {
                    fileList.Add(filename);
                    filename = reader.ReadLine();
                }
            }
            return fileList;
        }

        /// <summary>
        /// Gets the file contents.
        /// </summary>
        /// <param name="filename">Name of the file.</param>
        /// <returns></returns>
        public string GetFileContents(string filename)
        {
            var request = GetFtpRequest(WebRequestMethods.Ftp.DownloadFile, filename);
            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
                return reader.ReadToEnd();
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="filename">Name of the file.</param>
        public void DeleteFile(string filename)
        {
            var request = GetFtpRequest(WebRequestMethods.Ftp.DeleteFile, filename);
            using (request.GetResponse()) { };
        }

        private FtpWebRequest GetFtpRequest(string method, string filename = null)
        {
            var request = (FtpWebRequest)WebRequest.Create(_host + filename ?? "");
            request.Method = method;
            request.Credentials = new NetworkCredential(_user, _pass);
            return request;
        }
    }
}
