using System;
using NUnit.Framework;
using MediaLibrary;

namespace Tests
{
	[TestFixture]
	public class RecordingGatewayFixture : RecordingFixture
	{
		private RecordingGateway recordingGateway;

		[SetUp]
		public new void SetUp()
		{
			base.SetUp();
			recordingGateway = Builder.RecordingGateway;
		}

		[Test]
		public void RetrieveRecordingFromDatabase()
		{
			RecordingDataSet loadedFromDB = new RecordingDataSet();
			RecordingDataSet.Recording loadedRecording = 
				recordingGateway.FindById(Builder.RecordingId, loadedFromDB);
			
			Assert.AreEqual(Builder.RecordingId, loadedRecording.Id);
			Assert.AreEqual(Builder.Title, loadedRecording.Title);
			Assert.AreEqual(Builder.ReleaseDate, loadedRecording.ReleaseDate);
			Assert.AreEqual(Builder.ArtistId, loadedRecording.ArtistId);
			Assert.AreEqual(Builder.LabelId, loadedRecording.LabelId);
		}

		[Test]
		public void CheckDelete()
		{
			RecordingDataSet emptyDataSet = new RecordingDataSet();

			long deletedRecordingId = recordingGateway.Insert(emptyDataSet,
				"Deleted Title", new DateTime(1991,8,6), Builder.ArtistId, Builder.LabelId);

			recordingGateway.Delete(emptyDataSet,deletedRecordingId);
			
			RecordingDataSet.Recording deleletedRecording = 
				recordingGateway.FindById(deletedRecordingId, emptyDataSet);
			Assert.IsNull(deleletedRecording);
		}

		[Test]
		public void UpdateTitleFieldInRecording()
		{
			string modifiedTitle = "Modified Title";
	
			RecordingDataSet.Recording recording = Sample_recording;
			recording.Title = modifiedTitle;
			recordingGateway.Update(RecordingDataSet);
	
			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Recording updatedRecording = 
				recordingGateway.FindById(Builder.RecordingId, updatedDataSet);
			Assert.AreEqual(modifiedTitle, updatedRecording.Title);
		}

		[Test]
		public void UpdateReleaseDateFieldInRecording()
		{
			DateTime modifiedDate = new DateTime(1989,5,15);
	
			RecordingDataSet.Recording recording = Sample_recording;
			recording.ReleaseDate = modifiedDate;
			recordingGateway.Update(RecordingDataSet);
	
			RecordingDataSet updatedDataSet = new RecordingDataSet();
			RecordingDataSet.Recording updatedRecording = 
				recordingGateway.FindById(Builder.RecordingId, updatedDataSet);
			Assert.AreEqual(modifiedDate, updatedRecording.ReleaseDate);
		}
	}
}