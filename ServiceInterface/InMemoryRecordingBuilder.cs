using System;
using System.Collections;
using MediaLibrary;

namespace ServiceInterface
{
    public class InMemoryRecordingBuilder
    {
        static public RecordingDataSet.Recording Make()
        {
            RecordingDataSet dataSet = new RecordingDataSet();

            RecordingDataSet.Recording recording = dataSet.Recordings.NewRecording();
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

            RecordingDataSet.Genre genre = dataSet.Genres.NewGenre();
            genre.Id = 1;
            genre.Name = "Genre";
            dataSet.Genres.AddGenre(genre);

            int trackCount = 2;
            ArrayList tracks = new ArrayList(trackCount);
            for (int i = 0; i < trackCount; i++)
            {
                RecordingDataSet.Track track = dataSet.Tracks.NewTrack();
                track.Recording = recording;
                track.Id = i + 1;
                track.Title = "Track Title";
                track.Duration = 100;
                track.Genre = genre;
                track.Artist = artist;
                dataSet.Tracks.AddTrack(track);
                tracks.Add(track);
            }

            RecordingDataSet.Reviewer reviewer =
                dataSet.Reviewers.NewReviewer();
            reviewer.Id = 1;
            reviewer.Name = "Reviewer";
            dataSet.Reviewers.AddReviewer(reviewer);

            int reviewCount = 3;
            ArrayList reviews = new ArrayList(reviewCount);
            for (int j = 0; j < reviewCount; j++)
            {
                RecordingDataSet.Review review = dataSet.Reviews.NewReview();
                review.Id = j + 1;
                review.Content = "Review Content";
                review.Rating = j + 1;
                review.Reviewer = reviewer;
                review.Recording = recording;
                dataSet.Reviews.AddReview(review);
                reviews.Add(review);
            }

            return recording;
        }
    }
}