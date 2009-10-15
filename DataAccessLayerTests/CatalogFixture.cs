using System;
using DataAccessLayer;
using DataModel;
using NUnit.Framework;

namespace DataAccessLayerTests
{
    [TestFixture]
    public class CatalogFixture : ConnectionFixture
    {
        long artistId;
        long labelId;
        long genreId;
        long reviewerId;
        long recordingId;
        long reviewId;
        long trackId;

        RecordingGateway recordingGateway;
        TrackGateway trackGateway;
        ReviewGateway reviewGateway;
        ReviewerGateway reviewerGateway;
        LabelGateway labelGateway;
        ArtistGateway artistGateway;
        GenreGateway genreGateway;

        RecordingDataSet recordingDataSet;
        RecordingDataSet.Recording loadedRecording;

        [SetUp]
        public void SetUp_a_known_recording_then_verify_it_brings_back_the_same_data()
        {
            recordingDataSet = new RecordingDataSet();

            recordingGateway = new RecordingGateway(Connection);
            trackGateway = new TrackGateway(Connection);
            reviewGateway = new ReviewGateway(Connection);
            reviewerGateway = new ReviewerGateway(Connection);
            labelGateway = new LabelGateway(Connection);
            artistGateway = new ArtistGateway(Connection);
            genreGateway = new GenreGateway(Connection);

            artistId = artistGateway.Insert(recordingDataSet, "Artist");
            labelId = labelGateway.Insert(recordingDataSet, "Label");
            genreId = genreGateway.Insert(recordingDataSet, "Genre");
            reviewerId = reviewerGateway.Insert(recordingDataSet, "Reviewer");
            recordingId = recordingGateway.Insert(recordingDataSet, "Recording Title", new DateTime(1999, 1, 12), artistId, labelId);
            reviewId = reviewGateway.Insert(recordingDataSet, 1, "Review");
            trackId = trackGateway.Insert(recordingDataSet, "Track Title", 120);

            RecordingDataSet.Recording recording = recordingGateway.FindById(recordingId, recordingDataSet);
            RecordingDataSet.Review review = reviewGateway.FindById(reviewId, recordingDataSet);
            RecordingDataSet.Track track = trackGateway.FindById(trackId, recordingDataSet);
            RecordingDataSet.Label label = labelGateway.FindById(labelId, recordingDataSet);
            RecordingDataSet.Genre genre = genreGateway.FindById(genreId, recordingDataSet);
            RecordingDataSet.Artist artist = artistGateway.FindById(artistId, recordingDataSet);
            RecordingDataSet.Reviewer reviewer = reviewerGateway.FindById(reviewerId, recordingDataSet);

            // setup the relationships
            recording.Artist = artist;
            recording.Label = label;
            track.Recording = recording;
            track.Artist = artist;
            track.Genre = genre;
            review.Recording = recording;
            review.Reviewer = reviewer;

            // only these as they are in the 'middle' and depend??
            recordingGateway.Update(recordingDataSet);
            trackGateway.Update(recordingDataSet);
            reviewGateway.Update(recordingDataSet);

            RecordingDataSet loadedDataSet = new RecordingDataSet();
            loadedRecording = Catalog.FindByRecordingId(loadedDataSet, recordingId);
        }

        [TearDown]
        public void TearDown()
        {
            artistGateway.Delete(recordingDataSet, artistId);
            labelGateway.Delete(recordingDataSet, labelId);
            genreGateway.Delete(recordingDataSet, genreId);
            reviewerGateway.Delete(recordingDataSet, reviewerId);
            reviewGateway.Delete(recordingDataSet, reviewId);
            trackGateway.Delete(recordingDataSet, trackId);
            recordingGateway.Delete(recordingDataSet, recordingId);
        }

        [Test]
        public void NotNull()
        {
            Assert.IsNotNull(loadedRecording);
        }

        [Test]
        public void CountTracks()
        {
            RecordingDataSet.Track[] loadedTracks = loadedRecording.GetTracks();
            Assert.AreEqual(1, loadedTracks.Length);
        }

        [Test]
        public void CountReviews()
        {
            RecordingDataSet.Review[] loadedReviews = loadedRecording.GetReviews();
            Assert.AreEqual(1, loadedReviews.Length);
        }

        [Test]
        public void ArtistOfTheRecording()
        {
            RecordingDataSet.Artist loadedArtist = loadedRecording.Artist;
            Assert.AreEqual(artistId, loadedArtist.Id);
        }

        [Test]
        public void LabelOfTheRecording()
        {
            RecordingDataSet.Label loadedLabel = loadedRecording.Label;
            Assert.AreEqual(labelId, loadedLabel.Id);
        }

        [Test]
        public void ArtistOfTheTrack()
        {
            RecordingDataSet.Artist loadedArtist = loadedRecording.GetTracks()[0].Artist;
            Assert.AreEqual(artistId, loadedArtist.Id);
        }

        [Test]
        public void GenreOfTheTrack()
        {
            RecordingDataSet.Genre loadedGenre = loadedRecording.GetTracks()[0].Genre;
            Assert.AreEqual(genreId, loadedGenre.Id);
        }

        [Test]
        public void ReviewerOfTheReview()
        {
            RecordingDataSet.Reviewer loadedReviewer = loadedRecording.GetReviews()[0].Reviewer;
            Assert.AreEqual(reviewerId, loadedReviewer.Id);
        }
    }
}