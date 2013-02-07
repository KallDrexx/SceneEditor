using System;

namespace SceneEditor.Core.Commands
{
    public interface ICommandHandler
    {
        Type HandledCommandType { get; }
        void Execute(ICommand cmd);
    }
}
