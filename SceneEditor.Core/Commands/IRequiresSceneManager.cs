using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Core.Commands
{
    public interface IRequiresSceneManager
    {
        ISceneManager SceneManager { get; set; }
    }
}
