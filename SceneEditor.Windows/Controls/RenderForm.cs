using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace SceneEditor.Windows.Controls
{
    public class RenderForm : DockContent
    {
        private MenuStrip menuStrip1;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renderArea1 = new SceneEditor.Windows.Controls.RenderArea();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(361, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // renderArea1
            // 
            this.renderArea1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderArea1.Location = new System.Drawing.Point(0, 24);
            this.renderArea1.Name = "renderArea1";
            this.renderArea1.Size = new System.Drawing.Size(361, 295);
            this.renderArea1.TabIndex = 0;
            this.renderArea1.Text = "renderArea1";
            // 
            // RenderForm
            // 
            this.ClientSize = new System.Drawing.Size(361, 319);
            this.ControlBox = false;
            this.Controls.Add(this.renderArea1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RenderForm";
            this.ShowIcon = false;
            this.Text = "Scene Display";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void undoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            renderArea1.UndoCommand();
        }

        private void redoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            renderArea1.RedoCommand();
        }
    }
}
