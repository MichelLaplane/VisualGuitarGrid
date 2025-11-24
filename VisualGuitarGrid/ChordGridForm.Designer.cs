using System;
using System.Drawing;
using System.Windows.Forms;

namespace VisualGuitarGrid
{
  partial class ChordGridForm
  {
    private System.ComponentModel.IContainer components = null;
    private Panel panelChord;
    private Button btnExport;

    // new controls for templates and settings
    private ComboBox comboTemplate;
    private Button btnApplyTemplate;
    private Button btnApplyClose;
    private Button btnClose;
    private TextBox textTempo;
    private TextBox textTimeSignature;
    private TextBox textSectionLabel;
    private CheckBox chkRepeatLeft;
    private CheckBox chkRepeatRight;
    private Label lblTemplate;
    private Label lblTempo;
    private Label lblTimeSignature;
    private Label lblSection;

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
            panelChord = new Panel();
            btnExport = new Button();
            comboTemplate = new ComboBox();
            btnApplyTemplate = new Button();
            btnApplyClose = new Button();
            btnClose = new Button();
            textTempo = new TextBox();
            textTimeSignature = new TextBox();
            textSectionLabel = new TextBox();
            chkRepeatLeft = new CheckBox();
            chkRepeatRight = new CheckBox();
            lblTemplate = new Label();
            lblTempo = new Label();
            lblTimeSignature = new Label();
            lblSection = new Label();
            SuspendLayout();
            // 
            // panelChord
            // 
            panelChord.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelChord.Location = new Point(12, 12);
            panelChord.Name = "panelChord";
            panelChord.Size = new Size(972, 368);
            panelChord.TabIndex = 0;
            // 
            // btnExport
            // 
            btnExport.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnExport.Location = new Point(864, 498);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(120, 34);
            btnExport.TabIndex = 1;
            btnExport.Text = "Export PNG";
            // 
            // comboTemplate
            // 
            comboTemplate.Location = new Point(12, 448);
            comboTemplate.Name = "comboTemplate";
            comboTemplate.Size = new Size(180, 33);
            comboTemplate.TabIndex = 3;
            // 
            // btnApplyTemplate
            // 
            btnApplyTemplate.Location = new Point(198, 448);
            btnApplyTemplate.Name = "btnApplyTemplate";
            btnApplyTemplate.Size = new Size(96, 34);
            btnApplyTemplate.TabIndex = 4;
            btnApplyTemplate.Text = "Apply";
            // 
            // btnApplyClose
            // 
            btnApplyClose.Location = new Point(357, 498);
            btnApplyClose.Name = "btnApplyClose";
            btnApplyClose.Size = new Size(156, 34);
            btnApplyClose.TabIndex = 13;
            btnApplyClose.Text = "Apply && Close";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(519, 498);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(120, 34);
            btnClose.TabIndex = 14;
            btnClose.Text = "Close";
            // 
            // textTempo
            // 
            textTempo.Location = new Point(300, 450);
            textTempo.Name = "textTempo";
            textTempo.Size = new Size(100, 31);
            textTempo.TabIndex = 6;
            // 
            // textTimeSignature
            // 
            textTimeSignature.Location = new Point(406, 448);
            textTimeSignature.Name = "textTimeSignature";
            textTimeSignature.Size = new Size(60, 31);
            textTimeSignature.TabIndex = 8;
            // 
            // textSectionLabel
            // 
            textSectionLabel.Location = new Point(472, 448);
            textSectionLabel.Name = "textSectionLabel";
            textSectionLabel.Size = new Size(85, 31);
            textSectionLabel.TabIndex = 10;
            // 
            // chkRepeatLeft
            // 
            chkRepeatLeft.Location = new Point(598, 450);
            chkRepeatLeft.Name = "chkRepeatLeft";
            chkRepeatLeft.Size = new Size(143, 29);
            chkRepeatLeft.TabIndex = 11;
            chkRepeatLeft.Text = "Repeat Left";
            // 
            // chkRepeatRight
            // 
            chkRepeatRight.Location = new Point(759, 450);
            chkRepeatRight.Name = "chkRepeatRight";
            chkRepeatRight.Size = new Size(151, 29);
            chkRepeatRight.TabIndex = 12;
            chkRepeatRight.Text = "Repeat Right";
            // 
            // lblTemplate
            // 
            lblTemplate.Location = new Point(12, 420);
            lblTemplate.Name = "lblTemplate";
            lblTemplate.Size = new Size(120, 25);
            lblTemplate.TabIndex = 2;
            lblTemplate.Text = "Template";
            // 
            // lblTempo
            // 
            lblTempo.Location = new Point(300, 420);
            lblTempo.Name = "lblTempo";
            lblTempo.Size = new Size(100, 25);
            lblTempo.TabIndex = 5;
            lblTempo.Text = "Tempo";
            // 
            // lblTimeSignature
            // 
            lblTimeSignature.Location = new Point(406, 420);
            lblTimeSignature.Name = "lblTimeSignature";
            lblTimeSignature.Size = new Size(60, 25);
            lblTimeSignature.TabIndex = 7;
            lblTimeSignature.Text = "Time signature";
            // 
            // lblSection
            // 
            lblSection.Location = new Point(472, 420);
            lblSection.Name = "lblSection";
            lblSection.Size = new Size(120, 25);
            lblSection.TabIndex = 9;
            lblSection.Text = "Section label";
            // 
            // ChordGridForm
            // 
            ClientSize = new Size(996, 561);
            Controls.Add(panelChord);
            Controls.Add(btnExport);
            Controls.Add(lblTemplate);
            Controls.Add(comboTemplate);
            Controls.Add(btnApplyTemplate);
            Controls.Add(lblTempo);
            Controls.Add(textTempo);
            Controls.Add(lblTimeSignature);
            Controls.Add(textTimeSignature);
            Controls.Add(lblSection);
            Controls.Add(textSectionLabel);
            Controls.Add(chkRepeatLeft);
            Controls.Add(chkRepeatRight);
            Controls.Add(btnApplyClose);
            Controls.Add(btnClose);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ChordGridForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chord Grid Editor";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
