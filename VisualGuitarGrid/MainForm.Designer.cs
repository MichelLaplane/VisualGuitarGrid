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
            panelGrid = new Panel();
            numericStrings = new NumericUpDown();
            numericFrets = new NumericUpDown();
            btnUpdate = new Button();
            btnExport = new Button();
            btnClear = new Button();
            textTuning = new TextBox();
            label1 = new Label();
            label2 = new Label();
            numericFinger = new NumericUpDown();
            label3 = new Label();
            textChordName = new TextBox();
            btnChordGrid = new Button();
            ((System.ComponentModel.ISupportInitialize)numericStrings).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericFrets).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericFinger).BeginInit();
            SuspendLayout();
            // 
            // panelGrid
            // 
            panelGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelGrid.BackColor = Color.White;
            panelGrid.Location = new Point(12, 52);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1090, 384);
            panelGrid.TabIndex = 0;
            // 
            // numericStrings
            // 
            numericStrings.Location = new Point(12, 12);
            numericStrings.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numericStrings.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericStrings.Name = "numericStrings";
            numericStrings.Size = new Size(50, 31);
            numericStrings.TabIndex = 1;
            numericStrings.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // numericFrets
            // 
            numericFrets.Location = new Point(140, 12);
            numericFrets.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numericFrets.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericFrets.Name = "numericFrets";
            numericFrets.Size = new Size(50, 31);
            numericFrets.TabIndex = 2;
            numericFrets.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(200, 10);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(80, 33);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.Location = new Point(915, 10);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(105, 36);
            btnExport.TabIndex = 4;
            btnExport.Text = "Export PNG";
            btnExport.Click += btnExport_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.Location = new Point(1026, 10);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(76, 36);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            btnClear.Click += btnClear_Click;
            // 
            // textTuning
            // 
            textTuning.Location = new Point(320, 12);
            textTuning.Name = "textTuning";
            textTuning.Size = new Size(180, 31);
            textTuning.TabIndex = 6;
            textTuning.Text = "E A D G B E";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(68, 16);
            label1.Name = "label1";
            label1.Size = new Size(66, 25);
            label1.TabIndex = 7;
            label1.Text = "Strings";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(196, 16);
            label2.Name = "label2";
            label2.Size = new Size(50, 25);
            label2.TabIndex = 8;
            label2.Text = "Frets";
            // 
            // numericFinger
            // 
            numericFinger.Location = new Point(508, 12);
            numericFinger.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            numericFinger.Name = "numericFinger";
            numericFinger.Size = new Size(40, 31);
            numericFinger.TabIndex = 9;
            numericFinger.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(558, 16);
            label3.Name = "label3";
            label3.Size = new Size(289, 25);
            label3.TabIndex = 10;
            label3.Text = "(Shift+Click open, Right-click mute)";
            // 
            // textChordName
            // 
            textChordName.Location = new Point(12, 447);
            textChordName.Name = "textChordName";
            textChordName.PlaceholderText = "Chord name (optional)";
            textChordName.Size = new Size(300, 31);
            textChordName.TabIndex = 11;
            // 
            // btnChordGrid
            // 
            btnChordGrid.Location = new Point(508, 442);
            btnChordGrid.Name = "btnChordGrid";
            btnChordGrid.Size = new Size(220, 41);
            btnChordGrid.TabIndex = 12;
            btnChordGrid.Text = "Open Chord Grid";
            btnChordGrid.Click += btnChordGrid_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(1114, 490);
            Controls.Add(panelGrid);
            Controls.Add(numericStrings);
            Controls.Add(numericFrets);
            Controls.Add(btnUpdate);
            Controls.Add(btnExport);
            Controls.Add(btnClear);
            Controls.Add(textTuning);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(numericFinger);
            Controls.Add(label3);
            Controls.Add(textChordName);
            Controls.Add(btnChordGrid);
            Name = "MainForm";
            Text = "Visual Guitar Grid";
            ((System.ComponentModel.ISupportInitialize)numericStrings).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericFrets).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericFinger).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}