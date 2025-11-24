using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VisualGuitarGrid.Export;
using VisualGuitarGrid.Utils;
using VisualGuitarGrid.Preset;

namespace VisualGuitarGrid
{
    public partial class MainForm : Form
    {
        // New fields
        private int? barreFretIndex = null;
        private int? barreStartStringIndex = null;
        private int? barreEndStringIndex = null;

        // Drag support
        private bool isDragging = false;
        private int dragStringIndex = -1;
        private int dragFretIndex = -1;

        // Tuning options
        private bool reverseStringOrder = false; // wire to a checkbox in designer

        // ComputeLayout using logarithmic spacing
        private (Rectangle area, Point[] stringYs, int[] fretXs) ComputeLayout()
        {
            var area = panelGrid.ClientRectangle;
            int leftMargin = 60;
            int rightMargin = 20;
            int topMargin = 20;
            int bottomMargin = 20;

            int w = Math.Max(200, area.Width - leftMargin - rightMargin);
            int h = Math.Max(120, area.Height - topMargin - bottomMargin);

            // scale length in "pixels" is the drawable fretboard length (we'll use the full width between nut and last fret)
            double scaleLengthPx = w;

            // compute physical fret positions (offsets from nut)
            double[] fretPhys = FretCalculator.GetFretPositions(fretsCount, scaleLengthPx);

            // map to screen coordinates
            int[] fretXs = FretCalculator.MapToScreen(fretPhys, leftMargin, w);

            // compute string y positions evenly distributed
            Point[] stringYs = new Point[stringsCount];
            for (int s = 0; s < stringsCount; s++)
            {
                int y = topMargin + (int)((s) * (h / (double)(Math.Max(1, stringsCount - 1))));
                stringYs[s] = new Point(leftMargin, y);
            }

            // optionally reverse string order if UI says so
            if (reverseStringOrder)
            {
                Array.Reverse(stringYs);
            }

            return (new Rectangle(leftMargin, topMargin, w, h), stringYs, fretXs);
        }

        // draw barre chord if set
        private void DrawBarre(Graphics g, int[] fretXs, Point[] stringYs)
        {
            if (!barreFretIndex.HasValue || !barreStartStringIndex.HasValue || !barreEndStringIndex.HasValue) return;

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
            using var brush = new SolidBrush(Color.Black);
            g.FillRoundedRectangle(brush, rect, 6); // helper extension drawn below
        }

        // helper: high resolution PNG export (render at bigger scale)
        private void ExportHighResPng(string filePath, int scaleMultiplier = 3)
        {
            int w = panelGrid.Width * scaleMultiplier;
            int h = panelGrid.Height * scaleMultiplier;
            using var bmp = new Bitmap(w, h);
            using var g = Graphics.FromImage(bmp);
            g.ScaleTransform(scaleMultiplier, scaleMultiplier);
            // invoke the same painting logic used in panelGrid.Paint but on this Graphics
            // simplest approach: create a new PaintEventArgs and call the paint method
            var pe = new PaintEventArgs(g, new Rectangle(0, 0, panelGrid.Width, panelGrid.Height));
            PanelGrid_Paint(this, pe);
            bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        }

        // export svg using SvgExporter
        private void ExportSvg(string filePath)
        {
            var layout = ComputeLayout();
            var area = layout.area;
            var stringYs = layout.stringYs;
            var fretXs = layout.fretXs;

            // prepare arrays for exporter
            int[] stringYInts = new int[stringYs.Length];
            for (int i = 0; i < stringYs.Length; i++) stringYInts[i] = stringYs[i].Y;
            string[] tuningArr = tuning; // tuning[] already present
            int[] sf = stringFrets;
            int[] fn = stringFingers;

            var svg = SvgExporter.CreateSvgGrid(panelGrid.Width, panelGrid.Height, textChordName.Text, tuningArr, sf, fn, fretXs, stringYInts, barreFretIndex, barreStartStringIndex, barreEndStringIndex);
            File.WriteAllText(filePath, svg);
        }

        // Drag/Touch handlers - wire these to panelGrid MouseDown/MouseMove/MouseUp
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

        // small extension helper to fill rounded rectangles (System.Drawing doesn't provide direct FillRoundedRectangle)
    }
}