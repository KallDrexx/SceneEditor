using System;

namespace SceneEditor.Core.Exceptions
{
    public class AssetNotFoundException : InvalidOperationException
    {
        public string AssetName { get; private set; }

        public AssetNotFoundException(string assetName)
            : base("No asset exists with a name of " + assetName)
        {
            AssetName = assetName;
        }
    }
}
