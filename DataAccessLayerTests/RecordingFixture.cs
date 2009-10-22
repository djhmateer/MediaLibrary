using DataModel;
using NUnit.Framework;

namespace DataAccessLayerTests
{
    public abstract class RecordingFixture : ConnectionFixture
    {
        RecordingBuilder builder = new RecordingBuilder();
        RecordingDataSet dataSet;
        RecordingDataSet.Recording recording;

        [SetUp]
        public void SetUp()
        {
            dataSet = builder.make_sample_recording_with_artist_id_and_label_id_and_insert_into_database(Connection);
            recording = dataSet.Recordings[0];
        }

        [TearDown]
        public void TearDown()
        {
            builder.Delete(dataSet);
        }

        public RecordingBuilder Builder
        {
            get { return builder; }
        }

        public RecordingDataSet.Recording Recording
        {
            get { return recording; }
        }

        public RecordingDataSet RecordingDataSet
        {
            get { return dataSet; }
        }

    }
}