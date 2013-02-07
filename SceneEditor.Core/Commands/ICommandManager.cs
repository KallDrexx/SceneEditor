using System.Collections.Generic;

namespace SceneEditor.Core.Commands
{
    public interface ICommandManager
    {
        IEnumerable<string> UndoableCommandNames { get; }
        bool CanRedo { get; }
        void Execute(ICommand cmd);
        void UndoLastCommand();
        void RedoLastUndoneCommand();
    }
}