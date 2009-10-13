using MediaLibrary;

namespace ServiceInterface
{
    public class CatalogServiceStub
    {
        RecordingDataSet.Recording recording;

        public CatalogServiceStub(RecordingDataSet.Recording recording)
        {
            this.recording = recording;
        }

        public RecordingDataSet.Recording FindByRecordingId(long id)
        {
            if (id != recording.Id) return null;
            return recording;
        }
    }
}