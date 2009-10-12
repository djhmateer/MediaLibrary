using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;

namespace MediaLibrary.DAL
{
    [TestFixture]
    public class GenreFixture
    {
        static readonly string GenreName = "Rock";
        SqlConnection connection;
        GenreGateway gateway;
        RecordingDataSet recordingDataSet;
        long genreId;

        [SetUp]
        public void setup_and_open_connection_pass_to_gateway_setup_data_set()
        {
            connection = new SqlConnection(ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
            connection.Open();

            recordingDataSet = new RecordingDataSet();
            gateway = new GenreGateway(connection);
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
            connection.Close();
        }
    }
}