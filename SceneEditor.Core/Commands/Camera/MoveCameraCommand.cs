using SceneEditor.Core.General;

namespace SceneEditor.Core.Commands.Camera
{
    public class MoveCameraCommand : ICommand
    {
        public Vector MoveVector { get; set; }
        public bool MoveToExactPosition { get; set; }
    }
}
