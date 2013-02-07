using System;

namespace SceneEditor.Core.Commands
{
    public class UndoDetails
    {
        public string CommandName { get; set; }
        public Action<UndoDetails> PerformUndo { get; set; }
        public Action<UndoDetails> PerformRedo { get; set; }
    }
}
