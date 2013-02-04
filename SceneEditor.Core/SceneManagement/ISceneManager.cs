using SceneEditor.Core.General;

namespace SceneEditor.Core.SceneManagement
{
    public interface ISceneManager
    {
        Vector CameraPosition { get; }
        Vector CameraDimensions { get; }
        void MoveCameraBy(Vector moveBy);
        void MoveCameraTo(Vector finalPosition);
        void SetCameraDimensions(Vector dimensions);
        void Render();
    }
}