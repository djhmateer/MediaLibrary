namespace DataAccessLayerTests
{
    public class blah
    {
        	[TestFixture]
	public class TrackRecordingFixture : RecordingFixture
	{
		private TrackGateway trackGateway;
		private long trackId;

		[SetUp]
		public new void SetUp()
		{
			base.SetUp();

			trackGateway = new TrackGateway(Connection);
			trackId = trackGateway.Insert(RecordingDataSet, "Track", 120);
			RecordingDataSet.Track track = trackGateway.FindById(trackId, RecordingDataSet);
			track.Recording = Recording;
			trackGateway.Update(RecordingDataSet);
		}

		[TearDown]
		public new void TearDown()
		{
			trackGateway.Delete(RecordingDataSet,trackId);
			base.TearDown();
		}

		[Test]
		public void Count()
		{
			Assert.AreEqual(1, Recording.GetTracks().Length);
		}

		[Test]
		public void ParentId()
		{
			foreach(RecordingDataSet.Track track in Recording.GetTracks())
			{
				Assert.AreEqual(Recording.Id, track.RecordingId);
			}
		}

    }
}