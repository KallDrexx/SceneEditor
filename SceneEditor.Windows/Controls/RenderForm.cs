using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SceneEditor.Windows.Controls
{
    public class RenderForm : DockContent
    {
        private RenderArea renderArea1;
    
        public RenderForm()
        {
            InitializeComponent();
        }

        protected override string GetPersistString()
        {
            return "test";
        }

        private void InitializeComponent()
        {
            this.renderArea1 = new SceneEditor.Windows.Controls.RenderArea();
            this.SuspendLayout();
            // 
            // renderArea1
            // 
            this.renderArea1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderArea1.Location = new System.Drawing.Point(0, 0);
            this.renderArea1.Name = "renderArea1";
            this.renderArea1.Size = new System.Drawing.Size(361, 319);
            this.renderArea1.TabIndex = 0;
            this.renderArea1.Text = "renderArea1";
            // 
            // RenderForm
            // 
            this.ClientSize = new System.Drawing.Size(361, 319);
            this.ControlBox = false;
            this.Controls.Add(this.renderArea1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "RenderForm";
            this.ShowIcon = false;
            this.Text = "Scene Display";
            this.ResumeLayout(false);

        }
    }
}
