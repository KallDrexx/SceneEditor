using System;
using System.Drawing;
using System.Windows.Forms;
using SceneEditor.Core.General;
using SceneEditor.Core.Rendering;
using SceneEditor.Core.SceneManagement;
using SceneEditor.XnaRendering;

namespace SceneEditor.Windows.Controls
{
    public partial class RenderArea : Control
    {
        private IRenderer _renderer;
        private SceneManager _sceneManager;
        private bool _dragInProgress;
        private Point _prevDragPosition;

        public RenderArea()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                // Use a picturebox to render the XNA view so we do not get flicker
                //  and so we still can get notified of mouse events
                var pb = new PictureBox
                {
                    Parent = this,
                    Dock = DockStyle.Fill
                };

                pb.MouseMove += RenderArea_MouseMove;
                pb.MouseDown += RenderArea_MouseDown;
                pb.MouseUp += RenderArea_MouseUp;

                _renderer = new XnaRenderer(pb.Handle, pb.ClientSize.Width, pb.ClientSize.Height);
                _sceneManager = new SceneManager(_renderer);
                Application.Idle += delegate { Invalidate(); };

                var area = new Vector(ClientSize.Width, ClientSize.Height);
                _sceneManager.SetCameraDimensions(area);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (DesignMode)
            {
                base.OnPaint(e);
                return;
            }

            _sceneManager.Render();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (DesignMode)
                return;

            if (_sceneManager == null)
                return;

            var area = new Vector(ClientSize.Width, ClientSize.Height);
            _sceneManager.SetCameraDimensions(area);
        }

        protected override void OnPaintBackground(PaintEventArgs args)
        {
        }

        private void RenderArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragInProgress)
                return;

            var deltaX = (e.X - _prevDragPosition.X) * -1;
            var deltaY = (e.Y - _prevDragPosition.Y) * -1;

            _sceneManager.MoveCameraBy(new Vector(deltaX, deltaY));
            _prevDragPosition = e.Location;
        }

        private void RenderArea_MouseDown(object sender, MouseEventArgs e)
        {
            _dragInProgress = true;
            _prevDragPosition = e.Location;
        }

        private void RenderArea_MouseUp(object sender, MouseEventArgs e)
        {
            _dragInProgress = false;
        }
    }
}
