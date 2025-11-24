using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;

namespace VisualGuitarGrid
{
  public partial class MainForm : Form
  {
    private int stringsCount = 6;
    private int fretsCount = 12;
    private string[] tuning = new string[] { "E2", "A2", "D3", "G3", "B3", "E4" };

    // For each string, -1 = not played, -2 = muted, 0 = open, >=1 fret number
    private int[] stringFrets;
    private int[] stringFingers;

    // Barre and UI state
    private int? barreFretIndex = null;
    private int? barreStartStringIndex = null;
    private int? barreEndStringIndex = null;
    private bool reverseStringOrder = false;

    public MainForm()
    {
      InitializeComponent();
      InitState();
      panelGrid.Paint += PanelGrid_Paint;
      panelGrid.MouseClick += PanelGrid_MouseClick;
      panelGrid.MouseDown += panelGrid_MouseDown;
      panelGrid.MouseMove += panelGrid_MouseMove;
      panelGrid.MouseUp += panelGrid_MouseUp;
    }

    private void InitState()
    {
      stringsCount = (int)numericStrings.Value;
      fretsCount = (int)numericFrets.Value;
      stringFrets = Enumerable.Repeat(-1, stringsCount).ToArray();
      stringFingers = new int[stringsCount];
      ParseTuning();
      panelGrid.Invalidate();
    }

    private void ParseTuning()
    {
      var raw = textTuning.Text?.Trim();
      if (string.IsNullOrWhiteSpace(raw))
      {
        tuning = new string[] { "E2", "A2", "D3", "G3", "B3", "E4" };
      }
      else
      {
        var parts = raw.Split(new[] { ',', ' ', ';' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= stringsCount)
          tuning = parts.Take(stringsCount).ToArray();
        else
        {
          tuning = parts.Concat(Enumerable.Repeat("E2", Math.Max(0, stringsCount - parts.Length))).Take(stringsCount).ToArray();
        }
      }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
      stringsCount = (int)numericStrings.Value;
      fretsCount = (int)numericFrets.Value;
      InitState();
    }

    private void btnClear_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < stringFrets.Length; i++)
      {
        stringFrets[i] = -1;
        stringFingers[i] = 0;
      }
      panelGrid.Invalidate();
    }

    private void PanelGrid_MouseClick(object sender, MouseEventArgs e)
    {
      var layout = ComputeLayout();
      int s = HitTestString(e.Location, layout);
      int f = HitTestFret(e.Location, layout);
      if (s < 0 || f < 0) return;

      if (e.Button == MouseButtons.Right)
      {
        // Toggle muted
        stringFrets[s] = (stringFrets[s] == -2) ? -1 : -2;
        stringFingers[s] = 0;
      }
      else if (Control.ModifierKeys == Keys.Shift)
      {
        // Set open
        stringFrets[s] = 0;
        stringFingers[s] = 0;
      }
      else
      {
        // Set fret
        if (f == 0)
        {
          // clicking on nut area treat as open
          stringFrets[s] = 0;
          stringFingers[s] = 0;
        }
        else
        {
          if (stringFrets[s] == f)
          {
            // cycle: fret -> unassign
            stringFrets[s] = -1;
            stringFingers[s] = 0;
          }
          else
          {
            stringFrets[s] = f;
            stringFingers[s] = (int)numericFinger.Value;
          }
        }
      }
      panelGrid.Invalidate();
    }

    // Basic drag-to-place support
    private bool isDragging = false;
    private int dragStringIndex = -1;
    private int dragFretIndex = -1;

    private void panelGrid_MouseDown(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left) return;
      var layout = ComputeLayout();
      int s = HitTestString(e.Location, layout);
      int f = HitTestFret(e.Location, layout);
      if (s < 0 || f < 0) return;
      isDragging = true;
      dragStringIndex = s;
      dragFretIndex = f;
      ApplyNoteAt(s, f);
      panelGrid.Invalidate();
    }

    private void panelGrid_MouseMove(object sender, MouseEventArgs e)
    {
      if (!isDragging) return;
      var layout = ComputeLayout();
      int s = HitTestString(e.Location, layout);
      int f = HitTestFret(e.Location, layout);
      if (s < 0 || f < 0) return;
      if (s != dragStringIndex || f != dragFretIndex)
      {
        dragStringIndex = s;
        dragFretIndex = f;
        ApplyNoteAt(s, f);
        panelGrid.Invalidate();
      }
    }

    private void panelGrid_MouseUp(object sender, MouseEventArgs e)
    {
      isDragging = false;
      dragStringIndex = -1;
      dragFretIndex = -1;
    }

    private void ApplyNoteAt(int s, int f)
    {
      if (Control.ModifierKeys == Keys.Shift)
      {
        stringFrets[s] = 0;
        stringFingers[s] = 0;
      }
      else if (Control.ModifierKeys == Keys.Control)
      {
        // toggle mute
        stringFrets[s] = (stringFrets[s] == -2) ? -1 : -2;
        stringFingers[s] = 0;
      }
      else
      {
        if (f == 0)
        {
          stringFrets[s] = 0;
          stringFingers[s] = 0;
        }
        else
        {
          stringFrets[s] = f;
          stringFingers[s] = (int)numericFinger.Value;
        }
      }
    }

    private void btnExport_Click(object sender, EventArgs e)
    {
      using var sfd = new SaveFileDialog();
      sfd.Filter = "PNG Image|*.png";
      sfd.FileName = (string.IsNullOrWhiteSpace(textChordName.Text) ? "guitar-grid" : textChordName.Text) + ".png";
      if (sfd.ShowDialog() != DialogResult.OK) return;

      var bmp = new Bitmap(panelGrid.Width, panelGrid.Height);
      panelGrid.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
      bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
      MessageBox.Show("Exported to " + sfd.FileName);
    }

    private (Rectangle area, Point[] stringYs, int[] fretXs) ComputeLayout()
    {
      var area = panelGrid.ClientRectangle;
      int leftMargin = 60;
      int rightMargin = 20;
      int topMargin = 20;
      int bottomMargin = 20;

      int w = Math.Max(200, area.Width - leftMargin - rightMargin);
      int h = Math.Max(120, area.Height - topMargin - bottomMargin);

      int[] fretXs = new int[fretsCount + 1];
      for (int i = 0; i <= fretsCount; i++)
      {
        fretXs[i] = leftMargin + (int)(i * (w / (double)fretsCount));
      }

      Point[] stringYs = new Point[stringsCount];
      for (int s = 0; s < stringsCount; s++)
      {
        int y = topMargin + (int)((s) * (h / (double)(stringsCount - 1)));
        stringYs[s] = new Point(leftMargin, y);
      }

      if (reverseStringOrder)
        Array.Reverse(stringYs);

      return (new Rectangle(leftMargin, topMargin, w, h), stringYs, fretXs);
    }

    private int HitTestString(Point p, (Rectangle area, Point[] stringYs, int[] fretXs) layout)
    {
      var stringYs = layout.stringYs;
      for (int s = 0; s < stringYs.Length; s++)
      {
        if (Math.Abs(p.Y - stringYs[s].Y) <= 12) return s;
      }
      return -1;
    }

    private int HitTestFret(Point p, (Rectangle area, Point[] stringYs, int[] fretXs) layout)
    {
      var fretXs = layout.fretXs;
      for (int f = 0; f < fretXs.Length - 1; f++)
      {
        int x1 = fretXs[f];
        int x2 = fretXs[f + 1];
        if (p.X >= x1 - 6 && p.X <= x2 + 6)
        {
          return f; // return left fret index; 0 represents nut/open
        }
      }
      return -1;
    }

    private void PanelGrid_Paint(object sender, PaintEventArgs e)
    {
      var g = e.Graphics;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      g.Clear(Color.White);

      var layout = ComputeLayout();
      var area = layout.area;
      var stringYs = layout.stringYs;
      var fretXs = layout.fretXs;

      using var penString = new Pen(Color.Black, 2);
      using var penFret = new Pen(Color.Gray, 1);

      // Draw frets
      for (int f = 0; f < fretXs.Length; f++)
      {
        int x = fretXs[f];
        if (f == 0)
        {
          // nut
          g.FillRectangle(Brushes.Black, x - 4, area.Top - 6, 8, area.Height + 12);
        }
        else
        {
          g.DrawLine(penFret, x, area.Top, x, area.Bottom);
        }
      }

      // Draw strings
      for (int s = 0; s < stringYs.Length; s++)
      {
        int y = stringYs[s].Y;
        g.DrawLine(penString, fretXs[0], y, fretXs[fretXs.Length - 1], y);
      }

      // Draw open/muted labels
      var fontSmall = new Font("Segoe UI", 9);
      for (int s = 0; s < stringsCount; s++)
      {
        string label = (tuning != null && s < tuning.Length) ? tuning[s] : "";
        var sz = g.MeasureString(label, fontSmall);
        int y = stringYs[s].Y;
        g.DrawString(label, fontSmall, Brushes.Black, 8, y - sz.Height / 2);
      }

      // Draw fret numbers
      for (int f = 1; f <= fretsCount; f += 1)
      {
        int x = (fretXs[f] + fretXs[Math.Max(0, f - 1)]) / 2;
        g.DrawString(f.ToString(), fontSmall, Brushes.Black, x - 6, area.Bottom + 4);
      }

      // Draw notes
      for (int s = 0; s < stringsCount; s++)
      {
        int state = (s < stringFrets.Length) ? stringFrets[s] : -1;
        int finger = (s < stringFingers.Length) ? stringFingers[s] : 0;
        int y = stringYs[s].Y;

        if (state == -2)
        {
          // muted X above nut
          var sz = g.MeasureString("X", fontSmall);
          g.DrawString("X", fontSmall, Brushes.Black, fretXs[0] - 24, y - sz.Height / 2);
        }
        else if (state == 0)
        {
          // open O
          var sz = g.MeasureString("O", fontSmall);
          g.DrawString("O", fontSmall, Brushes.Black, fretXs[0] - 24, y - sz.Height / 2);
        }
        else if (state >= 1)
        {
          int f = Math.Min(state, fretsCount);
          int x1 = fretXs[f - 1];
          int x2 = fretXs[f];
          int cx = (x1 + x2) / 2;
          int radius = 12;
          var rect = new Rectangle(cx - radius, y - radius, radius * 2, radius * 2);
          g.FillEllipse(Brushes.Black, rect);
          g.DrawEllipse(Pens.Black, rect);
          if (finger > 0)
          {
            var fsz = g.MeasureString(finger.ToString(), fontSmall);
            g.DrawString(finger.ToString(), fontSmall, Brushes.White, cx - fsz.Width / 2, y - fsz.Height / 2);
          }
        }
      }

      // Draw barre if present
      if (barreFretIndex.HasValue && barreStartStringIndex.HasValue && barreEndStringIndex.HasValue)
      {
        int f = barreFretIndex.Value;
        int x1 = fretXs[Math.Max(0, f - 1)];
        int x2 = fretXs[Math.Min(f, fretXs.Length - 1)];
        int cx = (x1 + x2) / 2;
        int start = barreStartStringIndex.Value;
        int end = barreEndStringIndex.Value;
        if (start > end) { var tmp = start; start = end; end = tmp; }
        int top = stringYs[start].Y - 12;
        int bottom = stringYs[end].Y + 12;
        var rect = new Rectangle(cx - 40, top, 80, Math.Max(10, bottom - top));
        g.FillRectangle(Brushes.Black, rect);
      }

      // chord name
      if (!string.IsNullOrWhiteSpace(textChordName.Text))
      {
        var fontTitle = new Font("Segoe UI", 12, FontStyle.Bold);
        var t = textChordName.Text;
        var sz = g.MeasureString(t, fontTitle);
        g.DrawString(t, fontTitle, Brushes.Black, panelGrid.Width / 2 - sz.Width / 2, 2);
      }
    }

    private void btnChordGrid_Click(object sender, EventArgs e)
    {
      var dlg = new ChordGridForm();
      dlg.ShowDialog(this);
    }

    // Designer button handlers
    private void btnApplyBarre_Click(object sender, EventArgs e)
    {
      int fret = (int)numericBarreFret.Value;
      int s1 = (int)numericBarreStartString.Value - 1;
      int s2 = (int)numericBarreEndString.Value - 1;
      barreFretIndex = fret;
      barreStartStringIndex = Math.Max(0, Math.Min(stringsCount - 1, s1));
      barreEndStringIndex = Math.Max(0, Math.Min(stringsCount - 1, s2));
      panelGrid.Invalidate();
    }

    private void chkReverseStrings_CheckedChanged(object sender, EventArgs e)
    {
      reverseStringOrder = chkReverseStrings.Checked;
      panelGrid.Invalidate();
    }

    private void btnSavePreset_Click(object sender, EventArgs e)
    {
      using var sfd = new SaveFileDialog();
      sfd.Filter = "JSON Preset|*.json";
      sfd.FileName = (string.IsNullOrWhiteSpace(textChordName.Text) ? "preset" : textChordName.Text) + ".json";
      if (sfd.ShowDialog() != DialogResult.OK) return;
      var obj = new { Name = textChordName.Text, Frets = stringFrets, Fingers = stringFingers, Tuning = textTuning.Text };
      var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(sfd.FileName, json);
      MessageBox.Show("Preset saved to " + sfd.FileName);
    }

    private void btnLoadPreset_Click(object sender, EventArgs e)
    {
      using var ofd = new OpenFileDialog();
      ofd.Filter = "JSON Preset|*.json";
      if (ofd.ShowDialog() != DialogResult.OK) return;
      var json = File.ReadAllText(ofd.FileName);
      try
      {
        var doc = JsonDocument.Parse(json);
        if (doc.RootElement.TryGetProperty("Frets", out var fretsEl) && fretsEl.ValueKind == JsonValueKind.Array)
        {
          int i = 0;
          foreach (var el in fretsEl.EnumerateArray())
          {
            if (i >= stringFrets.Length) break;
            stringFrets[i++] = el.GetInt32();
          }
        }
        if (doc.RootElement.TryGetProperty("Fingers", out var fingersEl) && fingersEl.ValueKind == JsonValueKind.Array)
        {
          int i = 0;
          foreach (var el in fingersEl.EnumerateArray())
          {
            if (i >= stringFingers.Length) break;
            stringFingers[i++] = el.GetInt32();
          }
        }
        if (doc.RootElement.TryGetProperty("Tuning", out var tuningEl) && tuningEl.ValueKind == JsonValueKind.String)
        {
          textTuning.Text = tuningEl.GetString();
          ParseTuning();
        }
        panelGrid.Invalidate();
        MessageBox.Show("Preset loaded from " + ofd.FileName);
      }
      catch (Exception ex)
      {
        MessageBox.Show("Failed to load preset: " + ex.Message);
      }
    }

    private void btnExportSvg_Click(object sender, EventArgs e)
    {
      using var sfd = new SaveFileDialog();
      sfd.Filter = "SVG Image|*.svg";
      sfd.FileName = (string.IsNullOrWhiteSpace(textChordName.Text) ? "guitar-grid" : textChordName.Text) + ".svg";
      if (sfd.ShowDialog() != DialogResult.OK) return;
      // simple SVG writer: reuse layout and output minimal SVG
      var layout = ComputeLayout();
      var area = layout.area;
      var stringYs = layout.stringYs;
      var fretXs = layout.fretXs;
      using var sw = new StreamWriter(sfd.FileName);
      sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
      sw.WriteLine($"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{panelGrid.Width}\" height=\"{panelGrid.Height}\">\\n");
      // frets and strings
      for (int f = 0; f < fretXs.Length; f++)
      {
        int x = fretXs[f];
        sw.WriteLine($"<line x1=\"{x}\" y1=\"{area.Top}\" x2=\"{x}\" y2=\"{area.Bottom}\" stroke=\"#000\" />");
      }
      for (int s = 0; s < stringYs.Length; s++)
      {
        int y = stringYs[s].Y;
        sw.WriteLine($"<line x1=\"{fretXs[0]}\" y1=\"{y}\" x2=\"{fretXs[fretXs.Length - 1]}\" y2=\"{y}\" stroke=\"#000\" />");
      }
      sw.WriteLine("</svg>");
      MessageBox.Show("SVG exported to " + sfd.FileName);
    }

    private void btnExportHiRes_Click(object sender, EventArgs e)
    {
      using var sfd = new SaveFileDialog();
      sfd.Filter = "PNG Image|*.png";
      sfd.FileName = (string.IsNullOrWhiteSpace(textChordName.Text) ? "guitar-grid" : textChordName.Text) + "_hires.png";
      if (sfd.ShowDialog() != DialogResult.OK) return;
      int scale = 3;
      using var bmp = new Bitmap(panelGrid.Width * scale, panelGrid.Height * scale);
      using (var g = Graphics.FromImage(bmp))
      {
        g.ScaleTransform(scale, scale);
        var pe = new PaintEventArgs(g, new Rectangle(0, 0, panelGrid.Width, panelGrid.Height));
        PanelGrid_Paint(this, pe);
      }
      bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
      MessageBox.Show("Hi-res PNG exported to " + sfd.FileName);
    }
  }
}