using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace MediaLibrary
{
    [TestFixture]
    public class IdGeneratorFixture : ConnectionFixture
    {
        [Test]
        public void GetNextIdIncrement()
        {
            SqlCommand sqlCommand = new SqlCommand("select nextId from PKSequence where tableName = @tableName", Connection);
            sqlCommand.Parameters.Add("@tableName", SqlDbType.VarChar).Value = "Artist";

            long nextId = (long) sqlCommand.ExecuteScalar();
            long nextIdFromGenerator = IdGenerator.GetNextId("Artist", Connection);
            Assert.AreEqual(nextId, nextIdFromGenerator);
            nextId = (long) sqlCommand.ExecuteScalar();
            Assert.AreEqual(nextId, nextIdFromGenerator + 1);
        }
    }
}