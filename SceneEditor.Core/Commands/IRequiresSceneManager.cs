using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Core.Commands
{
    public interface IRequiresSceneManager
    {
        SceneManager SceneManager { get; set; }
    }
}
