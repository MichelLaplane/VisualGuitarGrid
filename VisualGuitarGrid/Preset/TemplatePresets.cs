using System;
using VisualGuitarGrid.Model;

namespace VisualGuitarGrid.Preset
{
    public static class TemplatePresets
    {
        public enum Preset { None, BarrelBarre, RepeatLeft, RepeatRight, DiagonalSplit }

        public static void ApplyPreset(GridBlock g, Preset preset)
        {
            if (g == null) return;
            // clear
            for (int r = 0; r < g.Rows; r++) for (int c = 0; c < g.Columns; c++) { g.Cells[r, c].State = -1; g.Cells[r, c].Finger = 0; g.Cells[r, c].CornerLabel = null; }

            switch (preset)
            {
                case Preset.BarrelBarre:
                    // simple barre: fill first column
                    for (int r = 0; r < g.Rows; r++) { g.Cells[r, 0].State = 1; g.Cells[r, 0].Finger = 1; }
                    break;
                case Preset.RepeatLeft:
                    g.RepeatLeft = true; g.RepeatRight = false;
                    break;
                case Preset.RepeatRight:
                    g.RepeatRight = true; g.RepeatLeft = false;
                    break;
                case Preset.DiagonalSplit:
                    for (int r = 0; r < g.Rows; r++) for (int c = 0; c < g.Columns; c++) if ((r+c)%2==0) { g.Cells[r,c].State = 1; g.Cells[r,c].Finger = (c%4)+1; } 
                    break;
                default: break;
            }
        }
    }
}