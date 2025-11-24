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
    private Button btnExportSvg;
    private Button btnExportHiRes;
    private Button btnClear;
    private TextBox textTuning;
    private Label label1;
    private Label label2;
    private NumericUpDown numericFinger;
    private Label label3;
    private TextBox textChordName;
    private Button btnChordGrid;
    private CheckBox chkReverseStrings;
    private NumericUpDown numericBarreFret;
    private NumericUpDown numericBarreStartString;
    private NumericUpDown numericBarreEndString;
    private Button btnApplyBarre;
    private Button btnSavePreset;
    private Button btnLoadPreset;
    private Button btnPresetLibrary;

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
            btnExportSvg = new Button();
            btnExportHiRes = new Button();
            btnClear = new Button();
            textTuning = new TextBox();
            label1 = new Label();
            label2 = new Label();
            numericFinger = new NumericUpDown();
            label3 = new Label();
            textChordName = new TextBox();
            btnChordGrid = new Button();
            chkReverseStrings = new CheckBox();
            numericBarreFret = new NumericUpDown();
            numericBarreStartString = new NumericUpDown();
            numericBarreEndString = new NumericUpDown();
            btnApplyBarre = new Button();
            btnSavePreset = new Button();
            btnLoadPreset = new Button();
            btnPresetLibrary = new Button();
            ((System.ComponentModel.ISupportInitialize)numericStrings).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericFrets).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericFinger).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericBarreFret).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericBarreStartString).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericBarreEndString).BeginInit();
            SuspendLayout();
            // 
            // panelGrid
            // 
            panelGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelGrid.BackColor = Color.White;
            panelGrid.Location = new Point(12, 58);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1278, 432);
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
            btnUpdate.Size = new Size(80, 34);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.Location = new Point(1113, 513);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(133, 34);
            btnExport.TabIndex = 4;
            btnExport.Text = "Export PNG";
            btnExport.Click += btnExport_Click;
            // 
            // btnExportSvg
            // 
            btnExportSvg.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportSvg.Location = new Point(982, 513);
            btnExportSvg.Name = "btnExportSvg";
            btnExportSvg.Size = new Size(125, 34);
            btnExportSvg.TabIndex = 5;
            btnExportSvg.Text = "Export SVG";
            btnExportSvg.Click += btnExportSvg_Click;
            // 
            // btnExportHiRes
            // 
            btnExportHiRes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportHiRes.Location = new Point(838, 513);
            btnExportHiRes.Name = "btnExportHiRes";
            btnExportHiRes.Size = new Size(138, 34);
            btnExportHiRes.TabIndex = 6;
            btnExportHiRes.Text = "Export HiRes";
            btnExportHiRes.Click += btnExportHiRes_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.Location = new Point(1214, 12);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(76, 34);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.Click += btnClear_Click;
            // 
            // textTuning
            // 
            textTuning.Location = new Point(320, 12);
            textTuning.Name = "textTuning";
            textTuning.Size = new Size(220, 31);
            textTuning.TabIndex = 8;
            textTuning.Text = "E2 A2 D3 G3 B3 E4";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(68, 16);
            label1.Name = "label1";
            label1.Size = new Size(66, 25);
            label1.TabIndex = 9;
            label1.Text = "Strings";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(196, 16);
            label2.Name = "label2";
            label2.Size = new Size(50, 25);
            label2.TabIndex = 10;
            label2.Text = "Frets";
            // 
            // numericFinger
            // 
            numericFinger.Location = new Point(508, 12);
            numericFinger.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            numericFinger.Name = "numericFinger";
            numericFinger.Size = new Size(40, 31);
            numericFinger.TabIndex = 11;
            numericFinger.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(721, 16);
            label3.Name = "label3";
            label3.Size = new Size(282, 25);
            label3.TabIndex = 12;
            label3.Text = "(Shift+Click open, Ctrl+Click mute)";
            // 
            // textChordName
            // 
            textChordName.Location = new Point(12, 557);
            textChordName.Name = "textChordName";
            textChordName.PlaceholderText = "Chord name (optional)";
            textChordName.Size = new Size(300, 31);
            textChordName.TabIndex = 13;
            // 
            // btnChordGrid
            // 
            btnChordGrid.Location = new Point(1025, 11);
            btnChordGrid.Name = "btnChordGrid";
            btnChordGrid.Size = new Size(183, 34);
            btnChordGrid.TabIndex = 14;
            btnChordGrid.Text = "Open Chord Grid";
            btnChordGrid.Click += btnChordGrid_Click;
            // 
            // chkReverseStrings
            // 
            chkReverseStrings.Location = new Point(565, 13);
            chkReverseStrings.Name = "chkReverseStrings";
            chkReverseStrings.Size = new Size(150, 34);
            chkReverseStrings.TabIndex = 15;
            chkReverseStrings.Text = "Reverse string order";
            chkReverseStrings.CheckedChanged += chkReverseStrings_CheckedChanged;
            // 
            // numericBarreFret
            // 
            numericBarreFret.Location = new Point(12, 520);
            numericBarreFret.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numericBarreFret.Name = "numericBarreFret";
            numericBarreFret.Size = new Size(60, 31);
            numericBarreFret.TabIndex = 16;
            numericBarreFret.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericBarreStartString
            // 
            numericBarreStartString.Location = new Point(80, 520);
            numericBarreStartString.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numericBarreStartString.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericBarreStartString.Name = "numericBarreStartString";
            numericBarreStartString.Size = new Size(60, 31);
            numericBarreStartString.TabIndex = 17;
            numericBarreStartString.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericBarreEndString
            // 
            numericBarreEndString.Location = new Point(148, 520);
            numericBarreEndString.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numericBarreEndString.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericBarreEndString.Name = "numericBarreEndString";
            numericBarreEndString.Size = new Size(60, 31);
            numericBarreEndString.TabIndex = 18;
            numericBarreEndString.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // btnApplyBarre
            // 
            btnApplyBarre.Location = new Point(216, 516);
            btnApplyBarre.Name = "btnApplyBarre";
            btnApplyBarre.Size = new Size(130, 34);
            btnApplyBarre.TabIndex = 19;
            btnApplyBarre.Text = "Apply Barre";
            btnApplyBarre.Click += btnApplyBarre_Click;
            // 
            // btnSavePreset
            // 
            btnSavePreset.Location = new Point(352, 513);
            btnSavePreset.Name = "btnSavePreset";
            btnSavePreset.Size = new Size(140, 34);
            btnSavePreset.TabIndex = 20;
            btnSavePreset.Text = "Save Preset";
            btnSavePreset.Click += btnSavePreset_Click;
            // 
            // btnLoadPreset
            // 
            btnLoadPreset.Location = new Point(498, 513);
            btnLoadPreset.Name = "btnLoadPreset";
            btnLoadPreset.Size = new Size(128, 34);
            btnLoadPreset.TabIndex = 21;
            btnLoadPreset.Text = "Load Preset";
            btnLoadPreset.Click += btnLoadPreset_Click;
            // 
            // btnPresetLibrary
            // 
            btnPresetLibrary.Location = new Point(632, 513);
            btnPresetLibrary.Name = "btnPresetLibrary";
            btnPresetLibrary.Size = new Size(157, 34);
            btnPresetLibrary.TabIndex = 22;
            btnPresetLibrary.Text = "Preset Library...";
            btnPresetLibrary.Click += btnPresetLibrary_Click;
            // 
            // MainForm
            // 
            ClientSize = new Size(1382, 624);
            Controls.Add(panelGrid);
            Controls.Add(numericStrings);
            Controls.Add(numericFrets);
            Controls.Add(btnUpdate);
            Controls.Add(btnExport);
            Controls.Add(btnExportSvg);
            Controls.Add(btnExportHiRes);
            Controls.Add(btnClear);
            Controls.Add(textTuning);
            Controls.Add(label1);
            Controls.Add(label2);
            Controls.Add(numericFinger);
            Controls.Add(label3);
            Controls.Add(textChordName);
            Controls.Add(btnChordGrid);
            Controls.Add(chkReverseStrings);
            Controls.Add(numericBarreFret);
            Controls.Add(numericBarreStartString);
            Controls.Add(numericBarreEndString);
            Controls.Add(btnApplyBarre);
            Controls.Add(btnSavePreset);
            Controls.Add(btnLoadPreset);
            Controls.Add(btnPresetLibrary);
            Name = "MainForm";
            Text = "Visual Guitar Grid";
            ((System.ComponentModel.ISupportInitialize)numericStrings).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericFrets).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericFinger).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericBarreFret).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericBarreStartString).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericBarreEndString).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}