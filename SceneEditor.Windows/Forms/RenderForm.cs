using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SceneEditor.Core.General;
using SceneEditor.Core.Rendering;
using SceneEditor.Core.SceneManagement;
using SceneEditor.XnaRendering;

namespace SceneEditor.Windows.Forms
{
    public class RenderForm : Form
    {
        private IRenderer _renderer;
        private Stopwatch _timer;
        private readonly SceneManager _sceneManager;
        private bool _dragInProgress;
        private Point _prevDragPosition;

        public RenderForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            _sceneManager = new SceneManager();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                var pb = new PictureBox
                {
                    Parent = this,
                    Dock = DockStyle.Fill
                };

                _renderer = new XnaRenderer(pb.Handle, ClientSize.Width, ClientSize.Height);
                Application.Idle += delegate { Invalidate(); };
                _timer = Stopwatch.StartNew();

                var area = new Vector(ClientSize.Width, ClientSize.Height);
                _sceneManager.SetCameraDimensions(area);

                pb.MouseMove += RenderForm_MouseMove;
                pb.MouseDown += RenderForm_MouseDown;
                pb.MouseUp += RenderForm_MouseUp;
            }

            base.OnHandleCreated(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var snapshot = new SceneSnapshot
            {
                CameraPosition = _sceneManager.CameraPosition,
                RenderAreaDimensions = _sceneManager.CameraDimensions
            };

            _renderer.RenderScene(snapshot);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            var area = new Vector(ClientSize.Width, ClientSize.Height);
            _sceneManager.SetCameraDimensions(area);
        }

        protected override void OnPaintBackground(PaintEventArgs args)
        {
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // RenderForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.DoubleBuffered = true;
            this.Name = "RenderForm";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RenderForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.RenderForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RenderForm_MouseUp);
            this.ResumeLayout(false);

        }

        private void RenderForm_MouseDown(object sender, MouseEventArgs e)
        {
            _dragInProgress = true;
            _prevDragPosition = e.Location;
        }

        private void RenderForm_MouseUp(object sender, MouseEventArgs e)
        {
            _dragInProgress = false;
        }

        private void RenderForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_dragInProgress)
                return;

            var deltaX = (e.X - _prevDragPosition.X) * -1;
            var deltaY = (e.Y - _prevDragPosition.Y) * -1;

            _sceneManager.MoveCameraBy(new Vector(deltaX, deltaY));
            _prevDragPosition = e.Location;
        }
    }
}
