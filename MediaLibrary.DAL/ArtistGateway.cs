using System;
using System.Data;
using System.Data.SqlClient;

namespace MediaLibrary.DAL
{
    public class ArtistGateway
    {
        SqlDataAdapter adapter;
        SqlConnection connection;
        SqlCommand command;
        SqlCommandBuilder builder;

        public ArtistGateway(SqlConnection connection)
        {
            this.connection = connection;

            command = new SqlCommand("select id, name from artist where id = @id",connection);
            command.Parameters.Add("@id", SqlDbType.BigInt);

            adapter = new SqlDataAdapter(command);
            builder = new SqlCommandBuilder(adapter);
        }

        public long Insert(RecordingDataSet recordingDataSet, string artistName)
        {
            long artistId = IdGenerator.GetNextId(recordingDataSet.Artists.TableName, connection);

            RecordingDataSet.Artist artistRow = recordingDataSet.Artists.NewArtist();
            artistRow.Id = artistId;
            artistRow.Name = artistName;
            recordingDataSet.Artists.AddArtist(artistRow);

            adapter.Update(recordingDataSet, recordingDataSet.Artists.TableName);

            return artistId;
        }

        public RecordingDataSet.Artist FindById(long artistId, RecordingDataSet recordingDataSet)
        {
            command.Parameters["@id"].Value = artistId;
            adapter.Fill(recordingDataSet, recordingDataSet.Artists.TableName);
            DataRow[] rows = recordingDataSet.Artists.Select(String.Format("id={0}", artistId));

            if (rows.Length < 1) return null;
            return (RecordingDataSet.Artist) rows[0];
        }

        public void Delete(RecordingDataSet recordingDataSet, long artistId)
        {
            RecordingDataSet.Artist loadedArtist = FindById(artistId, recordingDataSet);
            loadedArtist.Delete();
            adapter.Update(recordingDataSet, recordingDataSet.Artists.TableName);
        }

        public void Update(RecordingDataSet recordingDataSet)
        {
            adapter.Update(recordingDataSet, recordingDataSet.Artists.TableName);
        }
    }
}