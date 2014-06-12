using System;
using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using Moq;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestCdnConstruct : TestBase
    {
        [Test]
        public void Good()
        {
            var cdn = new CdnLink(
                "ConnectionString",
                GetMockCdnApi(),
                new FtpBox("FtpHost", "FtpRoot", "FtpUser", "FtpPass"));

            Assert.AreEqual("ConnectionString", cdn.ConnectionString);
            Assert.AreEqual("FtpHost", cdn.FtpBox.Host);
            Assert.AreEqual("FtpRoot", cdn.FtpBox.Root);
            Assert.AreEqual("FtpUser", cdn.FtpBox.User);
            Assert.AreEqual("FtpPass", cdn.FtpBox.Pass);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullConnectionString()
        {
            new CdnLink(null, GetMockCdnApi(), new FtpBox("0", "0", "0", "0"));
        }


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpHost()
        {
            new CdnLink("0", GetMockCdnApi(), new FtpBox(null, "0", "0", "0"));
        }

        [Test]
        public void NullFtpRoot()
        {
            // We can have a null FtpRoot
            new CdnLink("0", GetMockCdnApi(), new FtpBox("0", null, "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpUser()
        {
            new CdnLink("0", GetMockCdnApi(), new FtpBox("0", "0", null, "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpPass()
        {
            new CdnLink("0", GetMockCdnApi(), new FtpBox("0", "0", "0", null));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyConnectionString()
        {
            new CdnLink(string.Empty, GetMockCdnApi(), new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpHost()
        {
            new CdnLink("0", GetMockCdnApi(), new FtpBox(string.Empty, "0", "0", "0"));
        }

        [Test]
        public void EmptyFtpRoot()
        {
            // We can have an Empty FtpRoot
            new CdnLink("0", GetMockCdnApi(), new FtpBox("0", string.Empty, "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpUser()
        {
            new CdnLink("0", GetMockCdnApi(), new FtpBox("0", "0", string.Empty, "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpPass()
        {
            new CdnLink("0", GetMockCdnApi(), new FtpBox("0", "0", "0", string.Empty));
        }
    }
}
