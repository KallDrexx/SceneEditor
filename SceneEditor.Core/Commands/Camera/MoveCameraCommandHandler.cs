using System;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Core.Commands.Camera
{
    public class MoveCameraCommandHandler : ICommandHandler, IRequiresSceneManager
    {
        public Type HandledCommandType { get { return typeof (MoveCameraCommand); } }
        public ISceneManager SceneManager { get; set; }

        public void Execute(ICommand cmd)
        {
            if (cmd == null)
                throw new ArgumentNullException("cmd");

            var command = (MoveCameraCommand)cmd;

            if (command.MoveToExactPosition)
                SceneManager.MoveCameraTo(command.MoveVector);
            else
                SceneManager.MoveCameraBy(command.MoveVector);
        }
    }
}
