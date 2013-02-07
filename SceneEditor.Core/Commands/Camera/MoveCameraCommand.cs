using SceneEditor.Core.General;

namespace SceneEditor.Core.Commands.Camera
{
    public class MoveCameraCommand : ICommand
    {
        public string Name { get { return "Move Camera"; } }
        public Vector MoveVector { get; set; }
        public bool MoveToExactPosition { get; set; }
    }
}
