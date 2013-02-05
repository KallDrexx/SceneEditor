namespace SceneEditor.Core.Commands
{
    public interface ICommandManager
    {
        void Execute(ICommand cmd);
    }
}