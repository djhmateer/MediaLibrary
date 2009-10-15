using fit;
using ServiceInterface.ServiceGateway;

namespace CustomerTests
{
    public class CatalogAdapter : Fixture
    {
        CatalogGateway gateway = new CatalogGateway();
        static ServiceInterface.ServiceGateway.RecordingDto recording;

        public void FindByRecordingId(long id)
        {
            recording = gateway.FindByRecordingId(id);
        }

        public bool Found()
        {
            return recording != null;
        }

        public string Title()
        {
            return recording.title;
        }

   }
}