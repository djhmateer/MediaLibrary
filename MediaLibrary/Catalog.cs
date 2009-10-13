using System.Configuration;
using System.Data.SqlClient;

namespace MediaLibrary
{
    public class Catalog
    {
        static public RecordingDataSet.Recording FindByRecordingId(
            RecordingDataSet recordingDataSet, long recordingId)
        {
            SqlConnection connection = null;
            RecordingDataSet.Recording recording = null;

            try
            {
                //connection = new SqlConnection(ConfigurationSettings.AppSettings.Get("Catalog.Connection"));
                connection = new SqlConnection("Data Source=DAVEXPLAPTOP;Initial Catalog=catalog;Integrated Security=True");
                connection.Open();

                RecordingGateway recordingGateway = new RecordingGateway(connection);
                recording = recordingGateway.FindById(recordingId, recordingDataSet);
                if (recording != null)
                {
                    long artistId = recording.ArtistId;
                    ArtistGateway artistGateway = new ArtistGateway(connection);
                    RecordingDataSet.Artist artist =
                        artistGateway.FindById(artistId, recordingDataSet);

                    long labelId = recording.LabelId;
                    LabelGateway labelGateway = new LabelGateway(connection);
                    RecordingDataSet.Label label =
                        labelGateway.FindById(labelId, recordingDataSet);

                    GenreGateway genreGateway = new GenreGateway(connection);
                    TrackGateway trackGateway = new TrackGateway(connection);
                    foreach (RecordingDataSet.Track track in
                        trackGateway.FindByRecordingId(recordingId, recordingDataSet))
                    {
                        artistId = track.ArtistId;
                        long genreId = track.GenreId;
                        artist = artistGateway.FindById(artistId, recordingDataSet);
                        RecordingDataSet.Genre genre =
                            genreGateway.FindById(genreId, recordingDataSet);
                    }

                    ReviewGateway reviewGateway = new ReviewGateway(connection);
                    ReviewerGateway reviewerGateway = new ReviewerGateway(connection);
                    foreach (RecordingDataSet.Review review in
                        reviewGateway.FindByRecordingId(recordingId, recordingDataSet))
                    {
                        long reviewerId = review.ReviewerId;

                        RecordingDataSet.Reviewer reviewer =
                            reviewerGateway.FindById(reviewerId, recordingDataSet);
                    }
                }
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }

            return recording;
        }
    }
}