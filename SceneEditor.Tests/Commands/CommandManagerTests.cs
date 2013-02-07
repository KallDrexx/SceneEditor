using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Commands;
using SceneEditor.Core.Exceptions;
using SceneEditor.Core.SceneManagement;
using SceneEditor.Tests.Commands.TestTypes;

namespace SceneEditor.Tests.Commands
{
    [TestFixture]
    public class CommandManagerTests
    {
        private CommandManager _manager;
        private Mock<ISceneManager> _mockedSceneManager;
        private Mock<IAssetManager> _mockedAssetManager;

        [SetUp]
        public void Setup()
        {
            _mockedSceneManager = new Mock<ISceneManager>();
            _mockedAssetManager = new Mock<IAssetManager>();
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

        [Test]
        public void DoesNotThrowExceptionWithHandlerWithNullType()
        {
            // InvalidHandler has a null type, and thus shouldn't cause the crash 
            //    upon instantiation
        }

        [Test]
        public void CanGetUndoDetailsFromUndoableCommandHandler()
        {
            var cmd = new UndoableCommand();
            _manager.Execute(cmd);

            Assert.IsNotNull(_manager.UndoableCommandNames, "Undoable commands enumerable was null");

            var list = _manager.UndoableCommandNames.ToArray();
            Assert.AreEqual(1, list.Length, "Undoable commands list has an incorrect number of elements");
            Assert.AreEqual(cmd.Name, list[0], "First undoable command name was incorrect");
        }

        [Test]
        public void DoesNotAddNullUndoDetailsToUndoCommandList()
        {
            var cmd = new NullUndoCommand();
            _manager.Execute(cmd);

            Assert.IsNotNull(_manager.UndoableCommandNames, "Undoable commands enumerable was null");
            Assert.AreEqual(0, _manager.UndoableCommandNames.Count(),
                            "Undoable commands enumerable had an incorrect number of elements");
        }

        [Test]
        public void CallingUndoCallsUndoAction()
        {
            var undoCalled = false;
            var cmd = new UndoableCommand
            {
                OnUndo = delegate { undoCalled = true; }
            };

            _manager.Execute(cmd);
            _manager.UndoLastCommand();

            Assert.IsTrue(undoCalled, "Undo delegate was not called");
            Assert.IsNotNull(_manager.UndoableCommandNames, "Undoable commands enumerable was null");
            Assert.AreEqual(0, _manager.UndoableCommandNames.Count(),
                            "Undoable commands enumerable had an incorrect number of elements");
        }

        [Test]
        public void NoErrorOccursWithNoUndoableActions()
        {
            _manager.UndoLastCommand();
        }
    }
}
