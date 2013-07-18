using System;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestCdnConstruct
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullConnectionString()
        {
            new Cdn(null, "0", "0", "0", "0", "0", "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullApiUrl()
        {
            new Cdn("0", null, "0", "0", "0", "0", "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullApiKey()
        {
            new Cdn("0", "0", null, "0", "0", "0", "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpHost()
        {
            new Cdn("0", "0", "0", null, "0", "0", "0");
        }

        [Test]
        public void NullFtpRoot()
        {
            // We can have a null FtpRoot
            new Cdn("0", "0", "0", "0", null, "0", "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpUser()
        {
            new Cdn("0", "0", "0", "0", "0", null, "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NullFtpPass()
        {
            new Cdn("0", "0", "0", "0", "0", "0", null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyConnectionString()
        {
            new Cdn(string.Empty, "0", "0", "0", "0", "0", "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyApiUrl()
        {
            new Cdn("0", string.Empty, "0", "0", "0", "0", "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyApiKey()
        {
            new Cdn("0", "0", string.Empty, "0", "0", "0", "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpHost()
        {
            new Cdn("0", "0", "0", string.Empty, "0", "0", "0");
        }

        [Test]
        public void EmptyFtpRoot()
        {
            // We can have an Empty FtpRoot
            new Cdn("0", "0", "0", "0", string.Empty, "0", "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpUser()
        {
            new Cdn("0", "0", "0", "0", "0", string.Empty, "0");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyFtpPass()
        {
            new Cdn("0", "0", "0", "0", "0", "0", string.Empty);
        }
    }
}
