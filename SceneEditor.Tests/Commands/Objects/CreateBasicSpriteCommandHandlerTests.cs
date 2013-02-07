using System;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.Assets;
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

            _mockedSceneManager.Verify(x => x.AddBasicSceneSprite(assetId, position));
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

            _mockedSceneManager.Verify(x => x.AddBasicSceneSprite(assetId, position + cameraPos));

        }
    }
}
