using SceneEditor.Core.Assets;

namespace SceneEditor.Core.Commands
{
    public interface IRequiresAssetManager
    {
        AssetManager AssetManager { get; set; }
    }
}
