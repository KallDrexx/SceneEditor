using System;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Core.Commands.Objects
{
    public class CreateBasicSpriteCommandHandler : ICommandHandler, IRequiresSceneManager
    {
        public Type HandledCommandType { get { return typeof (CreateBasicSpriteCommand); } }
        public ISceneManager SceneManager { get; set; }

        public void Execute(ICommand cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            var command = (CreateBasicSpriteCommand)cmd;
            var finalPosition = command.PositionIsRelativeToCamera
                                    ? command.Position + SceneManager.CameraPosition
                                    : command.Position;

            SceneManager.AddBasicSceneSprite(command.AssetId, finalPosition);
        }
    }
}
