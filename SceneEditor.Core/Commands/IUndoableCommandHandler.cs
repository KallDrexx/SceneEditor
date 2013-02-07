namespace SceneEditor.Core.Commands
{
    public interface IUndoableCommandHandler : ICommandHandler
    {
        UndoDetails LastExecutionUndoDetails { get; }
    }
}
