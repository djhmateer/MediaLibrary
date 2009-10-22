using DataAccessLayer;
using DataModel;
using NUnit.Framework;
using DataAccessLayerTests;

namespace ServiceInterface
{
    // RF inherits off connection
    [TestFixture]
    public class DatabaseCatalogServiceFixture : RecordingFixture
    {
        DatabaseCatalogService service;
        RecordingDto dto;
        TrackGateway trackGateway;
        long newTrackId;

        [SetUp]
        public void SetUp()
        {
            base.SetUp();
 
            service = new DatabaseCatalogService();
            dto = service.FindByRecordingId(Recording.Id);
        }

        [Test]
        public void check_id_from_the_sample_recording_is_the_same_as_the_one_from_the_dto_after_it_has_been_processed()
        {
            Assert.AreEqual(Recording.Id, dto.id);
        }

        [Test]
        public void check_artist_name()
        {
            Assert.AreEqual(Recording.Artist.Name, dto.artistName);
        }

        //[Test]
        //public void check_first_track_is_duration_120()
        //{
        //    RecordingDataSet.Track[] tracks = Recording.GetTracks();
        //    Assert.AreEqual(tracks[0].Duration, dto.tracks[0].duration);
        //}

        //[Test]
        //public void total_duration_of_tracks()
        //{
        //    int i  = dto.totalRunTime;
        //    Assert.AreEqual(100, i);
        //}

    }
}