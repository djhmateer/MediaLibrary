using DataAccessLayer;
using NUnit.Framework;

namespace ServiceInterface
{
    [TestFixture]
    public class TrackAssemblerFixture
    {
        private RecordingDataSet.Artist artist;
        private RecordingDataSet.Genre genre;
        private RecordingDataSet.Track track;
        private TrackDto trackDto;

        [SetUp]
        public void SetUp()
        {
            RecordingDataSet recordingDataSet = new RecordingDataSet();

            artist = recordingDataSet.Artists.NewArtist();
            artist.Id = 1;
            artist.Name = "Artist";
            recordingDataSet.Artists.AddArtist(artist);

            genre = recordingDataSet.Genres.NewGenre();
            genre.Id = 1;
            genre.Name = "Genre";
            recordingDataSet.Genres.AddGenre(genre);

            track = recordingDataSet.Tracks.NewTrack();
            track.Id = 1;
            track.Title = "Track Title";
            track.Duration = 100;
            track.Genre = genre;
            track.Artist = artist;
            recordingDataSet.Tracks.AddTrack(track);

            trackDto = RecordingAssembler.WriteTrack(track);
        }

        [Test]
        public void Id()
        {
            Assert.AreEqual(track.Id, trackDto.id);
        }

        [Test]
        public void Title()
        {
            Assert.AreEqual(track.Title, trackDto.title);
        }

        [Test]
        public void Duration()
        {
            Assert.AreEqual(track.Duration, trackDto.duration);
        }

        [Test]
        public void GenreName()
        {
            Assert.AreEqual(genre.Name, trackDto.genreName);
        }

        [Test]
        public void ArtistName()
        {
            Assert.AreEqual(artist.Name, trackDto.artistName);
        }
    }

        
}