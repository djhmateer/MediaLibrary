using DataAccessLayerTests;
using NUnit.Framework;
using ServiceInterface.ServiceGateway;

namespace ServiceInterface
{
    public class CatalogGatewayFixture : RecordingFixture
    {
        CatalogGateway gateway = new CatalogGateway();
        ServiceGateway.RecordingDto dto;

        [SetUp]
        public void SetUp()
        {
            base.SetUp();
            dto = gateway.FindByRecordingId(Recording.Id);
        }

        [Test]
        public void Id()
        {
            Assert.AreEqual(Recording.Id, dto.id);
        }

        [Test]
        public void Title()
        {
            Assert.AreEqual(Recording.Title, dto.title);
        }

        [Test]
        public void TrackCount()
        {
            Assert.AreEqual(0, Recording.GetTracks().Length);
            Assert.IsNull(dto.tracks);
        }

        [Test]
        public void ReviewCount()
        {
            Assert.AreEqual(0, Recording.GetReviews().Length);
            Assert.IsNull(dto.reviews);
        }

        [Test]
        public void InvalidId()
        {
            ServiceGateway.RecordingDto nullDto = gateway.FindByRecordingId(2);
            Assert.IsNull(nullDto, "should be null");
        }
    }
}