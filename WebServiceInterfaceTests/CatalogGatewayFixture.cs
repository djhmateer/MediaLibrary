using DataAccessLayer;
using NUnit.Framework;
using DataAccessLayerTests;
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
                    dto = gateway.FindByRecordingId(Sample_recording.Id);
                }

                [Test]
                public void Id()
                {
                    Assert.AreEqual(Sample_recording.Id, dto.id);
                }

                [Test]
                public void Title()
                {
                    Assert.AreEqual(Sample_recording.Title, dto.title);
                }

                [Test]
                public void TrackCount()
                {
                    Assert.AreEqual(0, Sample_recording.GetTracks().Length);
                    Assert.IsNull(dto.tracks);
                }

                [Test]
                public void ReviewCount()
                {
                    Assert.AreEqual(0, Sample_recording.GetReviews().Length);
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
