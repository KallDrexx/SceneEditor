using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using SceneEditor.Core.Rendering;
using SceneEditor.XnaRendering;

namespace SceneEditor.Windows.Forms
{
    public class RenderForm : Form
    {
        private XnaRenderer _renderer;
        private Stopwatch _timer;

        public RenderForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
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
            var snapshot = new SceneSnapshot
            {
                CameraPosition = new Vector2((float)_timer.Elapsed.TotalSeconds * 5, (float)_timer.Elapsed.TotalSeconds * 5)
            };

            _renderer.RenderScene(snapshot);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            _renderer.ResetSize(ClientSize.Width, ClientSize.Height);
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
