using SceneEditor.Core.General;

namespace SceneEditor.Core.SceneManagement.Objects
{
    public interface ISceneObject
    {
        int Id { get; set; }
        Vector StartPosition { get; set; }
    }
}
