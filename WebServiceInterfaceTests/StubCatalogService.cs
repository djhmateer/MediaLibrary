using DataAccessLayer;
using DataModel;

namespace ServiceInterface
{
    // used to be CatalogServiceStub
    public class StubCatalogService : CatalogService
    {
        RecordingDataSet.Recording recording;

        public StubCatalogService(RecordingDataSet.Recording recording)
        {
            this.recording = recording;
        }

        protected override RecordingDataSet.Recording FindById(long id)
        {
            if (id != recording.Id) return null;
            return recording;
        }
    }
}