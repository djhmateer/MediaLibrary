using System.Data.SqlClient;
using NUnit.Framework;

namespace DataAccessLayerTests
{
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
            //connection = new SqlConnection(ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
            connection = new SqlConnection("Data Source=DAVEXPLAPTOP;Initial Catalog=catalog;Integrated Security=True");

            connection.Open();
        }

        [TestFixtureTearDown]
        public void CloseConnection()
        {
            connection.Close();
        }
    }
}