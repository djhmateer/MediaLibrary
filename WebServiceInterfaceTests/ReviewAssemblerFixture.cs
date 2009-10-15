using DataAccessLayer;
using ServiceInterface;
using NUnit.Framework;

namespace ServiceInterface {

[TestFixture]
public class ReviewAssemblerFixture
{
	private RecordingDataSet.Reviewer reviewer;
	private RecordingDataSet.Review review;
	private ReviewDto reviewDto;

	[SetUp]
	public void MakeReview()
	{
		RecordingDataSet recordingDataSet = new RecordingDataSet();

		reviewer = recordingDataSet.Reviewers.NewReviewer();
		reviewer.Id = 1;
		reviewer.Name = "Reviewer";
		recordingDataSet.Reviewers.AddReviewer(reviewer);

		review = recordingDataSet.Reviews.NewReview();
		review.Id = 1;
		review.Content = "Review Content";
		review.Rating = 2;
		review.Reviewer = reviewer;
		recordingDataSet.Reviews.AddReview(review);

		reviewDto = RecordingAssembler.WriteReview(review);
	}

	[Test]
	public void Id()
	{
		Assert.AreEqual(review.Id, reviewDto.id);
	}

	[Test]
	public void Content()
	{
		Assert.AreEqual(review.Content, reviewDto.reviewContent);
	}

	[Test]
	public void Rating()
	{
		Assert.AreEqual(review.Rating, reviewDto.rating);
	}

	[Test]
	public void ReviewerName()
	{
		Assert.AreEqual(reviewer.Name, reviewDto.reviewerName);
	}
}}