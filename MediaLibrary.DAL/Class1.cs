using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace MediaLibrary.DAL
{
    [TestFixture]
    public class SqlConnectionFixture
    {
        [Test]
        public void ConnectionIsOpen()
        {
            string connectionString = ConfigurationSettings.AppSettings.Get("Catalog.Connection");
            Assert.IsNotNull(connectionString);
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Assert.AreEqual(ConnectionState.Open, connection.State);
            connection.Close();
        }
    }
}