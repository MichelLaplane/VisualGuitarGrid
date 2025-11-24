using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace VisualGuitarGrid
{
  public partial class ChordGridForm : Form
  {
    private int rows = 2;
    private int cols = 4;
    private Cell[,] cells;

    // Presentation settings added for templates / header / repeats
    private string tempo = "120 bpm";
    private string timeSignature = "4/4";
    private string sectionLabel = "A";
    private bool repeatLeft = true;
    private bool repeatRight = true;

    public ChordGridForm()
    {
      InitializeComponent();
      InitCells();

      // wire existing handlers
      panelChord.Paint += PanelChord_Paint;
      panelChord.MouseDoubleClick += PanelChord_MouseDoubleClick;
      panelChord.MouseClick += PanelChord_MouseClick;
      btnExport.Click += BtnExport_Click;

      // wire new controls (they exist in the designer patch)
      comboTemplate.Items.Clear();
      comboTemplate.Items.AddRange(new string[] { "None", "Barre", "Repeat-Left", "Repeat-Right", "Diagonal-Split" });
      comboTemplate.SelectedIndex = 0;

      // load UI initial values
      textTempo.Text = tempo;
      textTimeSignature.Text = timeSignature;
      textSectionLabel.Text = sectionLabel;
      chkRepeatLeft.Checked = repeatLeft;
      chkRepeatRight.Checked = repeatRight;

      btnApplyTemplate.Click += BtnApplyTemplate_Click;
      btnApplyClose.Click += BtnApplyClose_Click;
      btnClose.Click += (s, e) => Close();
    }

    private void InitCells()
    {
      cells = new Cell[rows, cols];
      for (int r = 0; r < rows; r++)
        for (int c = 0; c < cols; c++)
          cells[r, c] = new Cell(CellType.Full) { Parts = new string[] { "" } };

      // sample content
      cells[0, 0].Parts[0] = "C";
      cells[0, 1].Parts[0] = "Em";
      cells[0, 2].Parts[0] = "Am";
      cells[0, 3].Parts[0] = "Em";
      cells[1, 0].Parts[0] = "C";
      cells[1, 1] = new Cell(CellType.Full) { Parts = new string[] { "%" } };
      cells[1, 2].Parts[0] = "G";
      cells[1, 3] = new Cell(CellType.DiagonalBLTR) { Parts = new string[] { "D", "Em" } };
    }

    private void PanelChord_MouseClick(object sender, MouseEventArgs e)
    {
      var rc = panelChord.ClientRectangle;
      var layout = ComputeLayout(rc);
      int col = (e.X - layout.leftArea) / layout.cellW;
      int row = (e.Y - layout.top) / layout.cellH;
      if (col < 0 || col >= cols || row < 0 || row >= rows) return;

      if (e.Button == MouseButtons.Left)
      {
        var cell = cells[row, col];
        cell.Type = cell.Type switch
        {
          CellType.Empty => CellType.Full,
          CellType.Full => CellType.DiagonalTLBR,
          CellType.DiagonalTLBR => CellType.DiagonalBLTR,
          CellType.DiagonalBLTR => CellType.Cross,
          CellType.Cross => CellType.Empty,
          _ => CellType.Full,
        };
        EnsureParts(cell);
      }
      panelChord.Invalidate();
    }

    private void PanelChord_MouseDoubleClick(object sender, MouseEventArgs e)
    {
      var rc = panelChord.ClientRectangle;
      var layout = ComputeLayout(rc);
      int col = (e.X - layout.leftArea) / layout.cellW;
      int row = (e.Y - layout.top) / layout.cellH;
      if (col < 0 || col >= cols || row < 0 || row >= rows) return;

      var cell = cells[row, col];
      EnsureParts(cell);
      string prompt = cell.Type switch
      {
        CellType.Full => "Enter text for cell:",
        CellType.DiagonalTLBR => "Enter top-left and bottom-right texts separated by comma:",
        CellType.DiagonalBLTR => "Enter top-right and bottom-left texts separated by comma:",
        CellType.Cross => "Enter 4 texts for TL, TR, BL, BR separated by commas:",
        _ => "Enter text:",
      };
      var input = SmallPrompt.ShowDialog(prompt, "Edit cell", string.Join(",", cell.Parts));
      if (input != null)
      {
        var parts = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < cell.Parts.Length; i++)
          cell.Parts[i] = i < parts.Length ? parts[i].Trim() : "";
        panelChord.Invalidate();
      }
    }

    private void EnsureParts(Cell cell)
    {
      switch (cell.Type)
      {
        case CellType.Full:
          if (cell.Parts == null || cell.Parts.Length != 1)
            cell.Parts = new string[] { "" };
          break;
        case CellType.DiagonalTLBR:
        case CellType.DiagonalBLTR:
          if (cell.Parts == null || cell.Parts.Length != 2)
            cell.Parts = new string[] { "", "" };
          break;
        case CellType.Cross:
          if (cell.Parts == null || cell.Parts.Length != 4)
            cell.Parts = new string[] { "", "", "", "" };
          break;
        default:
          cell.Parts = new string[] { "" };
          break;
      }
    }

    // Compute layout details taking into account header and left section area
    private (int leftArea, int top, int cellW, int cellH) ComputeLayout(Rectangle rc)
    {
      int headerH = Math.Max(36, rc.Height / 10);
      int leftArea = 72; // space for section label and tuning
      int top = headerH + 8;
      int bottom = rc.Bottom - 12;
      int usableW = rc.Width - leftArea - 24;
      int usableH = bottom - top;
      int cellW = Math.Max(40, usableW / Math.Max(1, cols));
      int cellH = Math.Max(28, usableH / Math.Max(1, rows));
      return (leftArea, top, cellW, cellH);
    }

    private void PanelChord_Paint(object sender, PaintEventArgs e)
    {
      var g = e.Graphics;
      g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      g.Clear(Color.White);
      var rc = panelChord.ClientRectangle;

      // Header band
      int headerH = Math.Max(36, rc.Height / 10);
      var headerRect = new Rectangle(rc.Left, rc.Top, rc.Width, headerH);
      using var headerBrush = new SolidBrush(Color.FromArgb(32, 32, 32));
      g.FillRectangle(headerBrush, headerRect);

      // Title / tempo
      var titleFont = new Font("Segoe UI", 14, FontStyle.Bold);
      g.DrawString("Chord Grid", titleFont, Brushes.White, headerRect.Left + 12, headerRect.Top + 6);
      var tempoFont = new Font("Segoe UI", 10, FontStyle.Regular);
      g.DrawString(tempo, tempoFont, Brushes.LightGray, headerRect.Left + 12, headerRect.Top + 26);
      g.DrawString(timeSignature, tempoFont, Brushes.CornflowerBlue, headerRect.Left + 100, headerRect.Top + 26);

      // Section label box on left below header
      var layout = ComputeLayout(rc);
      var boxRect = new Rectangle(8, headerRect.Bottom + 8, layout.leftArea - 16, Math.Max(64, rc.Height / 4));
      using var boxBrush = new SolidBrush(Color.FromArgb(240, 248, 255));
      g.FillRectangle(boxBrush, boxRect);
      var secFont = new Font("Segoe UI", 28, FontStyle.Bold);
      var sfCenter = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Near };
      g.DrawString(sectionLabel, secFont, Brushes.DodgerBlue, new RectangleF(boxRect.X, boxRect.Y + 8, boxRect.Width, boxRect.Height), sfCenter);

      // Grid area
      int leftArea = layout.leftArea;
      int top = layout.top;
      int cellW = layout.cellW;
      int cellH = layout.cellH;
      int gridW = cellW * cols;
      int gridH = cellH * rows;
      var gridRect = new Rectangle(leftArea, top, gridW, gridH);

      // Outer border
      using var borderPen = new Pen(Color.Black, 2);
      g.DrawRectangle(borderPen, gridRect);

      // lines
      using var thin = new Pen(Color.Gray, 1);
      for (int r = 0; r <= rows; r++)
      {
        int y = top + r * cellH;
        g.DrawLine(thin, leftArea, y, leftArea + gridW, y);
      }
      for (int c = 0; c <= cols; c++)
      {
        int x = leftArea + c * cellW;
        g.DrawLine(thin, x, top, x, top + gridH);
      }

      // Repeat bars (thick + thin + dots)
      if (repeatLeft)
      {
        int rx = leftArea - 14;
        g.FillRectangle(Brushes.Black, rx - 6, top + 8, 6, gridH - 16);
        g.FillRectangle(Brushes.Black, rx, top + 8, 2, gridH - 16);
        var mid = top + gridH / 2;
        g.FillEllipse(Brushes.Black, rx + 8, mid - 12, 8, 8);
        g.FillEllipse(Brushes.Black, rx + 8, mid + 4, 8, 8);
      }
      if (repeatRight)
      {
        int rx = leftArea + gridW + 14;
        g.FillRectangle(Brushes.Black, rx + 6, top + 8, 6, gridH - 16);
        g.FillRectangle(Brushes.Black, rx, top + 8, 2, gridH - 16);
        var mid = top + gridH / 2;
        g.FillEllipse(Brushes.Black, rx - 12, mid - 12, 8, 8);
        g.FillEllipse(Brushes.Black, rx - 12, mid + 4, 8, 8);
      }

      // Draw cells content with adjusted layout and better fitting
      for (int r = 0; r < rows; r++)
      {
        for (int c = 0; c < cols; c++)
        {
          var cellRect = new Rectangle(leftArea + c * cellW, top + r * cellH, cellW, cellH);
          var cell = cells[r, c];
          if (cell == null) continue;

          switch (cell.Type)
          {
            case CellType.Empty:
              break;
            case CellType.Full:
              DrawFittedTextCentered(g, cell.Parts.Length > 0 ? cell.Parts[0] : "", cellRect);
              break;
            case CellType.DiagonalTLBR:
              g.DrawLine(thin, cellRect.Left, cellRect.Top, cellRect.Right, cellRect.Bottom);
              DrawFittedTextInTriangle(g, cell.Parts.Length > 0 ? cell.Parts[0] : "", cellRect, TrianglePosition.TopLeft);
              DrawFittedTextInTriangle(g, cell.Parts.Length > 1 ? cell.Parts[1] : "", cellRect, TrianglePosition.BottomRight);
              break;
            case CellType.DiagonalBLTR:
              g.DrawLine(thin, cellRect.Left, cellRect.Bottom, cellRect.Right, cellRect.Top);
              DrawFittedTextInTriangle(g, cell.Parts.Length > 0 ? cell.Parts[0] : "", cellRect, TrianglePosition.TopRight);
              DrawFittedTextInTriangle(g, cell.Parts.Length > 1 ? cell.Parts[1] : "", cellRect, TrianglePosition.BottomLeft);
              break;
            case CellType.Cross:
              g.DrawLine(thin, cellRect.Left, cellRect.Top, cellRect.Right, cellRect.Bottom);
              g.DrawLine(thin, cellRect.Left, cellRect.Bottom, cellRect.Right, cellRect.Top);
              DrawFittedTextInQuadrant(g, cell.Parts, cellRect);
              break;
          }

          // draw small corner diagonal label if provided (bottom-right)
          if (cell.Parts != null && cell.Type == CellType.Full && cell.Parts.Length > 0 && cell.Parts[0].Contains("/"))
          {
            // if user uses slash notation to indicate corner, draw small label in corner (simple heuristic)
            var idx = cell.Parts[0].IndexOf('/');
            var corner = cell.Parts[0].Substring(idx + 1).Trim();
            if (!string.IsNullOrEmpty(corner))
            {
              var triRect = new Rectangle(cellRect.Right - 40, cellRect.Bottom - 20, 36, 18);
              using var triBrush = new SolidBrush(Color.FromArgb(220, Color.LightGray));
              g.FillRectangle(triBrush, triRect);
              var tf = FitFont(g, corner, "Segoe UI", FontStyle.Regular, triRect.Width - 4, triRect.Height - 2);
              var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
              g.DrawString(corner, tf, Brushes.Black, triRect, sf);
              tf.Dispose();
            }
          }
        }
      }
    }

    private void DrawFittedTextCentered(Graphics g, string text, Rectangle rect)
    {
      if (string.IsNullOrWhiteSpace(text)) return;

      // if repeat percent sign, draw large centered % sign
      if (text.Trim() == "%")
      {
        var f = FitFont(g, "%", "Segoe UI", FontStyle.Bold, rect.Width - 8, rect.Height - 8);
        var sz = g.MeasureString("%", f);
        g.DrawString("%", f, Brushes.Black, rect.Left + (rect.Width - sz.Width) / 2, rect.Top + (rect.Height - sz.Height) / 2);
        f.Dispose();
        return;
      }

      var font = FitFont(g, text, "Segoe UI", FontStyle.Bold, rect.Width - 8, rect.Height - 8);
      var sz2 = g.MeasureString(text, font);
      g.DrawString(text, font, Brushes.Black, rect.Left + (rect.Width - sz2.Width) / 2, rect.Top + (rect.Height - sz2.Height) / 2);
      font.Dispose();
    }

    enum TrianglePosition { TopLeft, TopRight, BottomLeft, BottomRight }

    private void DrawFittedTextInTriangle(Graphics g, string text, Rectangle rect, TrianglePosition pos)
    {
      if (string.IsNullOrWhiteSpace(text)) return;

      int margin = Math.Max(8, rect.Width / 20);
      Rectangle target;
      switch (pos)
      {
        case TrianglePosition.TopLeft:
          target = new Rectangle(rect.Left + margin, rect.Top + margin, rect.Width / 2 - margin * 2, rect.Height / 2 - margin * 2);
          break;
        case TrianglePosition.TopRight:
          target = new Rectangle(rect.Left + rect.Width / 2 + margin, rect.Top + margin, rect.Width / 2 - margin * 2, rect.Height / 2 - margin * 2);
          break;
        case TrianglePosition.BottomLeft:
          target = new Rectangle(rect.Left + margin, rect.Top + rect.Height / 2 + margin, rect.Width / 2 - margin * 2, rect.Height / 2 - margin * 2);
          break;
        default: // BottomRight
          target = new Rectangle(rect.Left + rect.Width / 2 + margin, rect.Top + rect.Height / 2 + margin, rect.Width / 2 - margin * 2, rect.Height / 2 - margin * 2);
          break;
      }

      var f = FitFont(g, text, "Segoe UI", FontStyle.Bold, target.Width - 4, target.Height - 4);
      var sz = g.MeasureString(text, f);
      g.DrawString(text, f, Brushes.Black, target.Left + (target.Width - sz.Width) / 2, target.Top + (target.Height - sz.Height) / 2);
      f.Dispose();
    }

    private void DrawFittedTextInQuadrant(Graphics g, string[] parts, Rectangle rect)
    {
      if (parts == null) return;
      int margin = Math.Max(6, rect.Width / 30);
      var small = new Font("Segoe UI", 12, FontStyle.Bold);
      if (parts.Length > 0 && !string.IsNullOrWhiteSpace(parts[0]))
      {
        var f = FitFont(g, parts[0], "Segoe UI", FontStyle.Bold, rect.Width / 2 - margin * 2, rect.Height / 2 - margin * 2);
        var sz = g.MeasureString(parts[0], f);
        g.DrawString(parts[0], f, Brushes.Black, rect.Left + margin, rect.Top + margin);
        f.Dispose();
      }
      if (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]))
      {
        var f = FitFont(g, parts[1], "Segoe UI", FontStyle.Bold, rect.Width / 2 - margin * 2, rect.Height / 2 - margin * 2);
        g.DrawString(parts[1], f, Brushes.Black, rect.Right - margin - g.MeasureString(parts[1], f).Width, rect.Top + margin);
        f.Dispose();
      }
      if (parts.Length > 2 && !string.IsNullOrWhiteSpace(parts[2]))
      {
        var f = FitFont(g, parts[2], "Segoe UI", FontStyle.Bold, rect.Width / 2 - margin * 2, rect.Height / 2 - margin * 2);
        g.DrawString(parts[2], f, Brushes.Black, rect.Left + margin, rect.Bottom - margin - g.MeasureString(parts[2], f).Height);
        f.Dispose();
      }
      if (parts.Length > 3 && !string.IsNullOrWhiteSpace(parts[3]))
      {
        var f = FitFont(g, parts[3], "Segoe UI", FontStyle.Bold, rect.Width / 2 - margin * 2, rect.Height / 2 - margin * 2);
        var w = g.MeasureString(parts[3], f).Width;
        g.DrawString(parts[3], f, Brushes.Black, rect.Right - margin - w, rect.Bottom - margin - g.MeasureString(parts[3], f).Height);
        f.Dispose();
      }
    }

    private void BtnExport_Click(object sender, EventArgs e)
    {
      using var sfd = new SaveFileDialog();
      sfd.Filter = "PNG Image|*.png";
      sfd.FileName = "chord-grid.png";
      if (sfd.ShowDialog() != DialogResult.OK) return;

      var bmp = new Bitmap(panelChord.Width, panelChord.Height);
      panelChord.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
      bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
      MessageBox.Show("Exported to " + sfd.FileName);
    }

    // Template application handlers (non-destructive)
    private void BtnApplyTemplate_Click(object sender, EventArgs e)
    {
      ApplySelectedTemplate(alsoApplySettings: false);
      panelChord.Invalidate();
    }

    private void BtnApplyClose_Click(object sender, EventArgs e)
    {
      ApplySelectedTemplate(alsoApplySettings: true);
      panelChord.Invalidate();
      Close();
    }

    private void ApplySelectedTemplate(bool alsoApplySettings)
    {
      var sel = (comboTemplate.SelectedItem ?? "None").ToString();
      switch (sel)
      {
        case "Barre":
          // fill first column with a barre-like marker (finger 1)
          for (int r = 0; r < rows; r++)
          {
            cells[r, 0].Type = CellType.Full;
            EnsureParts(cells[r, 0]);
            cells[r, 0].Parts[0] = "1";
          }
          break;
        case "Repeat-Left":
          repeatLeft = true;
          repeatRight = false;
          break;
        case "Repeat-Right":
          repeatRight = true;
          repeatLeft = false;
          break;
        case "Diagonal-Split":
          for (int r = 0; r < rows; r++)
          {
            for (int c = 0; c < cols; c++)
            {
              if ((r + c) % 2 == 0)
              {
                cells[r, c].Type = CellType.DiagonalTLBR;
                EnsureParts(cells[r, c]);
                cells[r, c].Parts[0] = "X";
                cells[r, c].Parts[1] = "";
              }
              else
              {
                cells[r, c].Type = CellType.Full;
                EnsureParts(cells[r, c]);
                cells[r, c].Parts[0] = "";
              }
            }
          }
          break;
        default:
          break;
      }

      if (alsoApplySettings)
      {
        // apply settings from the UI
        tempo = string.IsNullOrWhiteSpace(textTempo.Text) ? tempo : textTempo.Text.Trim();
        timeSignature = string.IsNullOrWhiteSpace(textTimeSignature.Text) ? timeSignature : textTimeSignature.Text.Trim();
        sectionLabel = string.IsNullOrWhiteSpace(textSectionLabel.Text) ? sectionLabel : textSectionLabel.Text.Trim();
        repeatLeft = chkRepeatLeft.Checked;
        repeatRight = chkRepeatRight.Checked;
      }
    }

    // Fit text into box by reducing font size until it fits
    private Font FitFont(Graphics g, string text, string family, FontStyle style, int maxWidth, int maxHeight)
    {
      for (int size = Math.Min(72, Math.Max(8, maxHeight)); size >= 8; size--)
      {
        var f = new Font(family, size, style);
        var sz = g.MeasureString(text, f);
        if (sz.Width <= maxWidth && sz.Height <= maxHeight)
          return f;
        f.Dispose();
      }
      return new Font(family, 8, style);
    }
  }

  enum CellType { Empty, Full, DiagonalTLBR, DiagonalBLTR, Cross }

  class Cell
  {
    public CellType Type { get; set; }
    public string[] Parts { get; set; }

    public Cell(CellType type)
    {
      Type = type;
      Parts = new string[] { "" };
    }
  }

  // small prompt helper
  internal static class SmallPrompt
  {
    public static string ShowDialog(string text, string caption, string initial = "")
    {
      using var form = new Form();
      using var lbl = new Label() { Left = 10, Top = 10, Text = text, AutoSize = true };
      using var txt = new TextBox() { Left = 10, Top = 36, Width = 420, Text = initial };
      using var btnOk = new Button() { Text = "OK", Left = 260, Width = 80, Height = 34, Top = 72, DialogResult = DialogResult.OK };
      using var btnCancel = new Button() { Text = "Cancel", Left = 350, Width = 80, Height=34 , Top = 72, DialogResult = DialogResult.Cancel };
      form.Text = caption;
      form.ClientSize = new Size(600, 110);
      form.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
      form.AcceptButton = btnOk;
      form.CancelButton = btnCancel;
      var dr = form.ShowDialog();
      return dr == DialogResult.OK ? txt.Text : null;
    }
  }
}