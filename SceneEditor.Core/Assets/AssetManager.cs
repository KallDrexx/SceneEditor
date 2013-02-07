using System.Collections.Generic;

namespace SceneEditor.Core.Assets
{
    public class AssetManager : IAssetManager
    {
        private readonly Dictionary<int, Asset> _assets;
        private int _currentAssetId;

        public AssetManager()
        {
            _assets = new Dictionary<int, Asset>();
        }

        public int AddAsset(Asset asset)
        {
            asset.Id = (++_currentAssetId);
            _assets.Add(asset.Id, asset);
            return asset.Id;
        }

        public Asset GetAsset(int id)
        {
            Asset foundAsset;
            _assets.TryGetValue(id, out foundAsset);
            return foundAsset;
        }
    }
}
