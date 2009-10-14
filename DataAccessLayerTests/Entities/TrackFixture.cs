using DataAccessLayer;
using NUnit.Framework;

namespace DataAccessLayerTests
{
	[TestFixture]
	public class TrackFixture : ConnectionFixture
	{
		private static readonly string title = "Title";
		private static readonly int duration = 120;

		private long trackId;
		private RecordingDataSet recordingDataSet;
		private TrackGateway gateway;

		[SetUp]
		public void SetUp()
		{
			recordingDataSet = new RecordingDataSet();
			gateway = new TrackGateway(Connection);

			trackId = gateway.Insert(recordingDataSet,title,duration);
		}

		[TearDown]
		public void TearDown()
		{
			gateway.Delete(recordingDataSet,trackId);
		}

		[Test]
		public void RetrieveTrackFromDatabase()
		{
			RecordingDataSet loadedFromDB = new RecordingDataSet();
			RecordingDataSet.Track loadedTrack = gateway.FindById(trackId, loadedFromDB);

			Assert.AreEqual(trackId, loadedTrack.Id);
			Assert.AreEqual(title, loadedTrack.Title);
			Assert.AreEqual(duration, loadedTrack.Duration);
		}

		[Test]
		public void DeleteTrackFromDatabase()
		{
			RecordingDataSet emptyDataSet = new RecordingDataSet();
			long deletedTrackId = gateway.Insert(emptyDataSet, "Deleted Title", 0);
			gateway.Delete(emptyDataSet, deletedTrackId);

			RecordingDataSet.Track deleletedTrack = gateway.FindById(deletedTrackId, emptyDataSet);
			Assert.IsNull(deleletedTrack);
		}

		[Test]
		public void UpdateTitleFieldInTrack()
		{
			string modifiedTitle = "Modified Title";

			RecordingDataSet.Track track = recordingDataSet.Tracks[0];
			track.Title = modifiedTitle;
			gateway.Update(recordingDataSet);

			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Track updatedTrack = gateway.FindById(trackId, updatedDataSet);
			Assert.AreEqual(modifiedTitle, updatedTrack.Title);
		}

		[Test]
		public void UpdateDurationFieldInTrack()
		{
			int modifiedDuration = 300; 

			RecordingDataSet.Track track = recordingDataSet.Tracks[0];
			track.Duration = modifiedDuration;
			gateway.Update(recordingDataSet);

			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Track updatedTrack = gateway.FindById(trackId, updatedDataSet);
			Assert.AreEqual(modifiedDuration, updatedTrack.Duration);
		}
	}
}