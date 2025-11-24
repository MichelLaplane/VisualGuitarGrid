using System;
using System.Drawing;

namespace VisualGuitarGrid.Model
{
    public class GridCell
    {
        // -1 = empty, -2 = muted, 0 = open, >=1 note
        public int State { get; set; } = -1;
        public int Finger { get; set; } = 0;
        public string CornerLabel { get; set; } = null; // small diag label
    }

    public class GridBlock
    {
        public int Rows { get; set; } = 6;
        public int Columns { get; set; } = 5;
        public Rectangle Rect { get; set; }
        public string Title { get; set; } = "";
        public string Tempo { get; set; } = "";
        public string TimeSignature { get; set; } = "";
        public string SectionLabel { get; set; } = "A";
        public bool RepeatLeft { get; set; } = false;
        public bool RepeatRight { get; set; } = false;
        public GridCell[,] Cells { get; set; }
        public float BorderThickness { get; set; } = 2f;

        public static GridBlock CreateDefault(string title, Rectangle rect)
        {
            var g = new GridBlock();
            g.Title = title;
            g.Rect = rect;
            g.Rows = 6;
            g.Columns = 5;
            g.Cells = new GridCell[g.Rows, g.Columns];
            for (int r = 0; r < g.Rows; r++)
                for (int c = 0; c < g.Columns; c++)
                    g.Cells[r, c] = new GridCell();
            return g;
        }

        public GridCell HitTestCell(Point p)
        {
            int headerH = Math.Min(72, (int)(Rect.Height * 0.12));
            int leftArea = Rect.X + 100;
            int right = Rect.Right - 24;
            int top = Rect.Y + headerH + 12;
            int bottom = Rect.Bottom - 12;
            int cellH = Math.Max(28, (bottom - top) / Math.Max(1, Rows));
            int cellW = Math.Max(40, (right - leftArea) / Math.Max(1, Columns));
            for (int r = 0; r < Rows; r++)
                for (int c = 0; c < Columns; c++)
                {
                    var rect = new Rectangle(leftArea + c * cellW, top + r * cellH, cellW, cellH);
                    if (rect.Contains(p)) return Cells[r, c];
                }
            return null;
        }

        public Rectangle GetResizeHandle()
        {
            return new Rectangle(Rect.Right - 12, Rect.Bottom - 12, 12, 12);
        }

        public int[] ToFlatStateArray()
        {
            var arr = new int[Rows * Columns];
            int idx = 0;
            for (int r = 0; r < Rows; r++) for (int c = 0; c < Columns; c++) arr[idx++] = Cells[r, c].State;
            return arr;
        }
        public int[] ToFlatFingerArray()
        {
            var arr = new int[Rows * Columns];
            int idx = 0;
            for (int r = 0; r < Rows; r++) for (int c = 0; c < Columns; c++) arr[idx++] = Cells[r, c].Finger;
            return arr;
        }
    }
}