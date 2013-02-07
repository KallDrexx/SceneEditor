using System;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Commands;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Tests.Commands.TestTypes
{
    public class TestCommandHandler : ICommandHandler, IRequiresSceneManager, IRequiresAssetManager
    {
        public Type HandledCommandType { get { return typeof (TestCommand); } }
        public string CommandName { get { return "Test Command"; } }
        
        public ISceneManager SceneManager
        {
            get { return StaticSceneManager; }
            set { StaticSceneManager = value; }
        }

        public IAssetManager AssetManager
        {
            get { return StaticAssetManager; }
            set { StaticAssetManager = value; }
        }

        public static ISceneManager StaticSceneManager { get; private set; }
        public static IAssetManager StaticAssetManager { get; private set; }

        public static Action OnExecuted;

        public void Execute(ICommand cmd)
        {
            if (OnExecuted != null)
                OnExecuted();
        }
    }
}
