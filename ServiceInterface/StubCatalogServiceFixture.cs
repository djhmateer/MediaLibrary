using MediaLibrary;
using NUnit.Framework;

namespace ServiceInterface
{
    [TestFixture]
    public class StubCatalogServiceFixture
    {
        RecordingDataSet.Recording recording;
        //private RecordingDto dto;
        RecordingDataSet.Recording actual;
        CatalogServiceStub service;

        [SetUp]
        public void SetUp()
        {
            //recording = InMemoryRecordingBuilder.Make();
            recording = CreateRecording();
            //service = new StubCatalogService(recording);
            service = new CatalogServiceStub(recording);
            //dto = service.FindByRecordingId(recording.Id);
            actual = service.FindByRecordingId(recording.Id);
        }

        RecordingDataSet.Recording CreateRecording()
        {
            RecordingDataSet dataSet = new RecordingDataSet();
            RecordingDataSet.Recording recording = dataSet.Recordings.NewRecording();
            recording.Id = 1;
            // more code needed here to fill in the rest of the recording.

            return recording;
        }

        [Test]
        public void CheckId()
        {
            //Assert.AreEqual(recording.Id, dto.id);
            Assert.AreEqual(recording.Id, actual.Id);
        }

        //[Test]
        //public void CheckTitle()
        //{
        //    Assert.AreEqual(recording.Title, dto.title);
        //}

        //[Test]
        //public void InvalidId()
        //{
        //    RecordingDto nullDto = service.FindByRecordingId(2);
        //    Assert.IsNull(nullDto, "should be null");
        //}

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