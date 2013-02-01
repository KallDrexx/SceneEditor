using NUnit.Framework;
using SceneEditor.Core.General;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Tests.SceneManagement
{
    [TestFixture]
    public class SceneManagerTests
    {
        [Test]
        public void StartsWithCameraAtOrigin()
        {
            var manager = new SceneManager();
            var position = manager.CameraPosition;

            Assert.AreEqual(0, position.X, "Camera's X value is incorrect");
            Assert.AreEqual(0, position.Y, "Camera's Y value is incorrect");
        }

        [Test]
        public void CanMoveCameraByAmount()
        {
            var manager = new SceneManager();
            var moveBy = new Vector(2, 3);

            // Since camera starts at 0,0, we have to do 2 moves to 
            //  verify it has moved by the amount specified instead of a 
            //  absolute position
            manager.MoveCameraBy(moveBy);
            manager.MoveCameraBy(moveBy);

            var expectedResult = (moveBy + moveBy);
            Assert.AreEqual(expectedResult, manager.CameraPosition, "Camera was not moved to the correct spot");
        }

        [Test]
        public void CanMoveCameraToExactPosition()
        {
            var manager = new SceneManager();
            var finalPosition = new Vector(2, 3);

            // Since the camera starts at 0,0 we have to do 2 moves
            //  to verify that the camera isn't just moving by the 
            //  vector's values but instead moving to the exact spot
            manager.MoveCameraTo(finalPosition);
            manager.MoveCameraTo(finalPosition);

            Assert.AreEqual(finalPosition, manager.CameraPosition, "Camera was not moved to the correct position");
        }

        [Test]
        public void CanSetCameraDimensions()
        {
            var manager = new SceneManager();
            var dimensions = new Vector(500, 300);
            manager.SetCameraDimensions(dimensions);
            Assert.AreEqual(dimensions, manager.CameraDimensions, "Camera's dimensions were incorrect");
        }

        [Test]
        public void CameraDimensionsStartAt100X100()
        {
            var manager = new SceneManager();
            var expectedResult = new Vector(100, 100);

            Assert.AreEqual(expectedResult, manager.CameraDimensions, "Camera dimensions were incorrect");
        }
    }
}
