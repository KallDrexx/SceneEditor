using System;

namespace SceneEditor.Core.Commands
{
    public class UndoDetails
    {
        public string CommandName { get; set; }
        public Action PerformUndo { get; set; }
        public Action PerformRedo { get; set; }
    }
}
