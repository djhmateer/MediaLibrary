using NUnit.Framework;

namespace MediaLibrary
{
    [TestFixture]
    public class ReviewReviewerFixture : ConnectionFixture
    {
        [Test]
        public void ReviewerId()
        {
            RecordingDataSet recordingDataSet = new RecordingDataSet();
            ReviewGateway reviewGateway = new ReviewGateway(Connection);
            long reviewId = reviewGateway.Insert(recordingDataSet, 1, "Review Content");

            

        }

    }
}