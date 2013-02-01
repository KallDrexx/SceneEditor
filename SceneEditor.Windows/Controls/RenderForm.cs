using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using SceneEditor.Core.General;
using SceneEditor.Core.Rendering;
using SceneEditor.Core.SceneManagement;
using SceneEditor.XnaRendering;

namespace SceneEditor.Windows.Controls
{
    public class RenderForm : Form
    {
        public RenderForm()
        {
            var renderArea = new RenderArea {Dock = DockStyle.Fill};
            Controls.Add(renderArea);
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
