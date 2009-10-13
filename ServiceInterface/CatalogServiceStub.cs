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

        public RecordingDto FindByRecordingId(long id)
        {
            if (id != recording.Id) return null;
            RecordingDto dto = new RecordingDto();
            dto.id = recording.Id;
            dto.title = recording.Title;
            return dto;
        }
    }
}