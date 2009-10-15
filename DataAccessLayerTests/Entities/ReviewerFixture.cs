using DataAccessLayer;
using DataModel;
using NUnit.Framework;

namespace DataAccessLayerTests
{
    [TestFixture]
    public class ReviewerFixture : ConnectionFixture
    {
        static readonly string reviewerName = "Reviewer";

        long reviewerId;
        ReviewerGateway gateway;
        RecordingDataSet recordingDataSet;

        [SetUp]
        public void SetUp()
        {
            recordingDataSet = new RecordingDataSet();
            gateway = new ReviewerGateway(Connection);
            reviewerId = gateway.Insert(recordingDataSet, reviewerName);
        }

        [TearDown]
        public void TearDown()
        {
            gateway.Delete(recordingDataSet, reviewerId);
        }

        [Test]
        public void RetrieveReviewerFromDatabase()
        {
            RecordingDataSet loadedFromDB = new RecordingDataSet();
            RecordingDataSet.Reviewer loadedReviewer = gateway.FindById(reviewerId, loadedFromDB);

            Assert.AreEqual(reviewerId, loadedReviewer.Id);
            Assert.AreEqual(reviewerName, loadedReviewer.Name);
        }

        [Test]
        public void DeleteReviewerFromDatabase()
        {
            RecordingDataSet emptyDataSet = new RecordingDataSet();
            long deletedReviewerId = gateway.Insert(emptyDataSet, "Deleted Reviewer");
            gateway.Delete(emptyDataSet, deletedReviewerId);

            RecordingDataSet.Reviewer deleletedReviewer =
                gateway.FindById(deletedReviewerId, emptyDataSet);
            Assert.IsNull(deleletedReviewer);
        }

        [Test]
        public void UpdateReviewerFieldInReviewer()
        {
            string modifiedName = "Modified Name";

            RecordingDataSet.Reviewer reviewer = recordingDataSet.Reviewers[0];
            reviewer.Name = modifiedName;
            gateway.Update(recordingDataSet);

            RecordingDataSet updatedDataSet = new RecordingDataSet();
            RecordingDataSet.Reviewer updatedReviewer =
                gateway.FindById(reviewerId, updatedDataSet);
            Assert.AreEqual(modifiedName, updatedReviewer.Name);
        }
    }
}