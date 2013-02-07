using System;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.Commands;
using SceneEditor.Core.Commands.Camera;
using SceneEditor.Core.General;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Tests.Commands.Camera
{
    [TestFixture]
    public class MoveCameraCommandHandlerTests
    {
        private MoveCameraCommandHandler _handler;
        private Mock<ISceneManager> _mockedSceneManager;

        [SetUp]
        public void Setup()
        {
            _handler = new MoveCameraCommandHandler();

            _mockedSceneManager = new Mock<ISceneManager>();
            _handler.SceneManager = _mockedSceneManager.Object;
        }

        [Test]
        public void HandlerHandlesMoveCameraCommand()
        {
            Assert.AreEqual(typeof (MoveCameraCommand), _handler.HandledCommandType,
                            "Handler's reported handled command type is incorrect");
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ThrowsExceptionOnNullCommands()
        {
            _handler.Execute(null);
        }

        [Test]
        [ExpectedException(typeof (InvalidCastException))]
        public void ThrowsExceptionOnInvalidCommandType()
        {
            var mockedCommand = new Mock<ICommand>();
            _handler.Execute(mockedCommand.Object);
        }

        [Test]
        public void HandlerCorrectlyMovesCamera()
        {
            var moveVector = new Vector(5, 6);
            var cmd = new MoveCameraCommand {MoveVector = moveVector};
            _handler.Execute(cmd);

            _mockedSceneManager.Verify(x => x.MoveCameraBy(moveVector));
        }

        [Test]
        public void HandlerMovesCameraToExactPosition()
        {
            var moveVector = new Vector(5, 6);
            var cmd = new MoveCameraCommand { MoveVector = moveVector, MoveToExactPosition = true };
            _handler.Execute(cmd);

            _mockedSceneManager.Verify(x => x.MoveCameraTo(moveVector));
        }
    }
}
