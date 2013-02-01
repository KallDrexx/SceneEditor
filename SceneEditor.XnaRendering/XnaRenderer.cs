using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SceneEditor.Core.Rendering;

namespace SceneEditor.XnaRendering
{
    public class XnaRenderer : IRenderer
    {
        private readonly GraphicsDeviceService _graphicsService;
        private readonly IntPtr _handle;
        private readonly SpriteBatch _spriteBatch;
        private readonly Camera2D _camera;
        private Dictionary<string, Scene2DNode> _assetNodes;
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
            using (var stream = new FileStream("arrow.png", FileMode.Open))
            {
                var node = new Scene2DNode(Texture2D.FromStream(_graphicsService.GraphicsDevice, stream), new Vector2(0, 0));
                _assetNodes.Add("arrow", node);
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

        private void DrawSprites(IEnumerable<SceneSprite> sprites)
        {
            if (sprites == null)
                return;

            foreach (var sprite in sprites)
            {
                var node = _assetNodes[sprite.AssetName];
                node.WorldPosition = sprite.Position.ToXnaVector();
                _camera.Draw(node);
            }
        }
    }
}