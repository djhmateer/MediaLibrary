using NUnit.Framework;

namespace MediaLibrary
{
    [TestFixture]
    public class ArtistFixture : ConnectionFixture
    {
        static readonly string artistName = "Artist";
        ArtistGateway gateway;
        RecordingDataSet recordingDataSet;
        long artistId;

        [SetUp]
        public void setup_and_open_Connection_pass_to_gateway_setup_data_set()
        {
            recordingDataSet = new RecordingDataSet();
            gateway = new ArtistGateway(Connection);
            // insert a new artist getting its ID from the database
            artistId = gateway.Insert(recordingDataSet, artistName);
        }

        [Test]
        public void RetrieveArtistFromDatabase()
        {
            // create new RDS, use same gateway.
            RecordingDataSet loadedFromDB = new RecordingDataSet();
            RecordingDataSet.Artist loadedArtist = gateway.FindById(artistId, loadedFromDB);

            Assert.AreEqual(artistId, loadedArtist.Id);
            Assert.AreEqual(artistName, loadedArtist.Name);
        }

        [Test]
        public void DeleteArtistFromDatabase()
        {
            //long artistId = gateway.Insert(new RecordingDataSet(), artistName);

            RecordingDataSet emptyDataSet = new RecordingDataSet();
            long deletedArtistID = gateway.Insert(emptyDataSet, "Deleted Artist");

            gateway.Delete(emptyDataSet, deletedArtistID);

            RecordingDataSet.Artist deletedArtist = gateway.FindById(deletedArtistID, emptyDataSet);
            Assert.IsNull(deletedArtist);
        }

        [Test]
        public void UpdateArtistAlreadyInTheDatabase()
        {
            RecordingDataSet.Artist artist = recordingDataSet.Artists[0];
            artist.Name = "Modified Name";
            gateway.Update(recordingDataSet);

            RecordingDataSet updatedDataSet = new RecordingDataSet();
            RecordingDataSet.Artist updatedArtist = gateway.FindById(artistId, updatedDataSet);
            Assert.AreEqual("Modified Name", updatedArtist.Name);
        }

        [TearDown]
        public void tear_down()
        {
            // delete the test Artist we set up in the setup method
            gateway.Delete(recordingDataSet, artistId);
        }
    }
}