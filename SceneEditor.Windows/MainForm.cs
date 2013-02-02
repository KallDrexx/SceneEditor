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
            var render = new RenderForm();
            render.Show(dockPanel1);

            var propertyWin = new PropertyWindow();
            propertyWin.Show(dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
