using System;
using Moq;
using NUnit.Framework;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Commands;
using SceneEditor.Core.Init;
using SceneEditor.Core.Rendering;
using SceneEditor.Core.SceneManagement;

namespace SceneEditor.Tests.Init
{
    [TestFixture]
    public class ManagerInitTests
    {
        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void InitThrowsExceptionWhenNullRendererPassedIn()
        {
            ISceneManager sceneManager;
            IAssetManager assetManager;
            ICommandManager commandManager;
            Managers.Init(null, out sceneManager, out assetManager, out commandManager);
        }

        [Test]
        public void InitCreatesSceneAssetAndCommandManagers()
        {
            var mockedRenderer = new Mock<IRenderer>();

            ISceneManager sceneManager;
            IAssetManager assetManager;
            ICommandManager commandManager;
            Managers.Init(mockedRenderer.Object, out sceneManager, out assetManager, out commandManager);

            Assert.IsNotNull(sceneManager, "Scene manager was null");
            Assert.IsNotNull(assetManager, "Asset manager was null");
            Assert.IsNotNull(commandManager, "Command manager was null");
        }

        [Test]
        public void InitSetsRenderersAssetManager()
        {
            var mockedRenderer = new Mock<IRenderer>();

            ISceneManager sceneManager;
            IAssetManager assetManager;
            ICommandManager commandManager;
            Managers.Init(mockedRenderer.Object, out sceneManager, out assetManager, out commandManager);

            mockedRenderer.VerifySet(x => x.AssetManager = assetManager);
        }
    }
}
