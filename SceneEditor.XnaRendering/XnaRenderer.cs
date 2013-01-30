using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SceneEditor.Core.Rendering;

namespace SceneEditor.XnaRendering
{
    public class XnaRenderer : IRenderer
    {
        private GraphicsDeviceService _graphicsService;
        private BasicEffect _effect;
        private Stopwatch _timer;
        private IntPtr _handle;
        private int _width, _height;

        // Vertex positions and colors used to display a spinning triangle.
        public readonly VertexPositionColor[] _vertices =
        {
            new VertexPositionColor(new Vector3(-1, -1, 0), Color.Red),
            new VertexPositionColor(new Vector3( 1, -1, 0), Color.Aqua),
            new VertexPositionColor(new Vector3( 0,  1, 0), Color.Yellow),
        };

        public XnaRenderer(IntPtr windowHandle, int width, int height)
        {
            _graphicsService = new GraphicsDeviceService(windowHandle, width, height);
            SetViewport(width, height);

            _width = width;
            _height = height;
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

            if (_effect == null)
            {
                _effect = new BasicEffect(graphicsDevice)
                {
                    VertexColorEnabled = true
                };

                _timer = Stopwatch.StartNew();
            }

            graphicsDevice.Clear(Color.CornflowerBlue);

            // Spin the triangle according to how much time has passed.
            var time = (float)_timer.Elapsed.TotalSeconds;

            var yaw = time * 0.7f;
            var pitch = time * 0.8f;
            var roll = time * 0.9f;

            // Set transform matrices.
            float aspect = graphicsDevice.Viewport.AspectRatio;

            _effect.World = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);

            _effect.View = Matrix.CreateLookAt(new Vector3(0, 0, -5),
                                              Vector3.Zero, Vector3.Up);

            _effect.Projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 10);

            // Set renderstates.
            graphicsDevice.RasterizerState = RasterizerState.CullNone;

            // Draw the triangle.
            _effect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertices, 0, 1);

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
