using DataModel;
using NUnit.Framework;

namespace ServiceInterface
{
    [TestFixture]
    public class CatalogServiceStubFixture
    {
        RecordingDataSet.Recording recording;
        // a flattened representation of a recording
        RecordingDto dto;
        StubCatalogService service;

        [SetUp]
        public void SetUp()
        {
            // create a test recording in memory
            recording = InMemoryRecordingBuilder.Make();

            service = new StubCatalogService(recording);
            dto = service.FindByRecordingId(recording.Id);
        }

        [Test]
        public void CheckId()
        {
            Assert.AreEqual(recording.Id, dto.id);
        }

        [Test]
        public void InvalidId()
        {
            RecordingDto nullDto = service.FindByRecordingId(2);
            Assert.IsNull(nullDto, "should be null");
        }

        [Test]
        public void CheckTitle()
        {
            Assert.AreEqual(recording.Title, dto.title);
        }

        [Test]
        public void TrackCount()
        {
            Assert.AreEqual(recording.GetTracks().Length, dto.tracks.Length);
            Assert.AreEqual(3, dto.tracks.Length);
        }

        [Test]
        public void total_track_duration_should_be_300_secs()
        {
            RecordingDataSet.Track[] loadedTracks = recording.GetTracks();
            int duration = 0;
            foreach (RecordingDataSet.Track track in loadedTracks)
            {
                duration += track.Duration;
            }
            Assert.AreEqual(duration, dto.totalRunTime);
            Assert.AreEqual(300, dto.totalRunTime);
        }

        [Test]
        public void ReviewCount()
        {
            Assert.AreEqual(recording.GetReviews().Length, dto.reviews.Length);
        }
    }
}