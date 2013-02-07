using System;

namespace SceneEditor.Core.Exceptions
{
    public class AssetNotFoundException : InvalidOperationException
    {
        public int AssetId { get; private set; }

        public AssetNotFoundException(int assetId)
            : base("No asset exists with an id of " + assetId)
        {
            AssetId = assetId;
        }
    }
}
