using MediaLibrary;
using NUnit.Framework;

namespace Tests
{
	[TestFixture]
	public class ReviewFixture : ConnectionFixture
	{
		private static readonly int rating = 3;
		private static readonly string content = "Content";

		private long reviewId; 
		private RecordingDataSet recordingDataSet; 
		private ReviewGateway gateway;

		[SetUp]
		public void SetUp()
		{
			recordingDataSet = new RecordingDataSet();

			gateway = new ReviewGateway(Connection);
			reviewId = gateway.Insert(recordingDataSet, rating, content);
		}

		[TearDown]
		public void TearDown()
		{
			gateway.Delete(recordingDataSet, reviewId);
		}

		[Test]
		public void RetrieveReviewFromDatabase()
		{
			RecordingDataSet loadedFromDB = new RecordingDataSet();
			RecordingDataSet.Review loadedReview = gateway.FindById(reviewId, loadedFromDB);

			Assert.AreEqual(reviewId, loadedReview.Id);
			Assert.AreEqual(rating, loadedReview.Rating);
			Assert.AreEqual(content, loadedReview.Content);
		}

		[Test]
		public void DeleteReviewFromDatabase()
		{
			RecordingDataSet emptyDataSet = new RecordingDataSet();
			long deletedReviewId = gateway.Insert(emptyDataSet, 1, "Deleted Review");
			gateway.Delete(emptyDataSet, deletedReviewId);

			RecordingDataSet.Review deleletedReview = gateway.FindById(deletedReviewId, emptyDataSet);
			Assert.IsNull(deleletedReview);
		}

		[Test]
		public void UpdateReviewFieldInReview()
		{
			string modifiedContent = "Modified Content";

			RecordingDataSet.Review review = recordingDataSet.Reviews[0];
			review.Content = modifiedContent;
			gateway.Update(recordingDataSet);

			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Review updatedReview = gateway.FindById(reviewId, updatedDataSet);
			Assert.AreEqual(modifiedContent, updatedReview.Content);
		}

		[Test]
		public void UpdateRatingFieldInReview()
		{
			int modifiedRating = 2; 

			RecordingDataSet.Review review = recordingDataSet.Reviews[0];
			review.Rating = modifiedRating;
			gateway.Update(recordingDataSet);

			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Review updatedReview = gateway.FindById(reviewId, updatedDataSet);
			Assert.AreEqual(modifiedRating, updatedReview.Rating);
		}
	}
}