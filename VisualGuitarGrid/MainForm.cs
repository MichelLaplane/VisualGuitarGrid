using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using VisualGuitarGrid.Preset;
using VisualGuitarGrid.Export;
using VisualGuitarGrid.Model;

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

    private List<GridBlock> grids = new List<GridBlock>();
    private int selectedGridIndex = -1;
    private bool isResizing = false;
    private int resizeGridIndex = -1;
    private Point resizeStart;
    private Size resizeStartSize;

    public MainForm()
    {
      InitializeComponent();
      InitState();
      panelGrid.Paint += PanelGrid_Paint;
      panelGrid.MouseClick += PanelGrid_MouseClick;
      panelGrid.MouseDown += panelGrid_MouseDown;
      panelGrid.MouseMove += panelGrid_MouseMove;
      panelGrid.MouseUp += panelGrid_MouseUp;

      // template combobox handlers
      if (this.comboTemplate != null)
      {
        comboTemplate.Items.Clear();
        comboTemplate.Items.AddRange(new string[] { "None", "Barre", "Repeat-Left", "Repeat-Right", "Diagonal-Split" });
        comboTemplate.SelectedIndex = 0;
      }
      //if (this.btnApplyTemplate != null)
      //  this.btnApplyTemplate.Click += btnApplyTemplate_Click;
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

    private void InitState()
    {
      // initialize two grids (A and B) stacked vertically by default
      grids.Clear();
      var width = panelGrid?.Width ?? 800;
      var height = panelGrid?.Height ?? 400;

      var gA = GridBlock.CreateDefault("A", new Rectangle(20, 20, Math.Max(300, width - 140), (height / 2) - 30));
      gA.Tempo = "120 bpm";
      gA.TimeSignature = "4/4";
      gA.RepeatLeft = true;
      gA.RepeatRight = true;
      grids.Add(gA);

      var gB = GridBlock.CreateDefault("B", new Rectangle(20, gA.Rect.Bottom + 20, Math.Max(300, width - 140), (height / 2) - 30));
      gB.Tempo = "120 bpm";
      gB.TimeSignature = "4/4";
      gB.RepeatLeft = true;
      gB.RepeatRight = true;
      grids.Add(gB);

      panelGrid?.Invalidate();
    }

    private void btnApplyTemplate_Click(object sender, EventArgs e)
    {
      if (selectedGridIndex < 0 || selectedGridIndex >= grids.Count) return;
      var grid = grids[selectedGridIndex];
      var sel = (comboTemplate?.SelectedItem ?? "None").ToString();
      switch (sel)
      {
        case "Barre": TemplatePresets.ApplyPreset(grid, TemplatePresets.Preset.BarrelBarre); break;
        case "Repeat-Left": TemplatePresets.ApplyPreset(grid, TemplatePresets.Preset.RepeatLeft); break;
        case "Repeat-Right": TemplatePresets.ApplyPreset(grid, TemplatePresets.Preset.RepeatRight); break;
        case "Diagonal-Split": TemplatePresets.ApplyPreset(grid, TemplatePresets.Preset.DiagonalSplit); break;
        default: break;
      }
      panelGrid.Invalidate();
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

    private void btnChordGrid_Click(object sender, EventArgs e)
    {
      var dlg = new ChordGridForm();
      dlg.ShowDialog(this);
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

    private void chkReverseStrings_CheckedChanged(object sender, EventArgs e)
    {
      reverseStringOrder = chkReverseStrings.Checked;
      panelGrid.Invalidate();
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

    private void btnPresetLibrary_Click(object sender, EventArgs e)
    {
      string defaultPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Presets", "sample_presets.json");
      List<ChordShape> presets = null;
      if (File.Exists(defaultPath))
      {
        presets = ChordPresetManager.LoadLibrary(defaultPath);
      }
      else
      {
        using var ofd = new OpenFileDialog();
        ofd.Filter = "Preset Library|*.json";
        ofd.Title = "Open preset library";
        if (ofd.ShowDialog() != DialogResult.OK) return;
        presets = ChordPresetManager.LoadLibrary(ofd.FileName);
      }

      if (presets == null || presets.Count == 0)
      {
        MessageBox.Show("No presets found in library.");
        return;
      }

      using var dlg = new PresetLibraryForm(presets);
      if (dlg.ShowDialog(this) == DialogResult.OK)
      {
        var p = dlg.SelectedPreset;
        if (p != null)
        {
          // apply frets and fingers (safe copy up to available strings)
          int copyCount = Math.Min(stringFrets.Length, p.StringFrets?.Length ?? 0);
          for (int i = 0; i < copyCount; i++) stringFrets[i] = p.StringFrets[i];
          for (int i = copyCount; i < stringFrets.Length; i++) stringFrets[i] = -1;

          int copyFingers = Math.Min(stringFingers.Length, p.Fingers?.Length ?? 0);
          for (int i = 0; i < copyFingers; i++) stringFingers[i] = p.Fingers[i];
          for (int i = copyFingers; i < stringFingers.Length; i++) stringFingers[i] = 0;

          if (!string.IsNullOrWhiteSpace(p.Tuning))
          {
            textTuning.Text = p.Tuning;
            ParseTuning();
          }

          if (!string.IsNullOrWhiteSpace(p.Name))
            textChordName.Text = p.Name;

          panelGrid.Invalidate();
        }
      }
    }



    private void PanelGrid_MouseClick(object sender, MouseEventArgs e)
    {
      // detect which grid
      for (int i = 0; i < grids.Count; i++)
      {
        if (grids[i].Rect.Contains(e.Location))
        {
          selectedGridIndex = i;
          var cell = grids[i].HitTestCell(e.Location);
          if (cell != null)
          {
            // toggle cell state similar to previous logic
            if (e.Button == MouseButtons.Right)
            {
              cell.State = (cell.State == -2) ? -1 : -2;
              cell.Finger = 0;
            }
            else if (Control.ModifierKeys == Keys.Shift)
            {
              cell.State = 0;
              cell.Finger = 0;
            }
            else
            {
              // set to next fret in that column (simple cycle)
              cell.State = (cell.State >= 1) ? -1 : 1;
              cell.Finger = (cell.State >= 1) ? (int)numericFinger.Value : 0;
            }
          }
          panelGrid.Invalidate();
          return;
        }
      }
      selectedGridIndex = -1;
      panelGrid.Invalidate();
    }

    private void panelGrid_MouseDown(object sender, MouseEventArgs e)
    {
      // check resize handles
      for (int i = 0; i < grids.Count; i++)
      {
        var grip = grids[i].GetResizeHandle();
        if (grip.Contains(e.Location))
        {
          isResizing = true;
          resizeGridIndex = i;
          resizeStart = e.Location;
          resizeStartSize = grids[i].Rect.Size;
          return;
        }
      }
    }

    private void panelGrid_MouseMove(object sender, MouseEventArgs e)
    {
      if (isResizing && resizeGridIndex >= 0 && resizeGridIndex < grids.Count)
      {
        var g = grids[resizeGridIndex];
        int dx = e.X - resizeStart.X;
        int dy = e.Y - resizeStart.Y;
        var newSize = new Size(Math.Max(120, resizeStartSize.Width + dx), Math.Max(80, resizeStartSize.Height + dy));
        g.Rect = new Rectangle(g.Rect.Location, newSize);
        panelGrid.Invalidate();
      }
      else
      {
        // change cursor if over a handle
        bool over = false;
        for (int i = 0; i < grids.Count; i++)
        {
          if (grids[i].GetResizeHandle().Contains(e.Location)) { over = true; break; }
        }
        panelGrid.Cursor = over ? Cursors.SizeNWSE : Cursors.Default;
      }
    }

    private void panelGrid_MouseUp(object sender, MouseEventArgs e)
    {
      isResizing = false;
      resizeGridIndex = -1;
    }

    private void PanelGrid_Paint(object sender, PaintEventArgs e)
    {
      var g = e.Graphics;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      g.Clear(Color.White);

      // draw each grid
      foreach (var grid in grids)
      {
        DrawGrid(g, grid);
      }

      // draw selection
      if (selectedGridIndex >= 0 && selectedGridIndex < grids.Count)
      {
        var sel = grids[selectedGridIndex];
        using var selPen = new Pen(Color.Orange, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
        g.DrawRectangle(selPen, sel.Rect);
      }
    }

    private void DrawGrid(Graphics g, GridBlock grid)
    {
      // header band
      int headerH = Math.Min(72, (int)(grid.Rect.Height * 0.12));
      var headerRect = new Rectangle(grid.Rect.X, grid.Rect.Y, grid.Rect.Width, headerH);
      g.FillRectangle(Brushes.Black, headerRect);

      // title
      if (!string.IsNullOrWhiteSpace(grid.Title))
      {
        var titleFont = FitFont(g, grid.Title.ToUpperInvariant(), "Segoe UI", FontStyle.Bold, headerRect.Width - 20, headerRect.Height - 8);
        var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        g.DrawString(grid.Title.ToUpperInvariant(), titleFont, Brushes.White, new RectangleF(headerRect.X, headerRect.Y + 6, headerRect.Width, headerRect.Height - 6), sf);
        titleFont.Dispose();
      }

      // tempo / time signature left area under header
      if (!string.IsNullOrWhiteSpace(grid.Tempo))
      {
        var tempoFont = new Font("Segoe UI", 10);
        g.DrawString(grid.Tempo, tempoFont, Brushes.DarkGray, grid.Rect.X + 12, headerRect.Bottom + 6);
      }
      if (!string.IsNullOrWhiteSpace(grid.TimeSignature))
      {
        var tsFont = new Font("Segoe UI", 10);
        g.DrawString(grid.TimeSignature, tsFont, Brushes.DodgerBlue, grid.Rect.X + 12, headerRect.Bottom + 24);
      }

      // left section label box
      if (!string.IsNullOrWhiteSpace(grid.SectionLabel))
      {
        var boxW = 56;
        var boxRect = new Rectangle(grid.Rect.X + 8, headerRect.Bottom + 6, boxW, Math.Max(80, grid.Rect.Height / 3));
        g.FillRectangle(Brushes.LightSkyBlue, boxRect);
        using var secFont = new Font("Segoe UI", 28, FontStyle.Bold);
        var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
        g.DrawString(grid.SectionLabel, secFont, Brushes.Blue, new RectangleF(boxRect.X, boxRect.Y + 6, boxRect.Width, boxRect.Height), sf);
      }

      // compute grid cell positions
      int leftArea = grid.Rect.X + 100; // reserve for tuning/section
      int right = grid.Rect.Right - 24;
      int top = headerRect.Bottom + 12;
      int bottom = grid.Rect.Bottom - 12;
      int rows = grid.Rows;
      int cols = grid.Columns;
      int cellH = Math.Max(28, (bottom - top) / Math.Max(1, rows));
      int cellW = Math.Max(40, (right - leftArea) / Math.Max(1, cols));

      // draw outer border
      using var borderPen = new Pen(Color.Black, grid.BorderThickness);
      g.DrawRectangle(borderPen, new Rectangle(leftArea, top, cellW * cols, cellH * rows));

      // draw horizontal and vertical cell lines
      using var linePen = new Pen(Color.Gray, 1);
      for (int r = 0; r <= rows; r++)
      {
        int y = top + r * cellH;
        g.DrawLine(linePen, leftArea, y, leftArea + cellW * cols, y);
      }
      for (int c = 0; c <= cols; c++)
      {
        int x = leftArea + c * cellW;
        g.DrawLine(linePen, x, top, x, top + cellH * rows);
      }

      // draw repeat bars if requested
      if (grid.RepeatLeft)
      {
        int rx = leftArea - 14;
        g.FillRectangle(Brushes.Black, rx - 6, top + 8, 6, (cellH * rows) - 16);
        g.FillRectangle(Brushes.Black, rx, top + 8, 2, (cellH * rows) - 16);
        var mid = top + cellH * rows / 2;
        g.FillEllipse(Brushes.Black, rx + 8, mid - 12, 8, 8);
        g.FillEllipse(Brushes.Black, rx + 8, mid + 4, 8, 8);
      }
      if (grid.RepeatRight)
      {
        int rx = leftArea + cellW * cols + 14;
        g.FillRectangle(Brushes.Black, rx + 6, top + 8, 6, (cellH * rows) - 16);
        g.FillRectangle(Brushes.Black, rx, top + 8, 2, (cellH * rows) - 16);
        var mid = top + cellH * rows / 2;
        g.FillEllipse(Brushes.Black, rx - 12, mid - 12, 8, 8);
        g.FillEllipse(Brushes.Black, rx - 12, mid + 4, 8, 8);
      }

      // draw cells content
      for (int r = 0; r < rows; r++)
      {
        for (int c = 0; c < cols; c++)
        {
          var cellRect = new Rectangle(leftArea + c * cellW, top + r * cellH, cellW, cellH);
          var cell = grid.Cells[r, c];
          DrawCell(g, cell, cellRect);
        }
      }

      // draw resize handle
      var handle = grid.GetResizeHandle();
      g.FillRectangle(Brushes.DarkGray, handle);
    }

    private void DrawCell(Graphics g, GridCell cell, Rectangle rect)
    {
      // background quadrant and diagonal labels
      if (!string.IsNullOrEmpty(cell.CornerLabel))
      {
        using var brush = new SolidBrush(Color.FromArgb(230, Color.LightGray));
        var tri = new Point[] { new Point(rect.Right - 1, rect.Top), new Point(rect.Right, rect.Bottom - 1), new Point(rect.Left + rect.Width - 1, rect.Top) };
      }

      // main symbol (O, X or filled dot)
      if (cell.State == -2)
      {
        var f = new Font("Segoe UI", Math.Max(10, rect.Height / 3));
        var sz = g.MeasureString("X", f);
        g.DrawString("X", f, Brushes.Black, rect.Left + 6, rect.Top + (rect.Height - sz.Height) / 2);
      }
      else if (cell.State == 0)
      {
        var f = new Font("Segoe UI", Math.Max(10, rect.Height / 3));
        var sz = g.MeasureString("O", f);
        g.DrawString("O", f, Brushes.Black, rect.Left + 6, rect.Top + (rect.Height - sz.Height) / 2);
      }
      else if (cell.State >= 1)
      {
        int radius = Math.Min(rect.Width, rect.Height) / 3;
        var circleRect = new Rectangle(rect.Left + (rect.Width / 2) - radius, rect.Top + (rect.Height / 2) - radius, radius * 2, radius * 2);
        g.FillEllipse(Brushes.Black, circleRect);
        if (cell.Finger > 0)
        {
          var ff = FitFont(g, cell.Finger.ToString(), "Segoe UI", FontStyle.Bold, circleRect.Width - 4, circleRect.Height - 4);
          var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
          g.DrawString(cell.Finger.ToString(), ff, Brushes.White, circleRect, sf);
          ff.Dispose();
        }
      }

      // triangle corner label if any
      if (!string.IsNullOrEmpty(cell.CornerLabel))
      {
        var triRect = new Rectangle(rect.Right - 36, rect.Bottom - 18, 36, 18);
        using var triBrush = new SolidBrush(Color.FromArgb(200, 240, 240, 255));
        g.FillRectangle(triBrush, triRect);
        var tf = FitFont(g, cell.CornerLabel, "Segoe UI", FontStyle.Regular, triRect.Width - 4, triRect.Height - 2);
        var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        g.DrawString(cell.CornerLabel, tf, Brushes.Black, triRect, sf);
        tf.Dispose();
      }
    }

    // Fit text into box by reducing font size until it fits
    private Font FitFont(Graphics g, string text, string family, FontStyle style, int maxWidth, int maxHeight)
    {
      for (int size = Math.Min(48, Math.Max(8, maxHeight)); size >= 8; size--)
      {
        var f = new Font(family, size, style);
        var sz = g.MeasureString(text, f);
        if (sz.Width <= maxWidth && sz.Height <= maxHeight)
          return f;
        f.Dispose();
      }
      return new Font(family, 8, style);
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


    private void btnExportSvg_Click(object sender, EventArgs e)
    {
      using var sfd = new SaveFileDialog();
      sfd.Filter = "SVG Image|*.svg";
      sfd.FileName = (string.IsNullOrWhiteSpace(textChordName.Text) ? "guitar-grid" : textChordName.Text) + ".svg";
      if (sfd.ShowDialog() != DialogResult.OK) return;

      // Build a combined SVG by delegating to StyledSvgExporter for each grid and composing.
      // For simplicity call CreateStyledSvgGrid for the bounding area encompassing all grids
      int minX = grids.Min(g => g.Rect.X);
      int minY = grids.Min(g => g.Rect.Y);
      int maxW = grids.Max(g => g.Rect.Right);
      int maxH = grids.Max(g => g.Rect.Bottom);
      int w = maxW + 20;
      int h = maxH + 20;

      // combine first grid as representative (StyledSvgExporter expects coordinates per grid)
      // Here we simply export the first grid for now but include others by stacking — a full composer could merge properly.
      var primary = grids.FirstOrDefault();
      if (primary == null) return;

      // compute fretXs/stringYs for primary
      int leftArea = primary.Rect.X + 100;
      int right = primary.Rect.Right - 24;
      int top = primary.Rect.Y + Math.Min(72, (int)(primary.Rect.Height * 0.12)) + 12;
      int bottom = primary.Rect.Bottom - 12;
      int rows = primary.Rows;
      int cols = primary.Columns;
      int cellH = Math.Max(28, (bottom - top) / Math.Max(1, rows));
      int cellW = Math.Max(40, (right - leftArea) / Math.Max(1, cols));
      int[] fretXs = new int[cols + 1];
      for (int c = 0; c <= cols; c++) fretXs[c] = leftArea + c * cellW;
      int[] sYs = new int[rows];
      for (int r = 0; r < rows; r++) sYs[r] = top + r * cellH + cellH / 2;

      var svg = StyledSvgExporter.CreateStyledSvgGrid(w, h,
          primary.Title,
          primary.Tempo,
          primary.SectionLabel,
          primary.TimeSignature,
          Enumerable.Repeat("E2", rows).ToArray(),
          primary.ToFlatStateArray(),
          primary.ToFlatFingerArray(),
          fretXs,
          sYs,
          null, null, null,
          repeatLeft: primary.RepeatLeft,
          repeatRight: primary.RepeatRight);

      File.WriteAllText(sfd.FileName, svg);
      MessageBox.Show("SVG exported to " + sfd.FileName);
    }
  }
}