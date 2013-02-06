using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SceneEditor.Core.Assets;
using SceneEditor.Core.Rendering;

namespace SceneEditor.XnaRendering
{
    public class XnaRenderer : IRenderer
    {
        private readonly GraphicsDeviceService _graphicsService;
        private readonly IntPtr _handle;
        private readonly SpriteBatch _spriteBatch;
        private readonly Camera2D _camera;
        private readonly Dictionary<string, Scene2DNode> _assetNodes;
        private IAssetManager _assetManager;
        private int _width, _height;

        public XnaRenderer(IntPtr windowHandle, int width, int height)
        {
            _graphicsService = new GraphicsDeviceService(windowHandle, width, height);
            SetViewport(width, height);

            _width = width;
            _height = height;
            _handle = windowHandle;

            _spriteBatch = new SpriteBatch(_graphicsService.GraphicsDevice);
            _camera = new Camera2D(_spriteBatch);

            _assetNodes = new Dictionary<string, Scene2DNode>();
        }

        public IAssetManager AssetManager
        {
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                _assetManager = value;
            }
        }

        public void RenderScene(SceneSnapshot snapshot)
        {
            if (_width != (int) snapshot.RenderAreaDimensions.X || _height != (int) snapshot.RenderAreaDimensions.Y)
                SetViewport((int) snapshot.RenderAreaDimensions.X, (int) snapshot.RenderAreaDimensions.Y);
                

            var graphicsDevice = _graphicsService.GraphicsDevice;
            graphicsDevice.Clear(Color.CornflowerBlue);

            _camera.Position = new Vector2(snapshot.CameraPosition.X, snapshot.CameraPosition.Y);

            _spriteBatch.Begin();
            DrawSprites(snapshot.Sprites);
            _spriteBatch.End();

            graphicsDevice.Present(new Rectangle(0, 0, _width, _height), null, _handle);
        }

        public void Dispose()
        {
            _graphicsService.Dispose();
        }

        private void SetViewport(int width, int height)
        {
            _graphicsService.ResetDevice(width, height);
            _width = width;
            _height = height;

            var viewPort = new Viewport
            {
                X = 0,
                Y = 0,
                Width = width,
                Height = height,
                MinDepth = 0,
                MaxDepth = 1
            };
            _graphicsService.GraphicsDevice.Viewport = viewPort;
        }

        private void DrawSprites(IEnumerable<RenderSprite> sprites)
        {
            if (sprites == null)
                return;

            foreach (var sprite in sprites)
            {
                Scene2DNode node;
                if (!_assetNodes.TryGetValue(sprite.AssetName, out node))
                {
                    // No node saved for this asset so create one
                    var asset = _assetManager.GetAsset(sprite.AssetName);
                    if (asset == null)
                        throw new InvalidOperationException("Renderer called to render asset that does not exist: " +
                                                            sprite.AssetName);

                    var texture = Texture2D.FromStream(_graphicsService.GraphicsDevice, asset.Stream);
                    node = new Scene2DNode(texture, new Vector2(0, 0));

                    _assetNodes.Add(sprite.AssetName, node);
                }

                node.WorldPosition = sprite.Position.ToXnaVector();
                _camera.Draw(node);
            }
        }
    }
}