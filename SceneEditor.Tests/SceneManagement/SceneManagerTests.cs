using System;
using System.IO;
using System.Linq;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Exceptions;
using SceneEditor.Core.General;
using SceneEditor.Core.Rendering;
using SceneEditor.Core.SceneManagement;
using SceneEditor.Core.SceneManagement.Objects;

namespace SceneEditor.Tests.SceneManagement
{
    [TestFixture]
    public class SceneManagerTests
    {
        private ISceneManager _manager;
        private Mock<IRenderer> _mockedRenderer;
        private Mock<IAssetManager> _mockedAssetManager;

        [SetUp]
        public void Setup()
        {
            _mockedRenderer = new Mock<IRenderer>();
            _mockedAssetManager = new Mock<IAssetManager>();
            _manager = new SceneManager(_mockedRenderer.Object, _mockedAssetManager.Object);
        }

        [Test]
        public void StartsWithCameraAtOrigin()
        {
            var position = _manager.CameraPosition;

            Assert.AreEqual(0, position.X, "Camera's X value is incorrect");
            Assert.AreEqual(0, position.Y, "Camera's Y value is incorrect");
        }

        [Test]
        public void CanMoveCameraByAmount()
        {
            var moveBy = new Vector(2, 3);

            // Since camera starts at 0,0, we have to do 2 moves to 
            //  verify it has moved by the amount specified instead of a 
            //  absolute position
            _manager.MoveCameraBy(moveBy);
            _manager.MoveCameraBy(moveBy);

            var expectedResult = (moveBy + moveBy);
            Assert.AreEqual(expectedResult, _manager.CameraPosition, "Camera was not moved to the correct spot");
        }

        [Test]
        public void CanMoveCameraToExactPosition()
        {
            var finalPosition = new Vector(2, 3);

            // Since the camera starts at 0,0 we have to do 2 moves
            //  to verify that the camera isn't just moving by the 
            //  vector's values but instead moving to the exact spot
            _manager.MoveCameraTo(finalPosition);
            _manager.MoveCameraTo(finalPosition);

            Assert.AreEqual(finalPosition, _manager.CameraPosition, "Camera was not moved to the correct position");
        }

        [Test]
        public void CanSetCameraDimensions()
        {
            var dimensions = new Vector(500, 300);
            _manager.SetCameraDimensions(dimensions);
            Assert.AreEqual(dimensions, _manager.CameraDimensions, "Camera's dimensions were incorrect");
        }

        [Test]
        public void CameraDimensionsStartAt100X100()
        {
            var expectedResult = new Vector(100, 100);

            Assert.AreEqual(expectedResult, _manager.CameraDimensions, "Camera dimensions were incorrect");
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ExceptionThrownWhenInstantiatedWithNullRenderer()
        {
// ReSharper disable ObjectCreationAsStatement
            new SceneManager(null, _mockedAssetManager.Object);
// ReSharper restore ObjectCreationAsStatement
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExceptionThrownWhenInstantiatedWithNullAssetmanager()
        {
// ReSharper disable ObjectCreationAsStatement
            new SceneManager(_mockedRenderer.Object, null);
            // ReSharper restore ObjectCreationAsStatement
        }

        [Test]
        public void RenderMethodCallsRenderer()
        {
            _manager.Render();

            _mockedRenderer.Verify(x => x.RenderScene(It.IsAny<SceneSnapshot>()));
        }

        [Test]
        public void RendererCalledWithCorrectCameraPosition()
        {
            var position = new Vector(5, 6);
            _manager.MoveCameraBy(position);
            _manager.Render();

            _mockedRenderer.Verify(x => x.RenderScene(It.Is<SceneSnapshot>(y => y.CameraPosition == position)));
        }

        [Test]
        public void RendererCalledWithCorrectCameraDimensions()
        {
            var size = new Vector(100, 200);
            _manager.SetCameraDimensions(size);
            _manager.Render();

            _mockedRenderer.Verify(x => x.RenderScene(It.Is<SceneSnapshot>(y => y.RenderAreaDimensions == size)));
        }

        [Test]
        public void CanAddSpriteToScene()
        {
            var position = new Vector(5, 6);
            var assetId = SetupAsset();

            _manager.AddBasicSceneSprite(assetId, position);
            var objects = _manager.GetAllSceneObjects();
            Assert.IsNotNull(objects, "Object list was null");
            var sceneObjects = objects as ISceneObject[] ?? objects.ToArray();

            Assert.AreEqual(1, sceneObjects.Count(), "Incorrect number of objects returned");

            var obj1 = sceneObjects.First() as BasicSceneSprite;
            Assert.IsNotNull(obj1, "First returned object was not a BasicSceneSrite");
            Assert.AreEqual(position, obj1.StartPosition, "Returned object's position was incorrect");
            Assert.AreEqual(assetId, obj1.AssetId, "Returned object's asset name was incorrect");
        }

        [Test]
        public void AssignsIncrementingIdToEachSprite()
        {
            var position = new Vector(5, 6);
            var assetId = SetupAsset();

            _manager.AddBasicSceneSprite(assetId, position);
            _manager.AddBasicSceneSprite(assetId, position);
            _manager.AddBasicSceneSprite(assetId, position);

            var objects = _manager.GetAllSceneObjects()
                                  .OrderBy(x => x.Id)
                                  .ToArray();

            Assert.AreEqual(1, objects[0].Id, "First object's ID was incorrect");
            Assert.AreEqual(2, objects[1].Id, "Second object's ID was incorrect");
            Assert.AreEqual(3, objects[2].Id, "Third object's ID was incorrect");
        }

        [Test]
        [ExpectedException(typeof (AssetNotFoundException))]
        public void ExceptionThrownIfSpritesAssetDoesNotExist()
        {
            var position = new Vector(5, 6);

            _manager.AddBasicSceneSprite(77, position);
        }

        [Test]
        public void AddedSpriteIsPassedToRenderer()
        {
            var position = new Vector(5, 6);
            var assetId = SetupAsset();

            _manager.AddBasicSceneSprite(assetId, position);
            _manager.Render();

            _mockedRenderer.Verify(x => x.RenderScene(It.Is<SceneSnapshot>(y => y.Sprites.Length == 1)),
                                   "Incorrect number of sprites was passed to renderer");

            _mockedRenderer.Verify(x => x.RenderScene(It.Is<SceneSnapshot>(y => y.Sprites[0].AssetId == assetId)),
                                   "Incorrect asset name passed to renderer");

            _mockedRenderer.Verify(x => x.RenderScene(It.Is<SceneSnapshot>(y => y.Sprites[0].Position == position)),
                                   "Incorrect position passed to renderer");
        }

        [Test]
        public void AddSpriteReturnsSpriteId()
        {
            var position = new Vector(5, 6);
            var assetId = SetupAsset();

            var id = _manager.AddBasicSceneSprite(assetId, position);
            Assert.AreEqual(1, id, "Returned object id was incorrect");
        }

        [Test]
        public void CanGetSpriteById()
        {
            var position = new Vector(5, 6);
            var assetId = SetupAsset();

            _manager.AddBasicSceneSprite(assetId, position);
            var id2 = _manager.AddBasicSceneSprite(assetId, new Vector(6, 7));
            _manager.AddBasicSceneSprite(assetId, new Vector(7, 8));

            var obj = _manager.GetObject(id2);
            Assert.IsNotNull(obj, "Null object returned");
            Assert.IsInstanceOf<BasicSceneSprite>(obj, "Object was of incorrect type");
            Assert.AreEqual(new Vector(6, 7), obj.StartPosition, "Returned object had an incorrect position");
        }

        [Test]
        public void CanDeleteObject()
        {
            var position = new Vector(5, 6);
            var assetId = SetupAsset();

            var id = _manager.AddBasicSceneSprite(assetId, position);
            _manager.DeleteObject(id);

            Assert.AreEqual(0, _manager.GetAllSceneObjects().Count(), "Manager contained an incorrect number of objects");
        }

        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void ExceptionThrownWhenDeletingNonexistantItem()
        {
            _manager.DeleteObject(2334);
        }

        [Test]
        public void CanSpecifyIdForSpriteWhenAdding()
        {
            const int testId = 356;
            var position = new Vector(5, 6);
            var assetId = SetupAsset();

            var returnedId =_manager.AddBasicSceneSprite(assetId, position, testId);
            var obj = _manager.GetAllSceneObjects().First();

            Assert.AreEqual(testId, returnedId, "Returned id did not match override id");
            Assert.AreEqual(testId, obj.Id, "Returned object did not have the override ID");
        }

        private int SetupAsset()
        {
            var testAssetStream = new MemoryStream();
            Asset asset;
            using (var writer = new StreamWriter(testAssetStream))
            {
                writer.Write("abcdefg");
                writer.Flush();
                testAssetStream.Position = 0;
                asset = new Asset("Test", testAssetStream) {Id = 25};
            }

            _mockedAssetManager.Setup(x => x.GetAsset(asset.Id))
                               .Returns(asset);

            return asset.Id;
        }
    }
}
