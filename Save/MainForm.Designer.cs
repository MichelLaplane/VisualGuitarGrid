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
        private Button btnApplyTemplate;
        private System.Windows.Forms.ComboBox comboTemplate;

    // new controls
    private TextBox textTempo;
        private TextBox textTimeSignature;
        private TextBox textSectionLabel;
        private CheckBox chkRepeatLeft;
        private CheckBox chkRepeatRight;

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
            btnApplyTemplate = new Button();
            textTempo = new TextBox();
            textTimeSignature = new TextBox();
            textSectionLabel = new TextBox();
            chkRepeatLeft = new CheckBox();
            chkRepeatRight = new CheckBox();
            comboTemplate = new ComboBox();
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
            panelGrid.Location = new Point(12, 83);
            panelGrid.Name = "panelGrid";
            panelGrid.Size = new Size(1412, 475);
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
            btnUpdate.Size = new Size(92, 34);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Update";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExport.Location = new Point(1299, 568);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(125, 34);
            btnExport.TabIndex = 4;
            btnExport.Text = "Export PNG";
            btnExport.Click += btnExport_Click;
            // 
            // btnExportSvg
            // 
            btnExportSvg.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportSvg.Location = new Point(1160, 569);
            btnExportSvg.Name = "btnExportSvg";
            btnExportSvg.Size = new Size(133, 34);
            btnExportSvg.TabIndex = 5;
            btnExportSvg.Text = "Export SVG";
            btnExportSvg.Click += btnExportSvg_Click;
            // 
            // btnExportHiRes
            // 
            btnExportHiRes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnExportHiRes.Location = new Point(825, 608);
            btnExportHiRes.Name = "btnExportHiRes";
            btnExportHiRes.Size = new Size(143, 34);
            btnExportHiRes.TabIndex = 6;
            btnExportHiRes.Text = "Export HiRes";
            btnExportHiRes.Click += btnExportHiRes_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClear.Location = new Point(1329, 12);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(95, 34);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear";
            btnClear.Click += btnClear_Click;
            // 
            // textTuning
            // 
            textTuning.Location = new Point(344, 12);
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
            numericFinger.Location = new Point(298, 11);
            numericFinger.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            numericFinger.Name = "numericFinger";
            numericFinger.Size = new Size(40, 31);
            numericFinger.TabIndex = 11;
            numericFinger.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(892, 12);
            label3.Name = "label3";
            label3.Size = new Size(282, 25);
            label3.TabIndex = 12;
            label3.Text = "(Shift+Click open, Ctrl+Click mute)";
            // 
            // textChordName
            // 
            textChordName.Location = new Point(12, 605);
            textChordName.Name = "textChordName";
            textChordName.PlaceholderText = "Chord name (optional)";
            textChordName.Size = new Size(300, 31);
            textChordName.TabIndex = 19;
            // 
            // btnChordGrid
            // 
            btnChordGrid.Location = new Point(825, 568);
            btnChordGrid.Name = "btnChordGrid";
            btnChordGrid.Size = new Size(165, 34);
            btnChordGrid.TabIndex = 20;
            btnChordGrid.Text = "Open Chord Grid";
            btnChordGrid.Click += btnChordGrid_Click;
            // 
            // chkReverseStrings
            // 
            chkReverseStrings.Location = new Point(570, 12);
            chkReverseStrings.Name = "chkReverseStrings";
            chkReverseStrings.Size = new Size(150, 34);
            chkReverseStrings.TabIndex = 21;
            chkReverseStrings.Text = "Reverse string order";
            chkReverseStrings.CheckedChanged += chkReverseStrings_CheckedChanged;
            // 
            // numericBarreFret
            // 
            numericBarreFret.Location = new Point(12, 568);
            numericBarreFret.Maximum = new decimal(new int[] { 24, 0, 0, 0 });
            numericBarreFret.Name = "numericBarreFret";
            numericBarreFret.Size = new Size(60, 31);
            numericBarreFret.TabIndex = 22;
            numericBarreFret.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericBarreStartString
            // 
            numericBarreStartString.Location = new Point(80, 568);
            numericBarreStartString.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numericBarreStartString.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericBarreStartString.Name = "numericBarreStartString";
            numericBarreStartString.Size = new Size(60, 31);
            numericBarreStartString.TabIndex = 23;
            numericBarreStartString.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numericBarreEndString
            // 
            numericBarreEndString.Location = new Point(148, 568);
            numericBarreEndString.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numericBarreEndString.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericBarreEndString.Name = "numericBarreEndString";
            numericBarreEndString.Size = new Size(60, 31);
            numericBarreEndString.TabIndex = 24;
            numericBarreEndString.Value = new decimal(new int[] { 6, 0, 0, 0 });
            // 
            // btnApplyBarre
            // 
            btnApplyBarre.Location = new Point(216, 569);
            btnApplyBarre.Name = "btnApplyBarre";
            btnApplyBarre.Size = new Size(151, 34);
            btnApplyBarre.TabIndex = 25;
            btnApplyBarre.Text = "Apply Barre";
            btnApplyBarre.Click += btnApplyBarre_Click;
            // 
            // btnSavePreset
            // 
            btnSavePreset.Location = new Point(373, 569);
            btnSavePreset.Name = "btnSavePreset";
            btnSavePreset.Size = new Size(129, 34);
            btnSavePreset.TabIndex = 26;
            btnSavePreset.Text = "Save Preset";
            btnSavePreset.Click += btnSavePreset_Click;
            // 
            // btnLoadPreset
            // 
            btnLoadPreset.Location = new Point(508, 569);
            btnLoadPreset.Name = "btnLoadPreset";
            btnLoadPreset.Size = new Size(124, 34);
            btnLoadPreset.TabIndex = 27;
            btnLoadPreset.Text = "Load Preset";
            btnLoadPreset.Click += btnLoadPreset_Click;
            // 
            // btnPresetLibrary
            // 
            btnPresetLibrary.Location = new Point(638, 569);
            btnPresetLibrary.Name = "btnPresetLibrary";
            btnPresetLibrary.Size = new Size(172, 34);
            btnPresetLibrary.TabIndex = 28;
            btnPresetLibrary.Text = "Preset Library...";
            btnPresetLibrary.Click += btnPresetLibrary_Click;
            // 
            // btnApplyTemplate
            // 
            btnApplyTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnApplyTemplate.Location = new Point(1011, 569);
            btnApplyTemplate.Name = "btnApplyTemplate";
            btnApplyTemplate.Size = new Size(143, 34);
            btnApplyTemplate.TabIndex = 6;
            btnApplyTemplate.Text = "Apply Template";
            btnApplyTemplate.Click += btnApplyTemplate_Click;
            // 
            // textTempo
            // 
            textTempo.Location = new Point(12, 46);
            textTempo.Name = "textTempo";
            textTempo.Size = new Size(120, 31);
            textTempo.TabIndex = 13;
            textTempo.Text = "120 bpm";
            // 
            // textTimeSignature
            // 
            textTimeSignature.Location = new Point(140, 46);
            textTimeSignature.Name = "textTimeSignature";
            textTimeSignature.Size = new Size(80, 31);
            textTimeSignature.TabIndex = 14;
            textTimeSignature.Text = "4/4";
            // 
            // textSectionLabel
            // 
            textSectionLabel.Location = new Point(232, 46);
            textSectionLabel.Name = "textSectionLabel";
            textSectionLabel.Size = new Size(60, 31);
            textSectionLabel.TabIndex = 15;
            textSectionLabel.Text = "A";
            // 
            // chkRepeatLeft
            // 
            chkRepeatLeft.Checked = true;
            chkRepeatLeft.CheckState = CheckState.Checked;
            chkRepeatLeft.Location = new Point(320, 46);
            chkRepeatLeft.Name = "chkRepeatLeft";
            chkRepeatLeft.Size = new Size(129, 34);
            chkRepeatLeft.TabIndex = 16;
            chkRepeatLeft.Text = "Repeat Left";
            // 
            // chkRepeatRight
            // 
            chkRepeatRight.Checked = true;
            chkRepeatRight.CheckState = CheckState.Checked;
            chkRepeatRight.Location = new Point(455, 43);
            chkRepeatRight.Name = "chkRepeatRight";
            chkRepeatRight.Size = new Size(152, 34);
            chkRepeatRight.TabIndex = 17;
            chkRepeatRight.Text = "Repeat Right";
            // 
            // comboTemplate
            // 
            comboTemplate.Location = new Point(726, 9);
            comboTemplate.Name = "comboTemplate";
            comboTemplate.Size = new Size(160, 33);
            comboTemplate.TabIndex = 18;
            // 
            // MainForm
            // 
            ClientSize = new Size(1436, 647);
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
            Controls.Add(textTempo);
            Controls.Add(textTimeSignature);
            Controls.Add(textSectionLabel);
            Controls.Add(chkRepeatLeft);
            Controls.Add(chkRepeatRight);
            Controls.Add(comboTemplate);
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
            Controls.Add(btnApplyTemplate);
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