using CarDeliveryNetwork.Api.ClientProxy;
using CarDeliveryNetwork.Api.Data;
using Moq;
using NUnit.Framework;

namespace CdnLink.Tests
{
    [TestFixture]
    public class TestSend
    {
        [TestFixtureSetUp]
        public void Init()
        {
            var db = new CdnLinkDataContext(Settings.GetConnectionString());
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
            var link = new CdnLink(Settings.GetConnectionString(), GetMockApi(), null);
            link.Send();
        }

        private ICdnApi GetMockApi()
        {
            var mock = new Mock<ICdnApi>();

            mock.Setup(s => s.CreateJob(It.IsAny<Job>()))
                .Returns((Job j) => j);

            return mock.Object;
        }
    }
}