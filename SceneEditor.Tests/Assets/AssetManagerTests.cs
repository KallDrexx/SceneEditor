using System;
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
            var testStream = new MemoryStream();
            using (var writer = new StreamWriter(testStream))
            {
                writer.Write(assetContent);
                writer.Flush();
                testStream.Position = 0;

                manager.AddAsset(new Asset(assetName, testStream));
            }

            var result = manager.GetAsset(assetName);
            Assert.IsNotNull(result, "Null asset returned");
            Assert.AreEqual(assetName, result.Name, "Asset's name was incorrect");

            using (var reader = new StreamReader(result.Stream))
            {
                var content = reader.ReadToEnd();
                Assert.AreEqual(assetContent, content, "Asset's content was incorrect");
            }
        }
    }
}
