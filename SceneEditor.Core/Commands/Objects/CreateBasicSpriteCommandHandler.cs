using System;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Core.Commands.Objects
{
    public class CreateBasicSpriteCommandHandler : IUndoableCommandHandler, IRequiresSceneManager
    {
        public Type HandledCommandType { get { return typeof (CreateBasicSpriteCommand); } }
        public ISceneManager SceneManager { get; set; }
        public UndoDetails LastExecutionUndoDetails { get; private set; }

        public void Execute(ICommand cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            var command = (CreateBasicSpriteCommand)cmd;
            var finalPosition = command.PositionIsRelativeToCamera
                                    ? command.Position + SceneManager.CameraPosition
                                    : command.Position;

            var objectId = SceneManager.AddBasicSceneSprite(command.AssetId, finalPosition);

            LastExecutionUndoDetails = new UndoDetails
            {
                CommandName = cmd.Name,
                PerformUndo = undo => SceneManager.DeleteObject(objectId),
                PerformRedo = undo => SceneManager.AddBasicSceneSprite(command.AssetId, finalPosition, objectId)
            };
        }
    }
}
