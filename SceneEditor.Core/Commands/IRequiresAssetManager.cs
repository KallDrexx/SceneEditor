using SceneEditor.Core.Assets;

namespace SceneEditor.Core.Commands
{
    public interface IRequiresAssetManager
    {
        IAssetManager AssetManager { get; set; }
    }
}
