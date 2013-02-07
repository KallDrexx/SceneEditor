using SceneEditor.Core.Commands;

namespace SceneEditor.Tests.Commands.TestTypes
{
    public class NullUndoCommand : ICommand
    {
        public string Name { get { return "test"; } }
    }
}
