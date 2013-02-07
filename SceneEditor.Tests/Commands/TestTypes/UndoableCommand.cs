using System;
using SceneEditor.Core.Commands;

namespace SceneEditor.Tests.Commands.TestTypes
{
    public class UndoableCommand : ICommand
    {
        public string Name { get { return "Undoable Test"; } }
        public Action<UndoDetails> OnUndo { get; set; }
        public Action<UndoDetails> OnRedo { get; set; }
    }
}
