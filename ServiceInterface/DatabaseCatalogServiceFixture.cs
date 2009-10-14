using NUnit.Framework;
using Tests;

namespace ServiceInterface
{
    // RF inherits off connection
    [TestFixture]
    public class DatabaseCatalogServiceFixture : RecordingFixture
    {
        DatabaseCatalogService service;
        RecordingDto dto;

        [SetUp]
        public void SetUp()
        {
            base.SetUp();
            service = new DatabaseCatalogService();
            dto = service.FindByRecordingId(Sample_recording.Id);
        }

        [Test]
        public void check_id_from_the_sample_recording_is_the_same_as_the_one_from_the_dto_after_it_has_been_processed()
        {
            Assert.AreEqual(Sample_recording.Id, dto.id);
        }
    }
}