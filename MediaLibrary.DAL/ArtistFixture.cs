using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;

namespace MediaLibrary.DAL
{
    [TestFixture]
    public class ArtistFixture
    {
        [Test]
        public void RetrieveArtistFromDatabase()
        {
            string artistName = "Artist";
            SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
            connection.Open();

            ArtistGateway gateway = new ArtistGateway(connection);
            long artistId = gateway.Insert(new RecordingDataSet(), artistName);
            RecordingDataSet loadedFromDB = new RecordingDataSet();
            RecordingDataSet.Artist loadedArtist = gateway.FindById(artistId, loadedFromDB);

            Assert.AreEqual(artistId, loadedArtist.Id);
            Assert.AreEqual(artistName, loadedArtist.Name);
         
            gateway.Delete(loadedFromDB, artistId);
            connection.Close();
        }

        [Test]
        public void DeleteArtistFromDatabase() {
            string artistName = "Artist";
            SqlConnection connection = new SqlConnection(ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
            connection.Open();

            ArtistGateway gateway = new ArtistGateway(connection);
            long artistId = gateway.Insert(new RecordingDataSet(), artistName);

            RecordingDataSet emptyDataSet = new RecordingDataSet();
            long deletedArtistID = gateway.Insert(emptyDataSet, "Deleted Artist");
            gateway.Delete(emptyDataSet, deletedArtistID);

            RecordingDataSet.Artist deletedArtist = gateway.FindById(deletedArtistID, emptyDataSet);
            Assert.IsNull(deletedArtist);
            connection.Close();
        }

    }
}