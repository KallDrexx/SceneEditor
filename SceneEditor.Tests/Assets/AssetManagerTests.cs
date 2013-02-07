using System.IO;
using NUnit.Framework;
using SceneEditor.Core.Assets;

namespace SceneEditor.Tests.Assets
{
    [TestFixture]
    public class AssetManagerTests
    {
        [Test]
        public void CanAddAndRetrieveAssets()
        {
            const string assetName = "Test Asset abcd";
            const string assetContent = "This is a test";

            var manager = new AssetManager();
            var assetId = AddTestAsset(assetContent, manager, assetName);

            var result = manager.GetAsset(assetId);
            Assert.IsNotNull(result, "Null asset returned");
            Assert.AreEqual(assetName, result.Name, "Asset's name was incorrect");

            using (var reader = new StreamReader(result.Stream))
            {
                var content = reader.ReadToEnd();
                Assert.AreEqual(assetContent, content, "Asset's content was incorrect");
            }
        }

        [Test]
        public void NonExistantAssetNameReturnsNull()
        {
            var manager = new AssetManager();
            var result = manager.GetAsset(5235);
            Assert.IsNull(result, "Non-null result was returned");
        }

        [Test]
        public void AssetManagerAssignsSequentialAssetIds()
        {
            var manager = new AssetManager();
            var assetId1 = AddTestAsset("aaaa", manager, "test1");
            var assetId2 = AddTestAsset("aaaa", manager, "test2");
            var assetId3 = AddTestAsset("aaaa", manager, "test3");

            Assert.AreEqual(1, assetId1, "First asset id was incorrect");
            Assert.AreEqual(2, assetId2, "Second asset id was incorrect");
            Assert.AreEqual(3, assetId3, "Third asset id was incorrect");
        }

        private static int AddTestAsset(string assetContent, AssetManager manager, string assetName)
        {
            var testStream = new MemoryStream();
            int assetId;
            using (var writer = new StreamWriter(testStream))
            {
                writer.Write(assetContent);
                writer.Flush();
                testStream.Position = 0;

                assetId = manager.AddAsset(new Asset(assetName, testStream));
            }
            return assetId;
        }
    }
}
