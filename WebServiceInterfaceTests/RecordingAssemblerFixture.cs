using System;
using DataAccessLayer;
using DataModel;
using NUnit.Framework;

namespace ServiceInterface
{
    [TestFixture]
    public class RecordingAssemblerFixture
    {
        RecordingDataSet.Recording recording;
        RecordingDto dto;

        [SetUp]
        public void SetUp()
        {
            recording = InMemoryRecordingBuilder.Make();
            dto = RecordingAssembler.WriteDto(recording);
        }

        [Test]
        public void Id()
        {
            Assert.AreEqual(recording.Id, dto.id);
        }

        [Test]
        public void Title()
        {
            Assert.AreEqual(recording.Title, dto.title);
        }

        [Test]
        public void ReleaseDate()
        {
            Assert.AreEqual(recording.ReleaseDate.ToShortDateString(), dto.releaseDate);
        }

        [Test]
        public void ArtistName()
        {
            Assert.AreEqual(recording.Artist.Name, dto.artistName);
        }

        [Test]
        public void LabelName()
        {
            Assert.AreEqual(recording.Label.Name, dto.labelName);
        }

        [Test]
        public void TrackCount()
        {
            Assert.AreEqual(recording.GetTracks().Length, dto.tracks.Length);
        }

        [Test]
        public void TotalRuntime()
        {
            int runTime = 0;
            foreach (RecordingDataSet.Track track in recording.GetTracks())
            {
                runTime += track.Duration;
            }

            Assert.AreEqual(runTime, dto.totalRunTime);
        }

        [Test]
        public void ReviewCount()
        {
            Assert.AreEqual(recording.GetReviews().Length, dto.reviews.Length);
        }

        [Test]
        public void AverateRating()
        {
            int totalRating = 0;
            foreach (RecordingDataSet.Review review in recording.GetReviews())
            {
                totalRating += review.Rating;
            }

            int averageRating = totalRating/recording.GetReviews().Length;
            Assert.AreEqual(averageRating, dto.averageRating);
        }

        [Test]
        public void AverageRatingZero()
        {
            RecordingDataSet dataSet = new RecordingDataSet();

            RecordingDataSet.Recording recording =
                dataSet.Recordings.NewRecording();
            recording.Id = 1;
            recording.Title = "Title";
            recording.ReleaseDate = DateTime.Today;

            RecordingDataSet.Label label = dataSet.Labels.NewLabel();
            label.Id = 1;
            label.Name = "Label";
            dataSet.Labels.AddLabel(label);

            RecordingDataSet.Artist artist = dataSet.Artists.NewArtist();
            artist.Id = 1;
            artist.Name = "Artist";
            dataSet.Artists.AddArtist(artist);

            recording.Label = label;
            recording.Artist = artist;
            dataSet.Recordings.AddRecording(recording);

            RecordingDto dto = RecordingAssembler.WriteDto(recording);
            Assert.AreEqual(0, dto.averageRating);
        }
    }
}