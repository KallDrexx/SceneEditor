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
        private int _width, _height;
        private Scene2DNode _arrow;

        public XnaRenderer(IntPtr windowHandle, int width, int height)
        {
            _graphicsService = new GraphicsDeviceService(windowHandle, width, height);
            SetViewport(width, height);

            _width = width;
            _height = height;
            _handle = windowHandle;

            _spriteBatch = new SpriteBatch(_graphicsService.GraphicsDevice);
            _camera = new Camera2D(_spriteBatch);
        }

        public void ResetSize(int width, int height)
        {
            _graphicsService.ResetDevice(width, height);
            SetViewport(width, height);

            _height = height;
            _width = width;
        }

        public void RenderScene(SceneSnapshot snapshot)
        {
            var graphicsDevice = _graphicsService.GraphicsDevice;
            graphicsDevice.Clear(Color.CornflowerBlue);

            if (_arrow == null)
            {
                using (var stream = new FileStream("arrow.png", FileMode.Open))
                    _arrow = new Scene2DNode(Texture2D.FromStream(_graphicsService.GraphicsDevice, stream), new Vector2(50, 50));
                    
            }

            if (snapshot != null)
            {
                _camera.Position = new Vector2(snapshot.CameraPosition.X, snapshot.CameraPosition.Y);

                if (snapshot.Sprites != null && snapshot.Sprites.Length >= 1)
                    _arrow.WorldPosition = new Vector2(snapshot.Sprites[0].Position.X, snapshot.Sprites[0].Position.Y);
            }

            _spriteBatch.Begin();
            _camera.Draw(_arrow);
            _spriteBatch.End();

            graphicsDevice.Present(new Rectangle(0, 0, _width, _height), null, _handle);
        }

        public void Dispose()
        {
            _graphicsService.Dispose();
        }

        private void SetViewport(int width, int height)
        {
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
    }
}