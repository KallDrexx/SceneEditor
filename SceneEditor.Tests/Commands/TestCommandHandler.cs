using System;
using SceneEditor.Core.Commands;

namespace SceneEditor.Tests.Commands
{
// ReSharper disable ClassNeverInstantiated.Global
    public class TestCommandHandler : ICommandHandler
// ReSharper restore ClassNeverInstantiated.Global
    {
        public Type HandledCommandType { get { return typeof (TestCommand); } }
        public string CommandName { get { return "Test Command"; } }

        public static Action OnExecuted;

        public void Execute(ICommand cmd)
        {
            if (OnExecuted != null)
                OnExecuted();
        }
    }
}
