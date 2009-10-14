using System.ComponentModel;
using System.Web.Services;

namespace ServiceInterface
{
    /// <summary>
    /// Summary description for CatalogServiceInterface
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
        // [System.Web.Script.Services.ScriptService]
    public class CatalogServiceInterface : WebService
    {
        DatabaseCatalogService service =  new DatabaseCatalogService();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public RecordingDto FindByRecordingId(long id)
        {
            return service.FindByRecordingId(id);
        }
    }
}