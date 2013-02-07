using System;
using SceneEditor.Core.Commands;

namespace SceneEditor.Tests.Commands.TestTypes
{
    public class UndoableCommandHandler : IUndoableCommandHandler
    {
        public Type HandledCommandType { get { return typeof (UndoableCommand); } }
        public UndoDetails LastExecutionUndoDetails { get; private set; }

        public void Execute(ICommand cmd)
        {
            LastExecutionUndoDetails = new UndoDetails
            {
                CommandName = cmd.Name,
                PerformUndo = ((UndoableCommand)cmd).OnUndo,
                PerformRedo = ((UndoableCommand)cmd).OnRedo
            };
        }
    }
}
