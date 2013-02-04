using System;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.Commands;
using SceneEditor.Core.Exceptions;

namespace SceneEditor.Tests.Commands
{
    [TestFixture]
    public class CommandManagerTests
    {
        private CommandManager _manager;

        [SetUp]
        public void Setup()
        {
            _manager = new CommandManager();
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
    }
}
