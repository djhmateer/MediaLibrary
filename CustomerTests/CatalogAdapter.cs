using ServiceInterface.ServiceGateway;

namespace CustomerTests
{
    public class CatalogAdapter
    {
        CatalogGateway gateway = new CatalogGateway();
        static ServiceInterface.ServiceGateway.RecordingDto recording;

        //public string Title { get; set; }

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

        public string ArtistName()
        {
            return recording.artistName;
        }

        public string ReleaseDate()
        {
            return recording.releaseDate;
        }

        public string LabelName()
        {
            return recording.labelName;
        }

        public string Duration()
        {
            return recording.totalRunTime.ToString();
        }

    }
}