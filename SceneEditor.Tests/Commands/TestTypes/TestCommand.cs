using SceneEditor.Core.Commands;

namespace SceneEditor.Tests.Commands.TestTypes
{
    public class TestCommand : ICommand
    {
        public string Name { get { return "test"; } }
    }
}
