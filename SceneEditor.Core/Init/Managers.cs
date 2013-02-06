using System;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Commands;
using SceneEditor.Core.Rendering;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Core.Init
{
    public static class Managers
    {
        public static void Init(IRenderer renderer, out ISceneManager sceneManager, out IAssetManager assetManager, out ICommandManager commandManager)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer");

            assetManager = new AssetManager();
            sceneManager = new SceneManager(renderer, assetManager);
            commandManager = new CommandManager(sceneManager, assetManager);

            renderer.AssetManager = assetManager;
        }
    }
}
