using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VisualGuitarGrid.Preset;

namespace VisualGuitarGrid
{
    public partial class PresetLibraryForm : Form
    {
        private List<ChordShape> presets;
        public ChordShape SelectedPreset { get; private set; }

        public PresetLibraryForm(IEnumerable<ChordShape> presets)
        {
            InitializeComponent();
            this.presets = presets?.ToList() ?? new List<ChordShape>();
            PopulateList();
            lstPresets.DoubleClick += (s, e) => ApplySelected();
            btnApply.Click += (s, e) => ApplySelected();
            btnCancel.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
        }

        private void PopulateList()
        {
            lstPresets.Items.Clear();
            foreach (var p in presets)
            {
                lstPresets.Items.Add(p.Name ?? "(unnamed)");
            }
            if (lstPresets.Items.Count > 0)
                lstPresets.SelectedIndex = 0;
            UpdatePreview();
            lstPresets.SelectedIndexChanged += (s, e) => UpdatePreview();
        }

        private void UpdatePreview()
        {
            int idx = lstPresets.SelectedIndex;
            if (idx < 0 || idx >= presets.Count)
            {
                txtPreview.Text = "";
                return;
            }
            var p = presets[idx];
            txtPreview.Text = $"Name: {p.Name}\r\nTuning: {p.Tuning}\r\nFrets: {string.Join(",", p.StringFrets)}\r\nFingers: {string.Join(",", p.Fingers)}";
        }

        private void ApplySelected()
        {
            int idx = lstPresets.SelectedIndex;
            if (idx < 0 || idx >= presets.Count) return;
            SelectedPreset = presets[idx];
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}