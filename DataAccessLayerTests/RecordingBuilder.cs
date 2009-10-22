using System;
using System.Collections;
using System.Data.SqlClient;
using DataAccessLayer;
using DataModel;

namespace DataAccessLayerTests
{
    public class RecordingBuilder
    {
        static readonly string title = "Title";
        static readonly DateTime releaseDate = new DateTime(1999, 1, 12);

        long recordingId;
        long artistId;
        long labelId;
        long trackId;

        RecordingGateway recordingGateway;
        ArtistGateway artistGateway;
        LabelGateway labelGateway;
        TrackGateway trackGateway;

        public RecordingDataSet make_sample_recording_with_artist_id_and_label_id_and_insert_into_database(SqlConnection connection)
        {
            RecordingDataSet recordingDataSet = new RecordingDataSet();

            recordingGateway = new RecordingGateway(connection);
            artistGateway = new ArtistGateway(connection);
            labelGateway = new LabelGateway(connection);

            artistId = artistGateway.Insert(recordingDataSet, "Artist");
            labelId = labelGateway.Insert(recordingDataSet, "Label");
            recordingId = recordingGateway.Insert(recordingDataSet, title, releaseDate, artistId, labelId);

            trackGateway = new TrackGateway(connection);
            trackId = trackGateway.Insert(recordingDataSet, "Track", 120);
            RecordingDataSet.Track inMemoryDataset_table_track = trackGateway.FindById(trackId, recordingDataSet);

           // RecordingDataSet rec = new RecordingDataSet();
            
           // RecordingDataSet.Recording recording = recordingGateway.FindById(recordingId, rec);
           //// // link the sample_recording to the track we just created
           //inMemoryDataset_table_track.Recording = recording;
           //trackGateway.Update(rec);


           // recordingGateway.FindById(recordingId, recordingDataSet);



            return recordingDataSet;
        }

        public void Delete(RecordingDataSet dataSet)
        {
            artistGateway.Delete(dataSet, artistId);
            labelGateway.Delete(dataSet, labelId);
            recordingGateway.Delete(dataSet, recordingId);
        }

        public long LabelId
        {
            get { return labelId; }
        }

        public long ArtistId
        {
            get { return artistId; }
        }

        public string Title
        {
            get { return title; }
        }

        public DateTime ReleaseDate
        {
            get { return releaseDate; }
        }

        public long RecordingId
        {
            get { return recordingId; }
        }

        public RecordingGateway RecordingGateway
        {
            get { return recordingGateway; }
        }
    }
}