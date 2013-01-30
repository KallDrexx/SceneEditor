﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SceneEditor.XnaRendering;

namespace SceneEditor.Windows.Forms
{
    public class RenderForm : Form
    {
        private XnaRenderer _renderer;

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
            }

            base.OnHandleCreated(e);

            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _renderer.RenderScene(null);
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
