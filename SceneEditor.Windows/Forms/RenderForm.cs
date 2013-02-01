using System;
using System.Diagnostics;
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
        private SceneManager _sceneManager;

        public RenderForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            _sceneManager = new SceneManager();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (!DesignMode)
            {
                _renderer = new XnaRenderer(Handle, ClientSize.Width, ClientSize.Height);
                Application.Idle += delegate { Invalidate(); };
                _timer = Stopwatch.StartNew();
            }

            base.OnHandleCreated(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _sceneManager.MoveCameraBy(new Vector((float)_timer.Elapsed.TotalSeconds / -20, (float)_timer.Elapsed.TotalSeconds/ -20));

            var snapshot = new SceneSnapshot
            {
                CameraPosition = new Vector(_sceneManager.CameraPosition.X, _sceneManager.CameraPosition.Y)
            };

            _renderer.RenderScene(snapshot);
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
            this.ResumeLayout(false);

        }
    }
}
