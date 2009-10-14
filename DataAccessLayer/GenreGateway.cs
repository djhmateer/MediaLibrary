using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class GenreGateway
    {
        SqlDataAdapter adapter;
        SqlConnection connection;
        SqlCommand command;
        SqlCommandBuilder builder;

        public GenreGateway(SqlConnection connection)
        {
            this.connection = connection;

            command = new SqlCommand("select id, name from genre where id = @id",connection);
            command.Parameters.Add("@id", SqlDbType.BigInt);

            adapter = new SqlDataAdapter(command);
            builder = new SqlCommandBuilder(adapter);
        }

        public long Insert(RecordingDataSet recordingDataSet, string genreName)
        {
            long genreId = IdGenerator.GetNextId(recordingDataSet.Genres.TableName, connection);

            RecordingDataSet.Genre genreRow = recordingDataSet.Genres.NewGenre();
            genreRow.Id = genreId;
            genreRow.Name = genreName;
            recordingDataSet.Genres.AddGenre(genreRow);

            adapter.Update(recordingDataSet, recordingDataSet.Genres.TableName);

            return genreId;
        }

        public RecordingDataSet.Genre FindById(long genreId, RecordingDataSet recordingDataSet)
        {
            command.Parameters["@id"].Value = genreId;
            adapter.Fill(recordingDataSet, recordingDataSet.Genres.TableName);
            DataRow[] rows = recordingDataSet.Genres.Select(String.Format("id={0}", genreId));

            if (rows.Length < 1) return null;
            return (RecordingDataSet.Genre) rows[0];
        }

        public void Delete(RecordingDataSet recordingDataSet, long genreId)
        {
            RecordingDataSet.Genre loadedgenre = FindById(genreId, recordingDataSet);
            loadedgenre.Delete();
            adapter.Update(recordingDataSet, recordingDataSet.Genres.TableName);
        }

        public void Update(RecordingDataSet recordingDataSet)
        {
            adapter.Update(recordingDataSet, recordingDataSet.Genres.TableName);
        }

    }
}