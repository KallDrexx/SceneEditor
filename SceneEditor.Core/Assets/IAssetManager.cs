namespace SceneEditor.Core.Assets
{
    public interface IAssetManager
    {
        int AddAsset(Asset asset);
        Asset GetAsset(int id);
    }
}