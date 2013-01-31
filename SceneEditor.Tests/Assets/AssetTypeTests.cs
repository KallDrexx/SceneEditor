using System;
using System.IO;
using NUnit.Framework;
using SceneEditor.Core.Assets;

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
    }
}
