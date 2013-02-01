using SceneEditor.Core.General;

namespace SceneEditor.Core.SceneManagement
{
    public class SceneManager
    {
        public SceneManager()
        {
            CameraDimensions = new Vector(100, 100);
        }

        public Vector CameraPosition { get; private set; }
        public Vector CameraDimensions { get; private set; }

        public void MoveCameraBy(Vector moveBy)
        {
            CameraPosition += moveBy;
        }

        public void MoveCameraTo(Vector finalPosition)
        {
            CameraPosition = finalPosition;
        }

        public void SetCameraDimensions(Vector dimensions)
        {
            CameraDimensions = dimensions;
        }
    }
}
