using System;
using System.Configuration;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestHelpers
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetSetting_Null()
        {
            Helpers.GetSetting(null);
        }

        [Test]
        [ExpectedException(typeof(SettingsPropertyNotFoundException))]
        public void GetSetting_Empty()
        {
            Helpers.GetSetting("");
        }
    }
}
