using SceneEditor.Core.General;

namespace SceneEditor.Core.SceneManagement
{
    public class SceneManager
    {
        public Vector CameraPosition { get; private set; }

        public void MoveCameraBy(Vector moveBy)
        {
            CameraPosition += moveBy;
        }

        public void MoveCameraTo(Vector finalPosition)
        {
            CameraPosition = finalPosition;
        }
    }
}
