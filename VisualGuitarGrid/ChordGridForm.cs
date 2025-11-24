using System;
using System.Drawing;
using System.Windows.Forms;
namespace VisualGuitarGrid
{
    public partial class ChordGridForm : Form
    {
        private int rows = 2;
        private int cols = 4;
        private Cell[,] cells;

        public ChordGridForm()
        {
            InitializeComponent();
            InitCells();
            panelChord.Paint += PanelChord_Paint;
            panelChord.MouseDoubleClick += PanelChord_MouseDoubleClick;
            panelChord.MouseClick += PanelChord_MouseClick;
            btnExport.Click += BtnExport_Click;
        }

        private void InitCells()
        {
            cells = new Cell[rows, cols];
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    cells[r, c] = new Cell(CellType.Full) { Parts = new string[] { "" } };
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
            int cellW = rc.Width / cols;
            int cellH = rc.Height / rows;
            int col = Math.Min(cols - 1, e.X / cellW);
            int row = Math.Min(rows - 1, e.Y / cellH);
            if (row < 0 || col < 0) return;
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
            int cellW = rc.Width / cols;
            int cellH = rc.Height / rows;
            int col = Math.Min(cols - 1, e.X / cellW);
            int row = Math.Min(rows - 1, e.Y / cellH);
            if (row < 0 || col < 0) return;
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
            var input = Prompt.ShowDialog(prompt, "Edit cell", string.Join(",", cell.Parts));
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

        private void PanelChord_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);
            var rc = panelChord.ClientRectangle;
            int cellW = rc.Width / cols;
            int cellH = rc.Height / rows;
            using var pen = new Pen(Color.Black, 3);
            using var penThin = new Pen(Color.Black, 1);
            var font = new Font("Segoe UI", 28, FontStyle.Regular);
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    var cellRect = new Rectangle(c * cellW, r * cellH, cellW, cellH);
                    g.DrawRectangle(pen, cellRect);
                    var cell = cells[r, c];
                    if (cell == null) continue;
                    switch (cell.Type)
                    {
                        case CellType.Empty:
                            break;
                        case CellType.Full:
                            DrawCenteredText(g, cell.Parts.Length > 0 ? cell.Parts[0] : "", font, Brushes.Black, cellRect);
                            break;
                        case CellType.DiagonalTLBR:
                            g.DrawLine(penThin, cellRect.Left, cellRect.Top, cellRect.Right, cellRect.Bottom);
                            DrawCenteredTextInTriangle(g, cell.Parts.Length > 0 ? cell.Parts[0] : "", font, Brushes.Black, cellRect, TrianglePosition.TopLeft);
                            DrawCenteredTextInTriangle(g, cell.Parts.Length > 1 ? cell.Parts[1] : "", font, Brushes.Black, cellRect, TrianglePosition.BottomRight);
                            break;
                        case CellType.DiagonalBLTR:
                            g.DrawLine(penThin, cellRect.Left, cellRect.Bottom, cellRect.Right, cellRect.Top);
                            DrawCenteredTextInTriangle(g, cell.Parts.Length > 0 ? cell.Parts[0] : "", font, Brushes.Black, cellRect, TrianglePosition.TopRight);
                            DrawCenteredTextInTriangle(g, cell.Parts.Length > 1 ? cell.Parts[1] : "", font, Brushes.Black, cellRect, TrianglePosition.BottomLeft);
                            break;
                        case CellType.Cross:
                            g.DrawLine(penThin, cellRect.Left, cellRect.Top, cellRect.Right, cellRect.Bottom);
                            g.DrawLine(penThin, cellRect.Left, cellRect.Bottom, cellRect.Right, cellRect.Top);
                            DrawCenteredTextInQuadrant(g, cell.Parts, font, Brushes.Black, cellRect);
                            break;
                    }
                }
            }
        }

        private void DrawCenteredText(Graphics g, string text, Font font, Brush brush, Rectangle rect)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            var sz = g.MeasureString(text, font);
            g.DrawString(text, font, brush, rect.Left + (rect.Width - sz.Width) / 2, rect.Top + (rect.Height - sz.Height) / 2);
        }

        enum TrianglePosition { TopLeft, TopRight, BottomLeft, BottomRight }

        private void DrawCenteredTextInTriangle(Graphics g, string text, Font font, Brush brush, Rectangle rect, TrianglePosition pos)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            float cx = rect.Left + rect.Width / 2f;
            float cy = rect.Top + rect.Height / 2f;
            float tx = cx, ty = cy;
            int margin = 12;
            switch (pos)
            {
                case TrianglePosition.TopLeft:
                    tx = rect.Left + margin;
                    ty = rect.Top + margin;
                    break;
                case TrianglePosition.TopRight:
                    tx = rect.Right - margin - 40;
                    ty = rect.Top + margin;
                    break;
                case TrianglePosition.BottomLeft:
                    tx = rect.Left + margin;
                    ty = rect.Bottom - margin - 40;
                    break;
                case TrianglePosition.BottomRight:
                    tx = rect.Right - margin - 40;
                    ty = rect.Bottom - margin - 40;
                    break;
            }
            g.DrawString(text, font, brush, tx, ty);
        }

        private void DrawCenteredTextInQuadrant(Graphics g, string[] parts, Font font, Brush brush, Rectangle rect)
        {
            // parts: TL, TR, BL, BR
            if (parts == null) return;
            var smallFont = new Font("Segoe UI", 16, FontStyle.Regular);
            if (parts.Length > 0 && !string.IsNullOrWhiteSpace(parts[0]))
                g.DrawString(parts[0], smallFont, brush, rect.Left + 8, rect.Top + 8);
            if (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]))
                g.DrawString(parts[1], smallFont, brush, rect.Right - 40, rect.Top + 8);
            if (parts.Length > 2 && !string.IsNullOrWhiteSpace(parts[2]))
                g.DrawString(parts[2], smallFont, brush, rect.Left + 8, rect.Bottom - 32);
            if (parts.Length > 3 && !string.IsNullOrWhiteSpace(parts[3]))
                g.DrawString(parts[3], smallFont, brush, rect.Right - 40, rect.Bottom - 32);
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
}