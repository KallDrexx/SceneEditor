using System;
using SceneEditor.Core.Commands;

namespace SceneEditor.Tests.Commands
{
    public class InvalidCommandHandler : ICommandHandler
    {
        public Type HandledCommandType { get; private set; }
        public string CommandName { get; private set; }
        public void Execute(ICommand cmd)
        {
            throw new NotImplementedException();
        }
    }
}
