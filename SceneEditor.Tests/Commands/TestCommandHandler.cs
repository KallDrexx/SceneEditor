using System;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Commands;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Tests.Commands
{
    public class TestCommandHandler : ICommandHandler, IRequiresSceneManager, IRequiresAssetManager
    {
        public Type HandledCommandType { get { return typeof (TestCommand); } }
        public string CommandName { get { return "Test Command"; } }
        
        public SceneManager SceneManager
        {
            get { return StaticSceneManager; }
            set { StaticSceneManager = value; }
        }

        public AssetManager AssetManager
        {
            get { return StaticAssetManager; }
            set { StaticAssetManager = value; }
        }

        public static SceneManager StaticSceneManager { get; private set; }
        public static AssetManager StaticAssetManager { get; private set; }

        public static Action OnExecuted;

        public void Execute(ICommand cmd)
        {
            if (OnExecuted != null)
                OnExecuted();
        }
    }
}
