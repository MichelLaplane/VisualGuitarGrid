using System;
using System.Windows.Forms;

namespace VisualGuitarGrid
{
    partial class PresetLibraryForm
    {
        private System.ComponentModel.IContainer components = null;
        private ListBox lstPresets;
        private Button btnApply;
        private Button btnCancel;
        private TextBox txtPreview;

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
            this.lstPresets = new ListBox();
            this.btnApply = new Button();
            this.btnCancel = new Button();
            this.txtPreview = new TextBox();
            this.SuspendLayout();
            // 
            // lstPresets
            // 
            this.lstPresets.Location = new System.Drawing.Point(12, 12);
            this.lstPresets.Size = new System.Drawing.Size(200, 300);
            this.lstPresets.Name = "lstPresets";
            // 
            // txtPreview
            // 
            this.txtPreview.Location = new System.Drawing.Point(220, 12);
            this.txtPreview.Size = new System.Drawing.Size(300, 300);
            this.txtPreview.Multiline = true;
            this.txtPreview.ReadOnly = true;
            this.txtPreview.Name = "txtPreview";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(364, 320);
            this.btnApply.Size = new System.Drawing.Size(75, 28);
            this.btnApply.Name = "btnApply";
            this.btnApply.Text = "Apply";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(445, 320);
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Text = "Cancel";
            // 
            // PresetLibraryForm
            // 
            this.ClientSize = new System.Drawing.Size(534, 360);
            this.Controls.Add(this.lstPresets);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCancel);
            this.Name = "PresetLibraryForm";
            this.Text = "Preset Library";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}