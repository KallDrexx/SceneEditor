using System;
using SceneEditor.Core.General;
using SceneEditor.Core.Rendering;

namespace SceneEditor.Core.SceneManagement
{
    public class SceneManager : ISceneManager
    {
        private readonly IRenderer _renderer;

        public SceneManager(IRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer");

            _renderer = renderer;
            CameraDimensions = new Vector(100, 100);
        }

        public Vector CameraPosition { get; private set; }
        public Vector CameraDimensions { get; private set; }

        public void MoveCameraBy(Vector moveBy)
        {
            CameraPosition += moveBy;
        }

        public void MoveCameraTo(Vector finalPosition)
        {
            CameraPosition = finalPosition;
        }

        public void SetCameraDimensions(Vector dimensions)
        {
            CameraDimensions = dimensions;
        }

        public void Render()
        {
            _renderer.RenderScene(new SceneSnapshot
            {
                CameraPosition = CameraPosition,
                RenderAreaDimensions = CameraDimensions,
                Sprites = new [] { new SceneSprite { AssetName = "arrow", Position = new Vector(5, 10)}}
            });
        }
    }
}
