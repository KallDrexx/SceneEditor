using System;
using Microsoft.Xna.Framework.Graphics;

namespace SceneEditor.XnaRendering
{
    internal class GraphicsDeviceService : IGraphicsDeviceService, IDisposable
    {
        private readonly PresentationParameters _presentationParams;
        private readonly GraphicsDevice _graphicsDevice;

        public GraphicsDevice GraphicsDevice { get { return _graphicsDevice; } }

        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;
        public event EventHandler<EventArgs> DeviceCreated;

        public GraphicsDeviceService(IntPtr windowHandle, int width, int height)
        {
            _presentationParams = new PresentationParameters
            {
                BackBufferFormat = SurfaceFormat.Color,
                BackBufferHeight = Math.Max(height, 1),
                BackBufferWidth = Math.Max(width, 1),
                DepthStencilFormat = DepthFormat.Depth24,
                DeviceWindowHandle = windowHandle,
                PresentationInterval = PresentInterval.Immediate
            };

            _graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, GraphicsProfile.Reach,
                                                 _presentationParams);
        }

        public void ResetDevice(int width, int height)
        {
            if (DeviceResetting != null)
                DeviceResetting(this, EventArgs.Empty);

            _presentationParams.BackBufferHeight = Math.Max(height, 1);
            _presentationParams.BackBufferWidth = Math.Max(width, 1);
            _graphicsDevice.Reset(_presentationParams);

            if (DeviceReset != null)
                DeviceReset(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            if (DeviceDisposing != null)
                DeviceDisposing(this, EventArgs.Empty);

            _graphicsDevice.Dispose();
        }        
    }
}
