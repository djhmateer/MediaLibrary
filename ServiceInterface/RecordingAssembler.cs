using DataAccessLayer;
using DataModel;

namespace ServiceInterface
{
    static public class RecordingAssembler
    {
        static public RecordingDto WriteDto(RecordingDataSet.Recording recording)
        {
            RecordingDto dto = new RecordingDto();
            dto.id = recording.Id;
            dto.title = recording.Title;

            dto.releaseDate = recording.ReleaseDate.ToShortDateString();
            dto.artistName = recording.Artist.Name;
            dto.labelName = recording.Label.Name;

            WriteTracks(dto, recording);
            WriteTotalRuntime(dto, recording);
            WriteReviews(dto, recording);
            WriteAverageRating(dto, recording);
            return dto;
        }

        static void WriteTracks(RecordingDto recordingDto, RecordingDataSet.Recording recording)
        {
            recordingDto.tracks = new TrackDto[recording.GetTracks().Length];
            int index = 0;
            foreach (RecordingDataSet.Track track in recording.GetTracks())
            {
                recordingDto.tracks[index++] = new TrackDto();
            }
        }

        static public TrackDto WriteTrack(RecordingDataSet.Track track)
        {
            TrackDto trackDto = new TrackDto();

            trackDto.id = track.Id;
            trackDto.title = track.Title;
            trackDto.duration = track.Duration;
            trackDto.genreName = track.Genre.Name;
            trackDto.artistName = track.Artist.Name;

            return trackDto;
        }

        // Intelligent mappings..may be better somewhere else?
        static public void WriteTotalRuntime(RecordingDto dto, RecordingDataSet.Recording recording)
        {
            int runTime = 0;
            foreach (RecordingDataSet.Track track in recording.GetTracks())
            {
                runTime += track.Duration;
            }
            dto.totalRunTime = runTime;
        }

        static void WriteReviews(RecordingDto recordingDto, RecordingDataSet.Recording recording)
        {
            recordingDto.reviews = new ReviewDto[recording.GetReviews().Length];
            int index = 0;
            foreach (RecordingDataSet.Review review in recording.GetReviews())
            {
                recordingDto.reviews[index++] = WriteReview(review);
            }
        }

        static public ReviewDto WriteReview(RecordingDataSet.Review review)
        {
            ReviewDto reviewDto = new ReviewDto();

            reviewDto.id = review.Id;
            reviewDto.reviewContent = review.Content;
            reviewDto.rating = review.Rating;
            reviewDto.reviewerName = review.Reviewer.Name;

            return reviewDto;
        }

        static void WriteAverageRating(RecordingDto recordingDto, RecordingDataSet.Recording recording)
        {
            if (recording.GetReviews().Length == 0)
            {
                recordingDto.averageRating = 0;
            }
            else
            {
                int totalRating = 0;
                foreach (RecordingDataSet.Review review in recording.GetReviews())
                {
                    totalRating += review.Rating;
                }
                recordingDto.averageRating = totalRating/recording.GetReviews().Length;
            }
        }
    }
}