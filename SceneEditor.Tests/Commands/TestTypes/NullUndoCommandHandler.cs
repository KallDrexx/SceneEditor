using System;
using SceneEditor.Core.Commands;

namespace SceneEditor.Tests.Commands.TestTypes
{
    public class NullUndoCommandHandler : IUndoableCommandHandler
    {
        public Type HandledCommandType { get { return typeof (NullUndoCommand); } }
        public UndoDetails LastExecutionUndoDetails { get; private set; }

        public void Execute(ICommand cmd)
        {
            LastExecutionUndoDetails = null;
        }
    }
}
