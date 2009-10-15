using System.Web.Services;

namespace ServiceInterface
{
    [WebService(Namespace = "http://nunit.org/services", Name = "CatalogGateway")]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
        // [System.Web.Script.Services.ScriptService]
    public class CatalogServiceInterface : WebService
    {
        DatabaseCatalogService service = new DatabaseCatalogService();

        [WebMethod]
        public RecordingDto FindByRecordingId(long id)
        {
            return service.FindByRecordingId(id);
        }
    }
}