using System;
using System.Data;
using System.Data.SqlClient;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class SimpleDatabaseTests
    {
        SqlConnection connection;

        [SetUp]
        public void setup_the_connection_string()
        {
            connection = new SqlConnection("Data Source=(local);Initial Catalog=catalog;Integrated Security=SSPI");
            //connection = new SqlConnection("Data Source=DAVEXPLAPTOP;Initial Catalog=catalog;uid=dbconnectuser;pwd=letmein");
        }

        [Test]
        public void do_a_simple_read_using_SqlClient()
        {
            SqlDataReader reader = null;
            connection.Open();

            SqlCommand command = new SqlCommand("select top 1 id from Artist", connection);
            reader = command.ExecuteReader();

            //int first_artist_id = 0;
            object first_artist_id = null;
            while (reader.Read())
                first_artist_id = reader[0];

            Assert.IsNotNull(first_artist_id);

            reader.Close();
            connection.Close();
        }

        [Test]
        public void Insertdata()
        {
            connection.Open();

            string insertString =
                @"
                 insert into Artist
                 (id, name)
                 values ('12345', 'Dave')";

            SqlCommand cmd = new SqlCommand(insertString, connection);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        [Test]
        public void UpdateData()
        {
            connection.Open();

            string updateString = @"
                 update Artist
                 set Name = 'Other'
                 where Name = 'Dave'";

            SqlCommand cmd = new SqlCommand(updateString);

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        [Test]
        public void DeleteData()
        {
            connection.Open();

            string deleteString = @"
                 delete from Artist
                 where Name = 'Other'";

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = deleteString;

            cmd.Connection = connection;
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        [Test]
        public void GetNumberOfRecords()
        {
            int count = -1;
            connection.Open();
            SqlCommand cmd = new SqlCommand("select count(*) from Artist", connection);
            count = (int) cmd.ExecuteScalar();
            Assert.Greater(count,1);
            connection.Close();
        }

        [Test]
        public void put_data_into_a_dataset()
        {
            DataSet dataset = new DataSet();
            SqlDataAdapter data_adapter = new SqlDataAdapter("select id, name from Artist", connection);
            SqlCommandBuilder sql_command_builder = new SqlCommandBuilder(data_adapter);
            data_adapter.Fill(dataset, "Artist");
            Assert.IsNotNull(dataset);

            DataTable data_table = dataset.Tables[0];
            for (int i = 0; i < data_table.Rows.Count; i++)
            {
                DataRow data_row = data_table.Rows[i];
                Console.Out.Write(data_row["id"] + " ");
                Console.Out.WriteLine(data_row["name"]);
            }
        }
    }
}