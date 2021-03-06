using DataAccessLayer;
using DataModel;
using NUnit.Framework;

namespace DataAccessLayerTests
{
    [TestFixture]
    public class TrackRecordingFixture : RecordingFixture
    {
        TrackGateway trackGateway;
        long newTrackId;

        [SetUp]
        public void setup_a_track_duration_120s_and_write_to_db()
        {
            base.SetUp();
            trackGateway = new TrackGateway(Connection);
            newTrackId = trackGateway.Insert(RecordingDataSet, "Track", 120);
            RecordingDataSet.Track inMemoryDataset_table_track = trackGateway.FindById(newTrackId, RecordingDataSet);
            // link the sample_recording to the track we just created
            inMemoryDataset_table_track.Recording = Recording;
            trackGateway.Update(RecordingDataSet);
        }

        [TearDown]
        public void delete_track_from_db()
        {
            trackGateway.Delete(RecordingDataSet, newTrackId);
            // taken this out otherwise get a nulrefexception.
            //base.TearDown();
        }

        [Test]
        public void number_of_tracks_in_the_recording_should_be_1()
        {
            Assert.AreEqual(1, Recording.GetTracks().Length);
        }

        [Test]
        public void ParentId()
        {
            foreach (RecordingDataSet.Track track in Recording.GetTracks())
            {
                Assert.AreEqual(Recording.Id, track.RecordingId);
            }
        }

    }
}