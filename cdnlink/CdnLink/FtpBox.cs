using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace CdnLink
{
    /// <summary>
    /// 
    /// </summary>
    public class FtpBox : ICdnFtpBox
    {
        public string Host { get; private set; }
        public string Root { get; private set; }
        public string User { get; private set; }
        public string Pass { get; private set; }

        private string FullPath
        {
            get
            {
                var host = Host.Trim().Trim('/');

                var root = (Root == null)
                    ? null
                    : Root.Trim().Trim('/');

                return string.IsNullOrWhiteSpace(root)
                    ? string.Format("{0}/", host)
                    : string.Format("{0}/{1}/", host, root);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FtpBox"/> class.
        /// </summary>
        /// <param name="host">The ftp host.</param>
        /// <param name="root">The ftp directory.</param>
        /// <param name="user">The ftp user.</param>
        /// <param name="pass">The ftp pass.</param>
        public FtpBox(string host, string root, string user, string pass)
        {
            if (string.IsNullOrWhiteSpace(host))
                throw new ArgumentException("host cannot be null or empty");
            if (string.IsNullOrWhiteSpace(user))
                throw new ArgumentException("user cannot be null or empty");
            if (string.IsNullOrWhiteSpace(pass))
                throw new ArgumentException("pass cannot be null or empty");

            Host = host;
            Root = root;
            User = user;
            Pass = pass;
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
                    if (filename != "." && filename != "..")
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
            using (request.GetResponse()) { }
        }

        private FtpWebRequest GetFtpRequest(string method, string filename = null)
        {
            var request = (FtpWebRequest)WebRequest.Create(FullPath + filename);
            request.Method = method;
            request.Credentials = new NetworkCredential(User, Pass);
            return request;
        }
    }
}
