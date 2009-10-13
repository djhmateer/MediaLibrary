using MediaLibrary;
using NUnit.Framework;

namespace ServiceInterface
{
    [TestFixture]
    public class CatalogServiceStubFixture
    {
        RecordingDataSet.Recording recording;
        // a flattened representation of a recording
        RecordingDto dto;
        CatalogServiceStub service;

        [SetUp]
        public void SetUp()
        {
            // create a test recording in memory
            //recording = CreateRecording();
            recording = InMemoryRecordingBuilder.Make();

            service = new CatalogServiceStub(recording);
            dto = service.FindByRecordingId(recording.Id);
        }

        //RecordingDataSet.Recording CreateRecording()
        //{
        //    RecordingDataSet dataSet = new RecordingDataSet();
        //    RecordingDataSet.Recording recording = dataSet.Recordings.NewRecording();
        //    recording.Id = 1;
        //    recording.Title = "test title";
        //    return recording;
        //}

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

        //[Test]
        //public void TrackCount()
        //{
        //    Assert.AreEqual(recording.GetTracks().Length,
        //        dto.tracks.Length);
        //}

        //[Test]
        //public void ReviewCount()
        //{
        //    Assert.AreEqual(recording.GetReviews().Length,
        //        dto.reviews.Length);
        //}
    }
}