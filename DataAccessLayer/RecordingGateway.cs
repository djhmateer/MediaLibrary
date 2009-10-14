using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class RecordingGateway
    {
        private SqlDataAdapter adapter;
        private SqlConnection connection;
        private SqlCommand command;
        private SqlCommandBuilder builder;

        public RecordingGateway(SqlConnection connection)
        {
            this.connection = connection;

            command = new SqlCommand("select id, title, releaseDate, artistId, labelId from recording where id = @id",
                connection);
            command.Parameters.Add("@id", SqlDbType.BigInt);

            adapter = new SqlDataAdapter(command);
            builder = new SqlCommandBuilder(adapter);
        }

        public long Insert(RecordingDataSet recordingDataSet, string title, DateTime releaseDate, long artistId, long labelId)
        {
            long recordingId = IdGenerator.GetNextId(recordingDataSet.Recordings.TableName, connection);
            RecordingDataSet.Recording recordingRow = recordingDataSet.Recordings.NewRecording();

            recordingRow.Id = recordingId;
            recordingRow.Title = title;
            recordingRow.ReleaseDate = releaseDate;
            recordingRow.ArtistId = artistId;
            recordingRow.LabelId = labelId;

            recordingDataSet.Recordings.AddRecording(recordingRow);

            adapter.Update(recordingDataSet, recordingDataSet.Recordings.TableName);

            return recordingId;
        }

        public RecordingDataSet.Recording FindById(long recordingId, RecordingDataSet recordingDataSet)
        {
            command.Parameters["@id"].Value = recordingId;
            adapter.Fill(recordingDataSet, recordingDataSet.Recordings.TableName);
            DataRow[] rows = recordingDataSet.Recordings.Select(String.Format("id={0}", recordingId));
            if (rows.Length < 1) return null;

            RecordingDataSet.Recording recording = (RecordingDataSet.Recording)rows[0];
            return recording;
        }

        public void Delete(RecordingDataSet recordingDataSet, long recordingId)
        {
            RecordingDataSet.Recording loadedRecording = FindById(recordingId, recordingDataSet);
            loadedRecording.Delete();

            adapter.Update(recordingDataSet, recordingDataSet.Recordings.TableName);
        }

        public void Update(RecordingDataSet recordingDataSet)
        {
            adapter.Update(recordingDataSet, recordingDataSet.Recordings.TableName);
        }
    }

}
