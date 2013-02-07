using System;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.Commands.Objects;
using SceneEditor.Core.General;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Tests.Commands.Objects
{
    [TestFixture]
    public class CreateBasicSpriteCommandHandlerTests
    {
        private CreateBasicSpriteCommandHandler _handler;
        private Mock<ISceneManager> _mockedSceneManager;

        [SetUp]
        public void Setup()
        {
            _mockedSceneManager = new Mock<ISceneManager>();

            _handler = new CreateBasicSpriteCommandHandler
            {
                SceneManager = _mockedSceneManager.Object
            };
        }

        [Test]
        public void CreatesCorrectBasicSpriteToScene()
        {
            const int assetId = 23;
            var position = new Vector(25, 33);
            var cmd = new CreateBasicSpriteCommand
            {
                AssetId = assetId,
                Position = position
            };

            _handler.Execute(cmd);

            _mockedSceneManager.Verify(x => x.AddBasicSceneSprite(assetId, position, null));
        }

        [Test]
        public void HandlerReportsHandlingCorrectCommandType()
        {
            Assert.AreEqual(typeof (CreateBasicSpriteCommand), _handler.HandledCommandType,
                            "Incorrect handled command type reported");
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ThrowsExceptionOnNullCommand()
        {
            _handler.Execute(null);
        }

        [Test]
        public void CanCreateSpriteRelativeToCameraPosition()
        {
            const int assetId = 23;
            var position = new Vector(25, 33);
            var cameraPos = new Vector(12, 43);
            _mockedSceneManager.SetupGet(x => x.CameraPosition).Returns(cameraPos);

            var cmd = new CreateBasicSpriteCommand
            {
                AssetId = assetId,
                Position = position,
                PositionIsRelativeToCamera = true
            };

            _handler.Execute(cmd);

            _mockedSceneManager.Verify(x => x.AddBasicSceneSprite(assetId, position + cameraPos, null));

        }

        [Test]
        public void ExecuteCreatesNotNullUndoAndRedoActions()
        {
            var cmd = new CreateBasicSpriteCommand();
            _handler.Execute(cmd);

            Assert.IsNotNull(_handler.LastExecutionUndoDetails, "LastExecutionUndoDetails was null");
            Assert.IsNotNull(_handler.LastExecutionUndoDetails.PerformUndo, "PerformUndo delegate was null");
            Assert.IsNotNull(_handler.LastExecutionUndoDetails.PerformRedo, "PerformRedo delegate was null");
        }

        [Test]
        public void ExecuteCreatesUndoDetailsWithCorrectCommandName()
        {
            var cmd = new CreateBasicSpriteCommand();
            _handler.Execute(cmd);

            Assert.AreEqual(cmd.Name, _handler.LastExecutionUndoDetails.CommandName);
        }

        [Test]
        public void UndoDeletesSceneObject()
        {
            const int assetId = 23;
            const int newObjId = 55;

            var position = new Vector(25, 33);
            _mockedSceneManager.Setup(x => x.AddBasicSceneSprite(assetId, position, null)).Returns(newObjId);
            
            var cmd = new CreateBasicSpriteCommand
            {
                AssetId = assetId,
                Position = position
            };

            _handler.Execute(cmd);
            _handler.LastExecutionUndoDetails.PerformUndo(_handler.LastExecutionUndoDetails);

            _mockedSceneManager.Verify(x => x.DeleteObject(newObjId));
        }

        [Test]
        public void RedoReAddsSprite()
        {
            const int assetId = 23;
            var position = new Vector(25, 33);
            var cmd = new CreateBasicSpriteCommand
            {
                AssetId = assetId,
                Position = position
            };

            _handler.Execute(cmd);
            _handler.LastExecutionUndoDetails.PerformRedo(_handler.LastExecutionUndoDetails);

            _mockedSceneManager.Verify(x => x.AddBasicSceneSprite(assetId, position, null), Times.Exactly(2));
        }
    }
}
