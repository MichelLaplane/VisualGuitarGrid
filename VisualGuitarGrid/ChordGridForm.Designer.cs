using System;
using System.Windows.Forms;
namespace VisualGuitarGrid
{
    partial class ChordGridForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelChord;
        private Button btnExport;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelChord = new Panel();
            this.btnExport = new Button();
            this.SuspendLayout();
            // 
            // panelChord
            // 
            this.panelChord.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.panelChord.Location = new System.Drawing.Point(12, 12);
            this.panelChord.Name = "panelChord";
            this.panelChord.Size = new System.Drawing.Size(760, 400);
            this.panelChord.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.btnExport.Location = new System.Drawing.Point(652, 420);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 26);
            this.btnExport.Text = "Export PNG";
            // 
            // ChordGridForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.panelChord);
            this.Controls.Add(this.btnExport);
            this.Name = "ChordGridForm";
            this.Text = "Chord Grid Editor";
            this.ResumeLayout(false);
        }
    }
}