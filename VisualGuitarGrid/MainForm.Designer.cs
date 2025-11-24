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
            this.components = new System.ComponentModel.Container();
            this.panelGrid = new Panel();
            this.numericStrings = new NumericUpDown();
            this.numericFrets = new NumericUpDown();
            this.btnUpdate = new Button();
            this.btnExport = new Button();
            this.btnExportSvg = new Button();
            this.btnExportHiRes = new Button();
            this.btnClear = new Button();
            this.textTuning = new TextBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.numericFinger = new NumericUpDown();
            this.label3 = new Label();
            this.textChordName = new TextBox();
            this.btnChordGrid = new Button();
            this.chkReverseStrings = new CheckBox();
            this.numericBarreFret = new NumericUpDown();
            this.numericBarreStartString = new NumericUpDown();
            this.numericBarreEndString = new NumericUpDown();
            this.btnApplyBarre = new Button();
            this.btnSavePreset = new Button();
            this.btnLoadPreset = new Button();
            this.btnPresetLibrary = new Button();

            this.textTempo = new TextBox();
            this.textTimeSignature = new TextBox();
            this.textSectionLabel = new TextBox();
            this.chkRepeatLeft = new CheckBox();
            this.chkRepeatRight = new CheckBox();

            ((System.ComponentModel.ISupportInitialize)(this.numericStrings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrets)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFinger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBarreFret)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBarreStartString)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBarreEndString)).BeginInit();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.Anchor = ((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right;
            this.panelGrid.BackColor = System.Drawing.Color.White;
            this.panelGrid.Location = new System.Drawing.Point(12, 98);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(960, 440);
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
            this.btnExport.Location = new System.Drawing.Point(880, 12);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(92, 26);
            this.btnExport.Text = "Export PNG";
            this.btnExport.Click += new EventHandler(this.btnExport_Click);
            // 
            // btnExportSvg
            // 
            this.btnExportSvg.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnExportSvg.Location = new System.Drawing.Point(776, 12);
            this.btnExportSvg.Name = "btnExportSvg";
            this.btnExportSvg.Size = new System.Drawing.Size(92, 26);
            this.btnExportSvg.Text = "Export SVG";
            this.btnExportSvg.Click += new EventHandler(this.btnExportSvg_Click);
            // 
            // btnExportHiRes
            // 
            this.btnExportHiRes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnExportHiRes.Location = new System.Drawing.Point(672, 12);
            this.btnExportHiRes.Name = "btnExportHiRes";
            this.btnExportHiRes.Size = new System.Drawing.Size(92, 26);
            this.btnExportHiRes.Text = "Export HiRes";
            this.btnExportHiRes.Click += new EventHandler(this.btnExportHiRes_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.btnClear.Location = new System.Drawing.Point(984, 12);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(76, 26);
            this.btnClear.Text = "Clear";
            this.btnClear.Click += new EventHandler(this.btnClear_Click);
            // 
            // textTuning
            // 
            this.textTuning.Location = new System.Drawing.Point(320, 12);
            this.textTuning.Name = "textTuning";
            this.textTuning.Size = new System.Drawing.Size(220, 23);
            this.textTuning.Text = "E2 A2 D3 G3 B3 E4";
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
            this.label3.Text = "(Shift+Click open, Ctrl+Click mute)";
            // 
            // textTempo
            // 
            this.textTempo.Location = new System.Drawing.Point(12, 46);
            this.textTempo.Name = "textTempo";
            this.textTempo.Size = new System.Drawing.Size(120, 23);
            this.textTempo.Text = "120 bpm";
            // 
            // textTimeSignature
            // 
            this.textTimeSignature.Location = new System.Drawing.Point(140, 46);
            this.textTimeSignature.Name = "textTimeSignature";
            this.textTimeSignature.Size = new System.Drawing.Size(80, 23);
            this.textTimeSignature.Text = "4/4";
            // 
            // textSectionLabel
            // 
            this.textSectionLabel.Location = new System.Drawing.Point(232, 46);
            this.textSectionLabel.Name = "textSectionLabel";
            this.textSectionLabel.Size = new System.Drawing.Size(60, 23);
            this.textSectionLabel.Text = "A";
            // 
            // chkRepeatLeft
            // 
            this.chkRepeatLeft.Location = new System.Drawing.Point(320, 46);
            this.chkRepeatLeft.Name = "chkRepeatLeft";
            this.chkRepeatLeft.Size = new System.Drawing.Size(100, 23);
            this.chkRepeatLeft.Text = "Repeat Left";
            this.chkRepeatLeft.Checked = true;
            // 
            // chkRepeatRight
            // 
            this.chkRepeatRight.Location = new System.Drawing.Point(428, 46);
            this.chkRepeatRight.Name = "chkRepeatRight";
            this.chkRepeatRight.Size = new System.Drawing.Size(100, 23);
            this.chkRepeatRight.Text = "Repeat Right";
            this.chkRepeatRight.Checked = true;
            // 
            // textChordName
            // 
            this.textChordName.Location = new System.Drawing.Point(12, 548);
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
            // chkReverseStrings
            // 
            this.chkReverseStrings.Location = new System.Drawing.Point(552, 40);
            this.chkReverseStrings.Name = "chkReverseStrings";
            this.chkReverseStrings.Size = new System.Drawing.Size(150, 22);
            this.chkReverseStrings.Text = "Reverse string order";
            this.chkReverseStrings.CheckedChanged += new EventHandler(this.chkReverseStrings_CheckedChanged);
            // 
            // numericBarreFret
            // 
            this.numericBarreFret.Location = new System.Drawing.Point(12, 520);
            this.numericBarreFret.Minimum = 0;
            this.numericBarreFret.Maximum = 24;
            this.numericBarreFret.Value = 1;
            this.numericBarreFret.Name = "numericBarreFret";
            this.numericBarreFret.Size = new System.Drawing.Size(60, 23);
            // 
            // numericBarreStartString
            // 
            this.numericBarreStartString.Location = new System.Drawing.Point(80, 520);
            this.numericBarreStartString.Minimum = 1;
            this.numericBarreStartString.Maximum = 12;
            this.numericBarreStartString.Value = 1;
            this.numericBarreStartString.Name = "numericBarreStartString";
            this.numericBarreStartString.Size = new System.Drawing.Size(60, 23);
            // 
            // numericBarreEndString
            // 
            this.numericBarreEndString.Location = new System.Drawing.Point(148, 520);
            this.numericBarreEndString.Minimum = 1;
            this.numericBarreEndString.Maximum = 12;
            this.numericBarreEndString.Value = 6;
            this.numericBarreEndString.Name = "numericBarreEndString";
            this.numericBarreEndString.Size = new System.Drawing.Size(60, 23);
            // 
            // btnApplyBarre
            // 
            this.btnApplyBarre.Location = new System.Drawing.Point(216, 516);
            this.btnApplyBarre.Name = "btnApplyBarre";
            this.btnApplyBarre.Size = new System.Drawing.Size(96, 28);
            this.btnApplyBarre.Text = "Apply Barre";
            this.btnApplyBarre.Click += new EventHandler(this.btnApplyBarre_Click);
            // 
            // btnSavePreset
            // 
            this.btnSavePreset.Location = new System.Drawing.Point(320, 516);
            this.btnSavePreset.Name = "btnSavePreset";
            this.btnSavePreset.Size = new System.Drawing.Size(96, 28);
            this.btnSavePreset.Text = "Save Preset";
            this.btnSavePreset.Click += new EventHandler(this.btnSavePreset_Click);
            // 
            // btnLoadPreset
            // 
            this.btnLoadPreset.Location = new System.Drawing.Point(424, 516);
            this.btnLoadPreset.Name = "btnLoadPreset";
            this.btnLoadPreset.Size = new System.Drawing.Size(96, 28);
            this.btnLoadPreset.Text = "Load Preset";
            this.btnLoadPreset.Click += new EventHandler(this.btnLoadPreset_Click);
            // 
            // btnPresetLibrary
            // 
            this.btnPresetLibrary.Location = new System.Drawing.Point(528, 516);
            this.btnPresetLibrary.Name = "btnPresetLibrary";
            this.btnPresetLibrary.Size = new System.Drawing.Size(120, 28);
            this.btnPresetLibrary.Text = "Preset Library...";
            this.btnPresetLibrary.Click += new EventHandler(this.btnPresetLibrary_Click);

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1064, 581);
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.numericStrings);
            this.Controls.Add(this.numericFrets);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnExportSvg);
            this.Controls.Add(this.btnExportHiRes);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.textTuning);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericFinger);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textTempo);
            this.Controls.Add(this.textTimeSignature);
            this.Controls.Add(this.textSectionLabel);
            this.Controls.Add(this.chkRepeatLeft);
            this.Controls.Add(this.chkRepeatRight);
            this.Controls.Add(this.textChordName);
            this.Controls.Add(this.btnChordGrid);
            this.Controls.Add(this.chkReverseStrings);
            this.Controls.Add(this.numericBarreFret);
            this.Controls.Add(this.numericBarreStartString);
            this.Controls.Add(this.numericBarreEndString);
            this.Controls.Add(this.btnApplyBarre);
            this.Controls.Add(this.btnSavePreset);
            this.Controls.Add(this.btnLoadPreset);
            this.Controls.Add(this.btnPresetLibrary);
            this.Name = "MainForm";
            this.Text = "Visual Guitar Grid";

            ((System.ComponentModel.ISupportInitialize)(this.numericStrings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFrets)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericFinger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBarreFret)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBarreStartString)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericBarreEndString)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}