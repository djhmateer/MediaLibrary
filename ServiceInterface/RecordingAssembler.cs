using MediaLibrary;

namespace ServiceInterface
{
    public class RecordingAssembler
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
            //WriteTotalRuntime(dto, recording);
            //WriteReviews(dto, recording);
            //WriteAverageRating(dto, recording);
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
    }
}