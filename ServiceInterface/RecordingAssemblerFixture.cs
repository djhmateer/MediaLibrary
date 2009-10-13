using MediaLibrary;
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
        public void TrackCount()
        {
            Assert.AreEqual(recording.GetTracks().Length, dto.tracks.Length);
        }
    }
}