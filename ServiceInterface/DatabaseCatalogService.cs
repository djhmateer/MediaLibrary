using MediaLibrary;

namespace ServiceInterface
{
    public class DatabaseCatalogService : CatalogService
    {
        protected override RecordingDataSet.Recording FindById(long id)
        {
            RecordingDataSet dataSet = new RecordingDataSet();
            return Catalog.FindByRecordingId(dataSet, id);
        }
    }
}