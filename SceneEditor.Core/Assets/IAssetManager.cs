namespace SceneEditor.Core.Assets
{
    public interface IAssetManager
    {
        void AddAsset(Asset asset);
        Asset GetAsset(string name);
    }
}