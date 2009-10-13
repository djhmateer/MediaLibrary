using NUnit.Framework;
using MediaLibrary;

namespace Tests
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

            ReviewerGateway reviewerGateway = new ReviewerGateway(Connection);
            long reviewerId = reviewerGateway.Insert(recordingDataSet, "Reviewer Name");

            RecordingDataSet.Review review = reviewGateway.FindById(reviewId, recordingDataSet);

            review.ReviewerId = reviewerId;
            reviewGateway.Update(recordingDataSet);

            Assert.AreEqual(reviewerId, review.Reviewer.Id);

            reviewGateway.Delete(recordingDataSet, reviewId);
            reviewerGateway.Delete(recordingDataSet, reviewerId);
        }
    }
}