using System;
using System.Collections.Generic;
using System.Linq;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Exceptions;
using SceneEditor.Core.General;
using SceneEditor.Core.Rendering;
using SceneEditor.Core.SceneManagement.Objects;

namespace SceneEditor.Core.SceneManagement
{
    public class SceneManager : ISceneManager
    {
        private readonly IRenderer _renderer;
        private readonly IAssetManager _assetManager;
        private readonly List<ISceneObject> _sceneObjects;
        private int _currentObjectId;

        public SceneManager(IRenderer renderer, IAssetManager assetManager)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer");

            if (assetManager == null)
                throw new ArgumentNullException("assetManager");

            _renderer = renderer;
            _assetManager = assetManager;
            _sceneObjects = new List<ISceneObject>();
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
                Sprites = _sceneObjects.OfType<BasicSceneSprite>()
                                       .Select(x => new RenderSprite
                                       {
                                           AssetName = x.AssetName,
                                           Position = x.StartPosition
                                       })
                                       .ToArray()
            });
        }

        public int AddBasicSceneSprite(string assetName, Vector position, Vector size)
        {
            if (_assetManager.GetAsset(assetName) == null)
                throw new AssetNotFoundException(assetName);

            var sprite = new BasicSceneSprite
            {
                Id = (++_currentObjectId),
                AssetName = assetName,
                StartPosition = position,
                Dimensions = size
            };

            _sceneObjects.Add(sprite);
            return sprite.Id;
        }

        public IEnumerable<ISceneObject> GetAllSceneObjects()
        {
            return _sceneObjects;
        }

        public ISceneObject GetObject(int id)
        {
            return _sceneObjects.FirstOrDefault(x => x.Id == id);
        }
    }
}
