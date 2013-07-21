using System.Data.SqlClient;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestSend
    {
        [TestFixtureSetUp]
        public void Init()
        {
            var db = new CdnLinkDataContext(Settings.GetConnetionString());
            db.CdnSendVehicles.DeleteAllOnSubmit(db.CdnSendVehicles);
            db.SubmitChanges();

            db.CdnSendLoads.DeleteAllOnSubmit(db.CdnSendLoads);
            db.SubmitChanges();

            db.CdnSends.DeleteAllOnSubmit(db.CdnSends);
            db.SubmitChanges();
        }

        [Test]
        public void Send()
        {
 
        }
    }
}