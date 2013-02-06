using System;
using System.IO;
using NUnit.Framework;
using SceneEditor.Core.Assets;
using SceneEditor.Core.General;

namespace SceneEditor.Tests.Assets
{
    [TestFixture]
    public class AssetTypeTests
    {
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ThrowsExceptionIfSourceStreamIsNull()
        {
// ReSharper disable ObjectCreationAsStatement
            new Asset("test", null);
// ReSharper restore ObjectCreationAsStatement
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void ThrowsExceptionIfSourceStreamIsEmpty()
        {
            var stream = new MemoryStream();

// ReSharper disable ObjectCreationAsStatement
            new Asset("test", stream);
// ReSharper restore ObjectCreationAsStatement
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ThrowsExceptionIfNameIsNull()
        {
            var testStream = new MemoryStream();
            using (var writer = new StreamWriter(testStream))
            {
                writer.Write("abccd");
                writer.Flush();
                testStream.Position = 0;

// ReSharper disable ObjectCreationAsStatement
                new Asset(null, testStream);
// ReSharper restore ObjectCreationAsStatement
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsExceptionIfNameIsEmpty()
        {
            var testStream = new MemoryStream();
            using (var writer = new StreamWriter(testStream))
            {
                writer.Write("abccd");
                writer.Flush();
                testStream.Position = 0;

// ReSharper disable ObjectCreationAsStatement
                new Asset(string.Empty, testStream);
// ReSharper restore ObjectCreationAsStatement
            }
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsExceptionIfNameIsWhitespace()
        {
            var testStream = new MemoryStream();
            using (var writer = new StreamWriter(testStream))
            {
                writer.Write("abccd");
                writer.Flush();
                testStream.Position = 0;

                // ReSharper disable ObjectCreationAsStatement
                new Asset("     ", testStream);
                // ReSharper restore ObjectCreationAsStatement
            }
        }

        [Test]
        public void NameIsCorrectlySet()
        {
            const string testName = "My Asset Name";
            var testStream = new MemoryStream();
            using (var writer = new StreamWriter(testStream))
            {
                writer.Write("abccd");
                writer.Flush();
                testStream.Position = 0;

                var asset = new Asset(testName, testStream);

                Assert.AreEqual(testName, asset.Name, "Asset's name was not correct");
            }
        }

        [Test]
        public void StreamContentsAreCorrectlyCopiedToAsset()
        {
            const string testContents = "My Asset Contents";
            Asset asset;
            var testStream = new MemoryStream();
            using (var writer = new StreamWriter(testStream))
            {
                writer.Write(testContents);
                writer.Flush();

                asset = new Asset("test", testStream);
            }

            using (var reader = new StreamReader(asset.Stream))
            {
                var contents = reader.ReadToEnd();
                Assert.AreEqual(testContents, contents, "Asset's stream contents were not correct");
            }
        }

        [Test]
        public void StreamReturnsFullStreamEachGet()
        {
            const string testContents = "My Asset Contents";
            Asset asset;
            var testStream = new MemoryStream();
            using (var writer = new StreamWriter(testStream))
            {
                writer.Write(testContents);
                writer.Flush();

                asset = new Asset("test", testStream);
            }

            string contents1;
            string contents2;

            using (var reader = new StreamReader(asset.Stream))
                contents1 = reader.ReadToEnd();

            using (var reader = new StreamReader(asset.Stream))
                contents2 = reader.ReadToEnd();

            Assert.AreEqual(contents1, contents2, "Stream's contents were not equal");
        }

        [Test]
        public void AssetDeterminesImageSize()
        {
            const string fileLocation = "TestFiles\\arrow.png";

            Asset asset;
            using (var stream = new FileStream(fileLocation, FileMode.Open))
                asset = new Asset("test", stream);

            var expected = new Vector(24, 23);
            Assert.AreEqual(expected, asset.ImageDimensions, "Image dimensions were incorrect");
        }

        [Test]
        public void AssetsImageSizeIsZeroWithNonImage()
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write("abcdef");
                writer.Flush();
                stream.Position = 0;
                new Asset("test", stream);
            }
        }
    }
}
