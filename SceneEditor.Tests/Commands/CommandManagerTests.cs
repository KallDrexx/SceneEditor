using System;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Commands;
using SceneEditor.Core.Exceptions;
using SceneEditor.Core.Rendering;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Tests.Commands
{
    [TestFixture]
    public class CommandManagerTests
    {
        private CommandManager _manager;
        private Mock<SceneManager> _mockedSceneManager;
        private Mock<AssetManager> _mockedAssetManager;
        private Mock<IRenderer> _mockedRenderer;

        [SetUp]
        public void Setup()
        {
            _mockedRenderer = new Mock<IRenderer>();
            _mockedSceneManager = new Mock<SceneManager>(_mockedRenderer.Object);
            _mockedAssetManager = new Mock<AssetManager>();
            _manager = new CommandManager(_mockedSceneManager.Object, _mockedAssetManager.Object);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ExceptionThrownWhenNullCommandExecuted()
        {
            _manager.Execute(null);
        }

        [Test]
        [ExpectedException(typeof(NoCommandHandlerForCommandException))]
        public void CommandWithoutHandlerThrowsException()
        {
            var mockedCommand = new Mock<ICommand>();
            _manager.Execute(mockedCommand.Object);
        }

        [Test]
        public void KnownHandlerIsInitializedAndExecutesAssociatedCommand()
        {
            var executionOccured = false;
            TestCommandHandler.OnExecuted = delegate { executionOccured = true; };
            _manager.Execute(new TestCommand());

            Assert.IsTrue(executionOccured, "Execution did not occur");
        }

        [Test]
        public void SceneManagerRequiredCommandHandlerHasSceneManagerSet()
        {
            Assert.IsNotNull(TestCommandHandler.StaticSceneManager, "SceneHandler was not set");
            Assert.AreEqual(_mockedSceneManager.Object, TestCommandHandler.StaticSceneManager,
                            "Scenehandler was not correct");
        }

        [Test]
        public void AssetManagerRequiredCommandHandlerHasAssetManagerSet()
        {
            Assert.IsNotNull(TestCommandHandler.StaticAssetManager, "AssetManager was not set");
            Assert.AreEqual(_mockedAssetManager.Object, TestCommandHandler.StaticAssetManager,
                            "AssetManager was not correct");
        }
    }
}
