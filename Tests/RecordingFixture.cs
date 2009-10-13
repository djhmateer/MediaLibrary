using MediaLibrary;
using NUnit.Framework;

namespace Tests
{
    public abstract class RecordingFixture : ConnectionFixture
    {
        RecordingBuilder builder = new RecordingBuilder();
        RecordingDataSet dataSet;
        RecordingDataSet.Recording sample_recording;

        [SetUp]
        public void SetUp()
        {
            dataSet = builder.Make_sample_recording_with_artist_id_and_label_id(Connection);
            sample_recording = dataSet.Recordings[0];
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

        public RecordingDataSet.Recording Sample_recording
        {
            get { return sample_recording; }
        }

        public RecordingDataSet RecordingDataSet
        {
            get { return dataSet; }
        }
    }
}