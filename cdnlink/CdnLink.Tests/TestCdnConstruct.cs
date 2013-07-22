using System;
using CarDeliveryNetwork.Api.ClientProxy;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestCdnConstruct
    {
        [Test]
        public void Good()
        {
            var cdn = new CdnLink(
                "ConnectionString",
                new OpenApi("ApiUrl", "ApiKey"),
                new FtpBox("FtpHost", "FtpRoot", "FtpUser", "FtpPass"));

            Assert.AreEqual("ConnectionString", cdn.ConnectionString);
            Assert.AreEqual("ApiUrl", cdn.Api.Uri);
            Assert.AreEqual("ApiKey", cdn.Api.ApiKey);
            Assert.AreEqual("FtpHost", cdn.FtpBox.Host);
            Assert.AreEqual("FtpRoot", cdn.FtpBox.Root);
            Assert.AreEqual("FtpUser", cdn.FtpBox.User);
            Assert.AreEqual("FtpPass", cdn.FtpBox.Pass);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullConnectionString()
        {
            new CdnLink(null, new OpenApi("0", "0"), new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullApiUrl()
        {
            new CdnLink("0", new OpenApi(null, "0"), new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullApiKey()
        {
            new CdnLink("0", new OpenApi("0", null), new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpHost()
        {
            new CdnLink("0", new OpenApi("0", "0"), new FtpBox(null, "0", "0", "0"));
        }

        [Test]
        public void NullFtpRoot()
        {
            // We can have a null FtpRoot
            new CdnLink("0", new OpenApi("0", "0"), new FtpBox("0", null, "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpUser()
        {
            new CdnLink("0", new OpenApi("0", "0"), new FtpBox("0", "0", null, "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpPass()
        {
            new CdnLink("0", new OpenApi("0", "0"), new FtpBox("0", "0", "0", null));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyConnectionString()
        {
            new CdnLink(string.Empty, new OpenApi("0", "0"), new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyApiUrl()
        {
            new CdnLink("0", new OpenApi(string.Empty, "0"), new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyApiKey()
        {
            new CdnLink("0", new OpenApi("0", string.Empty), new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpHost()
        {
            new CdnLink("0", new OpenApi("0", "0"), new FtpBox(string.Empty, "0", "0", "0"));
        }

        [Test]
        public void EmptyFtpRoot()
        {
            // We can have an Empty FtpRoot
            new CdnLink("0", new OpenApi("0", "0"), new FtpBox("0", string.Empty, "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpUser()
        {
            new CdnLink("0", new OpenApi("0", "0"), new FtpBox("0", "0", string.Empty, "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpPass()
        {
            new CdnLink("0", new OpenApi("0", "0"), new FtpBox("0", "0", "0", string.Empty));
        }

        [Test]
        public void NullCdn()
        {
            new CdnLink("0", null, new FtpBox("0", "0", "0", "0"));
        }

        public void NullFtpBox()
        {
            new CdnLink("0", new OpenApi("0", "0"), null);
        }
    }
}
