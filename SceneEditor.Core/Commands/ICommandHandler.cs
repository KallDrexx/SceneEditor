namespace SceneEditor.Core.Commands
{
    public interface ICommandHandler
    {
        string CommandName { get; }
        void Execute(ICommand cmd);
    }
}
