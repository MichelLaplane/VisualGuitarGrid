using System;
using System.Globalization;
using System.Text;

namespace VisualGuitarGrid.Export
{
    // A minimal SVG builder for the grid: lines, text, circles, rounded barres.
    public static class SvgExporter
    {
        public static string CreateSvgGrid(int width, int height, string title, string[] tuning, int[] stringFrets, int[] fingerNumbers, int[] fretXs, int[] stringYs, int? barreFret = null, int? barreStartString = null, int? barreEndString = null)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine($"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{width}\" height=\"{height}\" viewBox=\"0 0 {width} {height}\">");
            sb.AppendLine("<defs>");
            sb.AppendLine("<style>");
            sb.AppendLine("  .string { stroke: #000; stroke-width: 2 }");
            sb.AppendLine("  .fret { stroke: #777; stroke-width: 1 }");
            sb.AppendLine("  .nut { stroke: #000; stroke-width: 6 }");
            sb.AppendLine("  .note { fill: #000; }");
            sb.AppendLine("  .chordText { font-family: 'Segoe UI', Arial, sans-serif; font-size: 28px; fill: #000; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</defs>");

            // Background
            sb.AppendLine($"<rect x=\"0\" y=\"0\" width=\"{width}\" height=\"{height}\" fill=\"#ffffff\"/>");

            // frets
            for (int i = 0; i < fretXs.Length; i++)
            {
                int x = fretXs[i];
                if (i == 0) // nut thicker
                    sb.AppendLine($"<line x1=\"{x}\" y1=\"20\" x2=\"{x}\" y2=\"{height - 40}\" class=\"nut\" />");
                else
                    sb.AppendLine($"<line x1=\"{x}\" y1=\"20\" x2=\"{x}\" y2=\"{height - 40}\" class=\"fret\" />");
            }

            // strings
            for (int s = 0; s < stringYs.Length; s++)
                sb.AppendLine($"<line x1=\"{fretXs[0]}\" y1=\"{stringYs[s]}\" x2=\"{fretXs[fretXs.Length - 1]}\" y2=\"{stringYs[s]}\" class=\"string\" />");

            // tuning labels
            for (int s = 0; s < tuning.Length && s < stringYs.Length; s++)
                sb.AppendLine($"<text x=\"8\" y=\"{stringYs[s] + 8}\" class=\"chordText\">{Escape(tuning[s])}</text>");

            // notes
            for (int s = 0; s < stringFrets.Length && s < stringYs.Length; s++)
            {
                int state = stringFrets[s];
                int y = stringYs[s];
                if (state == -2)
                {
                    sb.AppendLine($"<text x=\"{fretXs[0] - 24}\" y=\"{y + 6}\" class=\"chordText\">X</text>");
                }
                else if (state == 0)
                {
                    sb.AppendLine($"<text x=\"{fretXs[0] - 24}\" y=\"{y + 6}\" class=\"chordText\">O</text>");
                }
                else if (state >= 1)
                {
                    int f = Math.Min(state, fretXs.Length - 1);
                    int x1 = fretXs[Math.Max(0, f - 1)];
                    int x2 = fretXs[f];
                    int cx = (x1 + x2) / 2;
                    int r = 12;
                    sb.AppendLine($"<circle cx=\"{cx}\" cy=\"{y}\" r=\"{r}\" class=\"note\" />");
                    if (fingerNumbers != null && fingerNumbers.Length > s && fingerNumbers[s] > 0)
                        sb.AppendLine($"<text x=\"{cx - 6}\" y=\"{y + 8}\" fill=\"#fff\" font-family=\"'Segoe UI'\" font-size=\"12\">{fingerNumbers[s]}</text>");
                }
            }

            // barre if any
            if (barreFret.HasValue && barreStartString.HasValue && barreEndString.HasValue)
            {
                int f = barreFret.Value;
                int x1 = fretXs[Math.Max(0, f - 1)];
                int x2 = fretXs[Math.Min(f, fretXs.Length - 1)];
                int cx = (x1 + x2) / 2;
                int top = stringYs[barreStartString.Value];
                int bottom = stringYs[barreEndString.Value];
                if (top > bottom) { var tmp = top; top = bottom; bottom = tmp; }
                int heightBar = Math.Max(12, bottom - top);
                int barY = (top + bottom) / 2;
                sb.AppendLine($"<rect x=\"{cx - 40}\" y=\"{barY - heightBar/2}\" width=\"80\" height=\"{heightBar}\" rx=\"6\" fill=\"#000\" />");
            }

            // title
            if (!string.IsNullOrEmpty(title))
                sb.AppendLine($"<text x=\"{width / 2}\" y=\"18\" text-anchor=\"middle\" font-family=\"'Segoe UI'\" font-size=\"16\">{Escape(title)}</text>");

            sb.AppendLine("</svg>");
            return sb.ToString();

            static string Escape(string s) => System.Security.SecurityElement.Escape(s ?? "");
        }
    }
}