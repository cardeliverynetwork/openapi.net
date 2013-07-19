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
        [TestFixtureSetUp]
        public void Init()
        {
            var db = new CdnLinkDataContext(Settings.GetConnetionString());
            db.CdnReceivedDamages.DeleteAllOnSubmit(db.CdnReceivedDamages);
            db.SubmitChanges();

            db.CdnReceivedVehicles.DeleteAllOnSubmit(db.CdnReceivedVehicles);
            db.SubmitChanges();

            db.CdnReceivedDocuments.DeleteAllOnSubmit(db.CdnReceivedDocuments);
            db.SubmitChanges();

            db.CdnReceives.DeleteAllOnSubmit(db.CdnReceives);
            db.SubmitChanges();

            db.CdnReceivedLoads.DeleteAllOnSubmit(db.CdnReceivedLoads);
            db.SubmitChanges();

            db.CdnReceivedFtpFiles.DeleteAllOnSubmit(db.CdnReceivedFtpFiles);
            db.SubmitChanges();
        }

        /// <summary>
        /// This test is very very slow while we wait for teh request to timeout
        /// </summary>
        [Test]
        [Ignore]
        [ExpectedException(typeof(SqlException))]
        public void DbBadHost()
        {
            var connectionString = Settings.GetConnetionString(
                "BAD",
                Settings.DbInitialCatalog,
                Settings.DbUser,
                Settings.DbPass);
            new Cdn(connectionString, "0", "0", GetFtpBox(true)).Receive();
        }

        [Test]
        [ExpectedException(typeof(SqlException))]
        public void DbBadCatalog()
        {
            var connectionString = Settings.GetConnetionString(
                Settings.DbDataSource,
                "BAD",
                Settings.DbUser,
                Settings.DbPass);
            new Cdn(connectionString, "0", "0", GetFtpBox(true)).Receive();
        }

        [Test]
        [ExpectedException(typeof(SqlException))]
        public void DbBadUser()
        {
            var connectionString = Settings.GetConnetionString(
                Settings.DbDataSource,
                Settings.DbInitialCatalog,
                "BAD",
                Settings.DbPass);
            new Cdn(connectionString, "0", "0", GetFtpBox(true)).Receive();
        }

        [Test]
        [ExpectedException(typeof(SqlException))]
        public void DbBadPass()
        {
            var connectionString = Settings.GetConnetionString(
                Settings.DbDataSource,
                Settings.DbInitialCatalog,
                Settings.DbUser,
                "BAD");
            new Cdn(connectionString, "0", "0", GetFtpBox(true)).Receive();
        }

        /// <summary>
        /// This test is very very slow while we wait for teh request to timeout
        /// </summary>
        [Test]
        [Ignore]
        [ExpectedException(typeof(WebException))]
        public void FtpBadHost()
        {
            new Cdn(
                Settings.GetConnetionString(),
                "0",
                "0",
                new FtpBox("ftp://ftp.example.com", "/", "example", "example")).Receive();
        }

        [Test]
        public void Receive()
        {
            var connectionString = Settings.GetConnetionString();
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
