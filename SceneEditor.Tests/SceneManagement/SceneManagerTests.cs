using System;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.General;
using SceneEditor.Core.Rendering;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Tests.SceneManagement
{
    [TestFixture]
    public class SceneManagerTests
    {
        private SceneManager _manager;
        private Mock<IRenderer> _mockedRenderer;

        [SetUp]
        public void Setup()
        {
            _mockedRenderer = new Mock<IRenderer>();
            _manager = new SceneManager(_mockedRenderer.Object);
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
            new SceneManager(null);
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
    }
}
