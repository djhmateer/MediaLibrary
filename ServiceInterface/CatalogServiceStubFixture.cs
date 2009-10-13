using MediaLibrary;
using NUnit.Framework;

namespace ServiceInterface
{
    [TestFixture]
    public class CatalogServiceStubFixture
    {
        RecordingDataSet.Recording recording;
        RecordingDataSet.Recording actual;
        CatalogServiceStub service;

        [SetUp]
        public void SetUp()
        {
            // create a test recording in memory
            recording = CreateRecording();
            service = new CatalogServiceStub(recording);
            actual = service.FindByRecordingId(recording.Id);
        }

        RecordingDataSet.Recording CreateRecording()
        {
            RecordingDataSet dataSet = new RecordingDataSet();
            RecordingDataSet.Recording recording = dataSet.Recordings.NewRecording();
            recording.Id = 1;
            return recording;
        }

        [Test]
        public void CheckId()
        {
            Assert.AreEqual(recording.Id, actual.Id);
        }

        //[Test]
        //public void CheckTitle()
        //{
        //    Assert.AreEqual(recording.Title, dto.title);
        //}

        [Test]
        public void InvalidId()
        {
            RecordingDataSet.Recording nullRecording = service.FindByRecordingId(2);
            Assert.IsNull(nullRecording, "should be null");
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