using System.Configuration;
using System.Data.SqlClient;
using NUnit.Framework;

namespace MediaLibrary.DAL
{
    [TestFixture]
    public abstract class ConnectionFixture
    {
        SqlConnection connection;

        public SqlConnection Connection
        {
            get { return connection; }
        }

        [TestFixtureSetUp]
        public void OpenConnection()
        {
            connection = new SqlConnection(ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
            connection.Open();
        }

        [TestFixtureTearDown]
        public void CloseConnection()
        {
            connection.Close();
        }
    }
}