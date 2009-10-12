using System.Data;
using System.Data.SqlClient;

namespace MediaLibrary.DAL
{
    public class IdGenerator
    {
        static public long GetNextId(string tableName, SqlConnection connection)
        {
            SqlTransaction transaction =
                connection.BeginTransaction(IsolationLevel.Serializable,
                                            "GenerateId");

            SqlCommand selectCommand = new SqlCommand(
                "select nextId from PKSequence where tableName = @tableName",
                connection, transaction);
            selectCommand.Parameters.Add("@tableName", SqlDbType.VarChar).Value = tableName;

            long nextId = (long) selectCommand.ExecuteScalar();
            SqlCommand updateCommand = new SqlCommand(
                "update PKSequence set nextId = @nextId where tableName=@tableName",
                connection, transaction);
            updateCommand.Parameters.Add("@tableName", SqlDbType.VarChar).Value = tableName;
            updateCommand.Parameters.Add("@nextId", SqlDbType.BigInt).Value = nextId + 1;
            updateCommand.ExecuteNonQuery();
            transaction.Commit();

            return nextId;
        }
    }
}