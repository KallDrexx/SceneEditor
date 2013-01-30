using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int _width, _height;
        private Texture2D _arrow;

        public XnaRenderer(IntPtr windowHandle, int width, int height)
        {
            _graphicsService = new GraphicsDeviceService(windowHandle, width, height);
            SetViewport(width, height);

            _width = width;
            _height = height;
            _handle = windowHandle;

            _spriteBatch = new SpriteBatch(_graphicsService.GraphicsDevice);

            using (var stream = new FileStream("arrow.png", FileMode.Open))
                _arrow = Texture2D.FromStream(_graphicsService.GraphicsDevice, stream);
        }

        public void ResetSize(int width, int height)
        {
            _graphicsService.ResetDevice(width, height);
            SetViewport(width, height);

            _height = height;
            _width = width;
        }

        public void RenderScene(IEnumerable<SceneRenderObject> sceneObjects)
        {
            var graphicsDevice = _graphicsService.GraphicsDevice;
            graphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(_arrow, new Rectangle(0, 0, _arrow.Width, _arrow.Height), Color.White);
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
