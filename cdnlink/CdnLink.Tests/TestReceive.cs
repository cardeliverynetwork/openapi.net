using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestReceive
    {
        [Test]
        [ExpectedException]
        public void Receive_NoDb()
        {
            
        }

        [Test]
        [ExpectedException]
        public void Receive_NoFtp()
        {

        }
    }
}
