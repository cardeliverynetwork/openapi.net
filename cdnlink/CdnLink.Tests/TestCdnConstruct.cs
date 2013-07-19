using System;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestCdnConstruct
    {
        [Test]
        public void Good()
        {
            var cdn = new Cdn("ConnectionString", "ApiUrl", "ApiKey", new FtpBox("FtpHost", "FtpRoot", "FtpUser", "FtpPass"));
            Assert.AreEqual("ConnectionString", cdn.ConnectionString);
            Assert.AreEqual("ApiUrl", cdn.ApiUrl);
            Assert.AreEqual("ApiKey", cdn.ApiKey);
            Assert.AreEqual("FtpHost", cdn.FtpBox.Host);
            Assert.AreEqual("FtpRoot", cdn.FtpBox.Root);
            Assert.AreEqual("FtpUser", cdn.FtpBox.User);
            Assert.AreEqual("FtpPass", cdn.FtpBox.Pass);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullConnectionString()
        {
            new Cdn(null, "0", "0", new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullApiUrl()
        {
            new Cdn("0", null, "0", new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullApiKey()
        {
            new Cdn("0", "0", null, new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpHost()
        {
            new Cdn("0", "0", "0", new FtpBox(null, "0", "0", "0"));
        }

        [Test]
        public void NullFtpRoot()
        {
            // We can have a null FtpRoot
            new Cdn("0", "0", "0", new FtpBox("0", null, "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpUser()
        {
            new Cdn("0", "0", "0", new FtpBox("0", "0", null, "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpPass()
        {
            new Cdn("0", "0", "0", new FtpBox("0", "0", "0", null));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyConnectionString()
        {
            new Cdn(string.Empty, "0", "0", new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyApiUrl()
        {
            new Cdn("0", string.Empty, "0", new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyApiKey()
        {
            new Cdn("0", "0", string.Empty, new FtpBox("0", "0", "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpHost()
        {
            new Cdn("0", "0", "0", new FtpBox(string.Empty, "0", "0", "0"));
        }

        [Test]
        public void EmptyFtpRoot()
        {
            // We can have an Empty FtpRoot
            new Cdn("0", "0", "0", new FtpBox("0", string.Empty, "0", "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpUser()
        {
            new Cdn("0", "0", "0", new FtpBox("0", "0", string.Empty, "0"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpPass()
        {
            new Cdn("0", "0", "0", new FtpBox("0", "0", "0", string.Empty));
        }
    }
}
