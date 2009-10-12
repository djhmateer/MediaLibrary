using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace MediaLibrary.DAL
{
    [TestFixture]
    public class IdGeneratorFixture
    {
        SqlConnection connection;

        [SetUp]
        public void open_connection()
        {
            connection = new SqlConnection(ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
            connection.Open();
        }

        [Test]
        public void GetNextIdIncrement()
        {
            SqlCommand sqlCommand = new SqlCommand("select nextId from PKSequence where tableName = @tableName", connection);
            sqlCommand.Parameters.Add("@tableName", SqlDbType.VarChar).Value = "Artist";

            long nextId = (long) sqlCommand.ExecuteScalar();
            long nextIdFromGenerator = IdGenerator.GetNextId("Artist", connection);
            Assert.AreEqual(nextId, nextIdFromGenerator);
            nextId = (long) sqlCommand.ExecuteScalar();
            Assert.AreEqual(nextId, nextIdFromGenerator + 1);
        }

        [TearDown]
        public void close_connection()
        {
            connection.Close();
        }
    }
}