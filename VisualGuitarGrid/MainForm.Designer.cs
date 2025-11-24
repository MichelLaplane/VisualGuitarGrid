using System;
using System.Windows.Forms;
namespace VisualGuitarGrid
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelGrid;
        private NumericUpDown numericStrings;
        private NumericUpDown numericFrets;
        private Button btnUpdate;
        private Button btnExport;
        private Button btnClear;
        private TextBox textTuning;
        private Label label1;
        private Label label2;
        private NumericUpDown numericFinger;
        private Label label3;
        private TextBox textChordName;
        private Button btnChordGrid;

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
            this.panelGrid = new Panel();
            this.numericStrings = new NumericUpDown();
            this.numericFrets = new NumericUpDown();
            this.btnUpdate = new Button();
            this.btnExport = new Button();
            this.btnClear = new Button();
            this.textTuning = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.numericFinger = new NumericUpDown();
            this.label3 = new Label();
            this.textChordName = new TextBox();
            this.btnChordGrid = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.numericStrings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFinger)).BeginInit();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;
            this.panelGrid.BackColor = System.Drawing.Color.White;
            this.panelGrid.Location = new System.Drawing.Point(12, 58);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(760, 380);
            this.panelGrid.TabIndex = 0;
            // 
            // numericStrings
            // 
            this.numericStrings.Location = new System.Drawing.Point(12, 12);
            this.numericStrings.Minimum = 1;
            this.numericStrings.Maximum = 12;
            this.numericStrings.Value = 6;
            this.numericStrings.Name = "numericStrings";
            this.numericStrings.Size = new System.Drawing.Size(50, 23);
            // 
            // numericFrets
            // 
            this.numericFrets.Location = new System.Drawing.Point(140, 12);
            this.numericFrets.Minimum = 1;
            this.numericFrets.Maximum = 24;
            this.numericFrets.Value = 12;
            this.numericFrets.Name = "numericFrets";
            this.numericFrets.Size = new System.Drawing.Size(50, 23);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(200, 10);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 26);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new EventHandler(this.btnUpdate_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnExport.Location = new System.Drawing.Point(620, 10);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(70, 26);
            this.btnExport.Text = "Export PNG";
            this.btnExport.Click += new EventHandler(this.btnExport_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClear.Location = new System.Drawing.Point(696, 10);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(76, 26);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            // 
            // textTuning
            // 
            this.textTuning.Location = new System.Drawing.Point(320, 12);
            this.textTuning.Name = "textTuning";
            this.textTuning.Size = new System.Drawing.Size(180, 23);
            this.textTuning.Text = "E A D G B E";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(68, 16);
            this.label1.Name = "label1";
            this.label1.Text = "Strings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(196, 16);
            this.label2.Name = "label2";
            this.label2.Text = "Frets";
            // 
            // numericFinger
            // 
            this.numericFinger.Location = new System.Drawing.Point(508, 12);
            this.numericFinger.Minimum = 0;
            this.numericFinger.Maximum = 4;
            this.numericFinger.Value = 1;
            this.numericFinger.Name = "numericFinger";
            this.numericFinger.Size = new System.Drawing.Size(40, 23);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(558, 16);
            this.label3.Name = "label3";
            this.label3.Text = "(Shift+Click open, Right-click mute)";
            // 
            // textChordName
            // 
            this.textChordName.Location = new System.Drawing.Point(12, 444);
            this.textChordName.Name = "textChordName";
            this.textChordName.Size = new System.Drawing.Size(300, 23);
            this.textChordName.PlaceholderText = "Chord name (optional)";
            // 
            // btnChordGrid
            // 
            this.btnChordGrid.Location = new System.Drawing.Point(288, 10);
            this.btnChordGrid.Name = "btnChordGrid";
            this.btnChordGrid.Size = new System.Drawing.Size(100, 26);
            this.btnChordGrid.Text = "Open Chord Grid";
            this.btnChordGrid.Click += new EventHandler(this.btnChordGrid_Click);

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 481);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.numericStrings);
            this.Controls.Add(this.numericFrets);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.textTuning);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericFinger);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textChordName);
            this.Controls.Add(this.btnChordGrid);
            this.Name = "MainForm";
            this.Text = "Visual Guitar Grid";

            ((System.ComponentModel.ISupportInitialize)(this.numericStrings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFinger)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}