using System;
using System.Data;
using System.Data.SqlClient;
using DataModel;

namespace DataAccessLayer
{
    public class TrackGateway
    {
        SqlCommand command;
        SqlCommandBuilder builder;
        SqlConnection connection;
        SqlDataAdapter adapter;

        SqlCommand findByRecordingIdCommand;
        SqlDataAdapter findByRecordingIdAdapter;

        public TrackGateway(SqlConnection connection)
        {
            this.connection = connection;

            command = new SqlCommand("select id, title, duration,artistId, genreId, recordingId from track where id = @id",
                                     connection);
            command.Parameters.Add("@id", SqlDbType.BigInt);

            adapter = new SqlDataAdapter(command);
            builder = new SqlCommandBuilder(adapter);

            findByRecordingIdCommand =
                new SqlCommand("select id, title, duration, artistId, genreId, recordingId from track where recordingId=@recordingId",
                               connection);

            findByRecordingIdCommand.Parameters.Add("@recordingId", SqlDbType.BigInt);
            findByRecordingIdAdapter = new SqlDataAdapter(findByRecordingIdCommand);
        }

        public long Insert(RecordingDataSet recordingDataSet, string title, int duration)
        {
            long trackId = IdGenerator.GetNextId(recordingDataSet.Tracks.TableName, connection);
            RecordingDataSet.Track trackRow = recordingDataSet.Tracks.NewTrack();

            trackRow.Id = trackId;
            trackRow.Title = title;
            trackRow.Duration = duration;

            recordingDataSet.Tracks.AddTrack(trackRow);

            adapter.Update(recordingDataSet, recordingDataSet.Tracks.TableName);

            return trackId;
        }

        public RecordingDataSet.Track FindById(long trackId, RecordingDataSet recordingDataSet)
        {
            command.Parameters["@id"].Value = trackId;
            adapter.Fill(recordingDataSet, recordingDataSet.Tracks.TableName);
            DataRow[] rows = recordingDataSet.Tracks.Select(String.Format("id={0}", trackId));
            if (rows.Length < 1) return null;

            RecordingDataSet.Track track = (RecordingDataSet.Track) rows[0];
            return track;
        }

        public RecordingDataSet.Track[] FindByRecordingId(
            long recordingId, RecordingDataSet recordingDataSet)
        {
            findByRecordingIdCommand.Parameters["@recordingId"].Value = recordingId;
            findByRecordingIdAdapter.Fill(recordingDataSet, recordingDataSet.Tracks.TableName);
            DataRow[] rows = recordingDataSet.Tracks.Select(String.Format("recordingId={0}", recordingId));
            return (RecordingDataSet.Track[]) rows;
        }

        public void Delete(RecordingDataSet recordingDataSet, long trackId)
        {
            RecordingDataSet.Track loadedTrack = FindById(trackId, recordingDataSet);
            loadedTrack.Delete();

            adapter.Update(recordingDataSet, recordingDataSet.Tracks.TableName);
        }

        public void Update(RecordingDataSet recordingDataSet)
        {
            adapter.Update(recordingDataSet, recordingDataSet.Tracks.TableName);
        }
    }
}