using NUnit.Framework;
using MediaLibrary;

namespace Tests
{
    [TestFixture]
    public class GenreFixture : ConnectionFixture
    {
        static readonly string GenreName = "Rock";
        GenreGateway gateway;
        RecordingDataSet recordingDataSet;
        long genreId;

        [SetUp]
        public void pass_connection_to_gateway_setup_data_set()
        {
            recordingDataSet = new RecordingDataSet();
            gateway = new GenreGateway(Connection);
            genreId = gateway.Insert(recordingDataSet, GenreName);
        }

        [Test]
        public void RetrieveGenreFromDatabase()
        {
            RecordingDataSet loadedFromDB = new RecordingDataSet();
            RecordingDataSet.Genre loadedGenre = gateway.FindById(genreId, loadedFromDB);

            Assert.AreEqual(genreId, loadedGenre.Id);
            Assert.AreEqual(GenreName, loadedGenre.Name);
        }

        [Test]
        public void DeleteArtistFromDatabase()
        {
            //long artistId = gateway.Insert(new RecordingDataSet(), artistName);

            RecordingDataSet emptyDataSet = new RecordingDataSet();
            long deletedGenreID = gateway.Insert(emptyDataSet, "Deleted Genre");

            gateway.Delete(emptyDataSet, deletedGenreID);

            RecordingDataSet.Genre deletedGenre = gateway.FindById(deletedGenreID, emptyDataSet);
            Assert.IsNull(deletedGenre);
        }

        [Test]
        public void UpdateArtistAlreadyInTheDatabase()
        {
            RecordingDataSet.Genre genre = recordingDataSet.Genres[0];
            genre.Name = "Modified Name";
            gateway.Update(recordingDataSet);

            RecordingDataSet updatedDataSet = new RecordingDataSet();
            RecordingDataSet.Genre updatedGenre = gateway.FindById(genreId, updatedDataSet);
            Assert.AreEqual("Modified Name", updatedGenre.Name);
        }

        [TearDown]
        public void tear_down()
        {
            gateway.Delete(recordingDataSet, genreId);
        }
    }
}