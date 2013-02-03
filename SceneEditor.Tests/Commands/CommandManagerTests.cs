using System;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.Commands;

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
        [ExpectedException(typeof (InvalidOperationException))]
        public void CommandWithoutHandlerThrowsException()
        {
            var mockedCommand = new Mock<ICommand>();
            _manager.Execute(mockedCommand.Object);
        }
    }
}
