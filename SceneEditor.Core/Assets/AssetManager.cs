using System;
using System.Collections.Generic;

namespace SceneEditor.Core.Assets
{
    public class AssetManager
    {
        private readonly Dictionary<string, Asset> _assets;

        public AssetManager()
        {
            _assets = new Dictionary<string, Asset>();
        }

        public void AddAsset(Asset asset)
        {
            _assets.Add(asset.Name, asset);
        }

        public Asset GetAsset(string name)
        {
            return _assets[name];
        }
    }
}
