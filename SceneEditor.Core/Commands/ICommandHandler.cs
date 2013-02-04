using System;

namespace SceneEditor.Core.Commands
{
    public interface ICommandHandler
    {
        Type HandledCommandType { get; }
        string CommandName { get; }
        void Execute(ICommand cmd);
    }
}
