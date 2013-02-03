using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneEditor.Core.Commands
{
    public interface IUndoableCommandHandler : ICommandHandler
    {
        void Undo();
    }
}
