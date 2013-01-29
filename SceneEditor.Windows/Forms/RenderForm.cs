using System;
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

        protected override void OnCreateControl()
        {
            if (!DesignMode)
            {
               // 
            }

            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_renderer == null)
                _renderer = new XnaRenderer(Handle, ClientSize.Width, ClientSize.Height);

            _renderer.RenderScene(null);
        }
    }
}
