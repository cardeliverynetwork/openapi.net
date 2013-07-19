using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using Moq;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestReceive
    {
        [Test]
        [ExpectedException(typeof(SqlException))]
        public void DbNoBadCatalog()
        {
            new Cdn("Data Source=localhost;Initial Catalog=BAD;uid=CdnLinkTestUsr;pwd=password", "0", "0", GetFtpBox(true)).Receive();
        }

        [Test]
        [ExpectedException(typeof(SqlException))]
        public void DbBadUser()
        {
            new Cdn("Data Source=localhost;Initial Catalog=CdnLinkTest;uid=BAD;pwd=password", "0", "0", GetFtpBox(true)).Receive();
        }

        [Test]
        [ExpectedException(typeof(SqlException))]
        public void DbBadPass()
        {
            new Cdn("Data Source=localhost;Initial Catalog=CdnLinkTest;uid=CdnLinkTestUsr;pwd=BAD", "0", "0", GetFtpBox(true)).Receive();
        }

        /// <summary>
        /// This test is very very slow while we wait for teh request to timeout
        /// </summary>
        [Test]
        [ExpectedException(typeof(WebException))]
        public void FtpBadHost()
        {
            new Cdn(
                "Data Source=localhost;Initial Catalog=CdnLinkTest;uid=CdnLinkTestUsr;pwd=password",
                "123",
                "123",
                new FtpBox("ftp://ftp.example.com", "/", "example", "example")).Receive();
        }

        [Test]
        public void Receive()
        {
            var connectionString = "Data Source=localhost;Initial Catalog=CdnLinkTest;uid=CdnLinkTestUsr;pwd=password";
            var ftp = GetFtpBox(true);
            var fileNames = new List<string>(ftp.GetFileList());
            var cdn = new Cdn(connectionString,"0","0",ftp);

            Assert.AreEqual(fileNames.Count, cdn.Receive());
            Assert.AreEqual(0, cdn.Receive());

            var db = new CdnLinkDataContext(connectionString);

            foreach (var filename in fileNames)
            {
                var ftpFile = db.CdnReceivedFtpFiles.Where(f => f.Filename.Contains(filename)).Single();
                Assert.IsNotNull(ftpFile);
                Assert.AreEqual(ftp.GetFileContents(filename), ftpFile.JsonMessage);

                var receive = ftpFile.CdnReceive;
                Assert.IsNotNull(receive);
                Assert.AreEqual((int)CdnReceives.ReceiveStatus.Queued, receive.Status);
                Assert.AreEqual(DateTime.Today, receive.FetchedDate.Date);

                var load = ftpFile.CdnReceivedLoad;
                Assert.IsNotNull(load);
                Assert.AreNotEqual(0, load.CdnId);

                var vehicles = load.CdnReceivedVehicles;
                Assert.IsNotNull(vehicles);
                Assert.Greater(vehicles.Count(), 0);
            }
        }

        
        public ICdnFtpBox GetFtpBox(bool hasFiles)
        {
            var testFiles = hasFiles
                ? Directory.GetFiles("FtpFiles").ToList()
                : new List<string>();

            var files = testFiles.ToDictionary(f => Guid.NewGuid().ToString(), f => File.ReadAllText(f));
            var fileNames = files.Select(f => f.Key).ToList();

            var mock = new Mock<ICdnFtpBox>();

            mock.Setup(s => s.GetFileList())
                .Returns(fileNames);

            mock.Setup(s => s.GetFileContents(It.IsAny<string>()))
                .Returns((string s) => files.ContainsKey(s) ? files[s] : null);
            
            mock.Setup(s => s.DeleteFile(It.IsAny<string>()))
                .Callback((string s) => fileNames.Remove(s));
            
            return mock.Object;
        }
    }
}
