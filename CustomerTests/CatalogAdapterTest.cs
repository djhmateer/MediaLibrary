using NUnit.Framework;

namespace CustomerTests
{
    [TestFixture]
    public class CatalogAdapterTest
    {
        [Test]
        public void stuff()
        {
            CatalogAdapter catalog_adapter = new CatalogAdapter();
            Assert.IsNotNull(catalog_adapter);
            catalog_adapter.FindByRecordingId(4);
            Assert.AreEqual("The Rising", catalog_adapter.Title()); 
        }

    }
}