using System;
using System.Windows.Forms;
using SceneEditor.Windows.Controls;

namespace SceneEditor.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var render = new RenderForm { MdiParent = this };
            render.Show();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
