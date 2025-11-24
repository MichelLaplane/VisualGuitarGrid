using System;
using System.Globalization;
using System.Text;

namespace VisualGuitarGrid.Export
{
    // StyledSvgExporter: produces an SVG tuned to the visual style:
    // - Header band with bold title
    // - Tempo text
    // - A/B section label box
    // - Thick nut and repeat bar markers with dots
    // - Larger chord title and serif-like weights approximated via font weights
    public static class StyledSvgExporter
    {
        public static string CreateStyledSvgGrid(int width, int height,
            string title,
            string tempo,
            string sectionLabel,
            string timeSignature,
            string[] tuning,
            int[] stringFrets,
            int[] fingerNumbers,
            int[] fretXs,
            int[] stringYs,
            int? barreFret = null,
            int? barreStartString = null,
            int? barreEndString = null,
            bool repeatLeft = false,
            bool repeatRight = false)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine($"<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"{width}\" height=\"{height}\" viewBox=\"0 0 {width} {height}\">");

            // Styles tuned to match the reference look
            sb.AppendLine("<defs>");
            sb.AppendLine("<style type=\"text/css\"><![CDATA[");
            sb.AppendLine("  .bg{fill:#ffffff}");
            sb.AppendLine("  .header{fill:#111111}");
            sb.AppendLine("  .header-title{fill:#ffffff;font-family:'Segoe UI',Arial,sans-serif;font-weight:800;font-size:28px;letter-spacing:1px}");
            sb.AppendLine("  .header-sub{fill:#e35205;font-family:'Segoe UI',Arial,sans-serif;font-weight:700;font-size:18px}");
            sb.AppendLine("  .tempo{fill:#444;font-family:'Segoe UI',Arial,sans-serif;font-size:16px}");
            sb.AppendLine("  .section-label{fill:#1f73b7;font-family:'Segoe UI',Arial,sans-serif;font-weight:800;font-size:42px}");
            sb.AppendLine("  .timesig{fill:#1f73b7;font-family:'Segoe UI',Arial,sans-serif;font-size:14px}");
            sb.AppendLine("  .grid-border{fill:none;stroke:#000;stroke-width:3}");
            sb.AppendLine("  .cell-line{fill:none;stroke:#333;stroke-width:1}");
            sb.AppendLine("  .nut{stroke:#000;stroke-width:8}");
            sb.AppendLine("  .string{stroke:#222;stroke-width:2}");
            sb.AppendLine("  .note{fill:#000}");
            sb.AppendLine("  .note-text{fill:#fff;font-family:'Segoe UI',Arial,sans-serif;font-weight:700;font-size:14px}");
            sb.AppendLine("  .chord-text{fill:#000;font-family:'Segoe UI',Arial,sans-serif;font-weight:800;font-size:48px}");
            sb.AppendLine("  .repeat-dot{fill:#000}");
            sb.AppendLine("]]></style>");
            sb.AppendLine("</defs>");

            // Background
            sb.AppendLine($"<rect x=\"0\" y=\"0\" width=\"{width}\" height=\"{height}\" class=\"bg\" />");

            // Header band
            int headerH = 72;
            sb.AppendLine($"<rect x=\"0\" y=\"0\" width=\"{width}\" height=\"{headerH}\" class=\"header\" />");
            if (!string.IsNullOrEmpty(title))
            {
                // Title centered in header (uppercase)
                sb.AppendLine($"<text x=\"{width / 2}\" y=\"42\" text-anchor=\"middle\" class=\"header-title\">{Escape(title.ToUpperInvariant())}</text>");
                // Small subtitle to mimic the reference (can be changed)
                sb.AppendLine($"<text x=\"{width / 2}\" y=\"58\" text-anchor=\"middle\" class=\"header-sub\">GUITAR DIAGRAM</text>");
            }

            // Tempo text under header, left side
            if (!string.IsNullOrEmpty(tempo))
            {
                sb.AppendLine($"<text x=\"24\" y=\"{headerH + 28}\" class=\"tempo\">Tempo: {Escape(tempo)}</text>");
            }

            // Section label (A/B) left vertical box
            if (!string.IsNullOrEmpty(sectionLabel))
            {
                int leftBoxW = 56;
                int leftX = 18;
                int leftY = headerH + 10;
                sb.AppendLine($"<rect x=\"{leftX - 6}\" y=\"{leftY - 6}\" width=\"{leftBoxW + 12}\" height=\"{220}\" fill=\"#f2f8ff\" rx=\"6\" stroke=\"#1f73b7\" stroke-width=\"2\" />");
                sb.AppendLine($"<text x=\"{leftX + leftBoxW / 2}\" y=\"{leftY + 44}\" text-anchor=\"middle\" class=\"section-label\">{Escape(sectionLabel)}</text>");
                if (!string.IsNullOrEmpty(timeSignature))
                {
                    sb.AppendLine($"<text x=\"{leftX + leftBoxW / 2}\" y=\"{leftY + 84}\" text-anchor=\"middle\" class=\"timesig\">{Escape(timeSignature)}</text>");
                }
            }

            // Board area (under the header)
            int top = headerH + 8;
            int leftMargin = 110; // keep space for tuning labels and section label
            int rightMargin = 48;
            int boardW = width - leftMargin - rightMargin;
            int boardH = height - top - 40;
            int gridX = leftMargin;
            int gridY = top;

            // Outer border
            sb.AppendLine($"<rect x=\"{gridX}\" y=\"{gridY}\" width=\"{boardW}\" height=\"{boardH}\" class=\"grid-border\" rx=\"6\" />");

            // If fretXs and stringYs are passed as absolute positions, assume they are ready to use.
            // Draw frets
            for (int i = 0; i < fretXs.Length; i++)
            {
                int x = fretXs[i];
                int y1 = gridY + 8;
                int y2 = gridY + boardH - 8;
                if (i == 0)
                {
                    sb.AppendLine($"<line x1=\"{x}\" y1=\"{y1}\" x2=\"{x}\" y2=\"{y2}\" class=\"nut\" />");
                }
                else
                {
                    sb.AppendLine($"<line x1=\"{x}\" y1=\"{y1}\" x2=\"{x}\" y2=\"{y2}\" class=\"cell-line\" />");
                }
            }

            // Draw strings (horizontal lines)
            for (int s = 0; s < stringYs.Length; s++)
            {
                int y = stringYs[s];
                sb.AppendLine($"<line x1=\"{fretXs[0]}\" y1=\"{y}\" x2=\"{fretXs[fretXs.Length - 1]}\" y2=\"{y}\" class=\"string\" />");
            }

            // Repeat bars (left/right)
            if (repeatLeft)
            {
                int rx = fretXs[0] - 10;
                sb.AppendLine($"<line x1=\"{rx - 6}\" y1=\"{gridY + 16}\" x2=\"{rx - 6}\" y2=\"{gridY + boardH - 16}\" stroke=\"#000\" stroke-width=\"6\" />");
                sb.AppendLine($"<line x1=\"{rx}\" y1=\"{gridY + 16}\" x2=\"{rx}\" y2=\"{gridY + boardH - 16}\" stroke=\"#000\" stroke-width=\"2\" />");
                int dotX = rx + 8;
                int mid = gridY + boardH / 2;
                sb.AppendLine($"<circle cx=\"{dotX}\" cy=\"{mid - 12}\" r=\"4\" class=\"repeat-dot\" />");
                sb.AppendLine($"<circle cx=\"{dotX}\" cy=\"{mid + 12}\" r=\"4\" class=\"repeat-dot\" />");
            }
            if (repeatRight)
            {
                int rx = fretXs[fretXs.Length - 1] + 10;
                sb.AppendLine($"<line x1=\"{rx + 6}\" y1=\"{gridY + 16}\" x2=\"{rx + 6}\" y2=\"{gridY + boardH - 16}\" stroke=\"#000\" stroke-width=\"6\" />");
                sb.AppendLine($"<line x1=\"{rx}\" y1=\"{gridY + 16}\" x2=\"{rx}\" y2=\"{gridY + boardH - 16}\" stroke=\"#000\" stroke-width=\"2\" />");
                int dotX = rx - 8;
                int mid = gridY + boardH / 2;
                sb.AppendLine($"<circle cx=\"{dotX}\" cy=\"{mid - 12}\" r=\"4\" class=\"repeat-dot\" />");
                sb.AppendLine($"<circle cx=\"{dotX}\" cy=\"{mid + 12}\" r=\"4\" class=\"repeat-dot\" />");
            }

            // Tuning labels (left of nut)
            for (int s = 0; s < tuning.Length && s < stringYs.Length; s++)
            {
                int y = stringYs[s];
                sb.AppendLine($"<text x=\"{gridX - 52}\" y=\"{y + 6}\" font-family=\"'Segoe UI'\" font-size=\"12\" fill=\"#000\">{Escape(tuning[s])}</text>");
            }

            // Notes (circles, O, X)
            for (int s = 0; s < stringFrets.Length && s < stringYs.Length; s++)
            {
                int state = stringFrets[s];
                int y = stringYs[s];
                if (state == -2)
                {
                    sb.AppendLine($"<text x=\"{fretXs[0] - 44}\" y=\"{y + 6}\" font-family=\"'Segoe UI'\" font-size=\"14\" fill=\"#000\">X</text>");
                }
                else if (state == 0)
                {
                    sb.AppendLine($"<text x=\"{fretXs[0] - 44}\" y=\"{y + 6}\" font-family=\"'Segoe UI'\" font-size=\"14\" fill=\"#000\">O</text>");
                }
                else if (state >= 1)
                {
                    int f = Math.Min(state, fretXs.Length - 1);
                    int x1 = fretXs[Math.Max(0, f - 1)];
                    int x2 = fretXs[f];
                    int cx = (x1 + x2) / 2;
                    int r = 14;
                    sb.AppendLine($"<circle cx=\"{cx}\" cy=\"{y}\" r=\"{r}\" class=\"note\" />");
                    if (fingerNumbers != null && fingerNumbers.Length > s && fingerNumbers[s] > 0)
                        sb.AppendLine($"<text x=\"{cx - 6}\" y=\"{y + 6}\" class=\"note-text\">{fingerNumbers[s]}</text>");
                }
            }

            // Barre (rounded rectangle)
            if (barreFret.HasValue && barreStartString.HasValue && barreEndString.HasValue)
            {
                int f = barreFret.Value;
                int x1 = fretXs[Math.Max(0, f - 1)];
                int x2 = fretXs[Math.Min(f, fretXs.Length - 1)];
                int cx = (x1 + x2) / 2;
                int topS = Math.Min(barreStartString.Value, barreEndString.Value);
                int bottomS = Math.Max(barreStartString.Value, barreEndString.Value);
                int topY = stringYs[topS] - 16;
                int bottomY = stringYs[bottomS] + 16;
                int bw = 88;
                int bh = Math.Max(14, bottomY - topY);
                int bx = cx - bw / 2;
                int by = topY;
                sb.AppendLine($"<rect x=\"{bx}\" y=\"{by}\" width=\"{bw}\" height=\"{bh}\" rx=\"10\" fill=\"#000\" />");
            }

            // Footer small copyright to match reference feel
            if (!string.IsNullOrEmpty(title))
            {
                sb.AppendLine($"<text x=\"{width / 2}\" y=\"{height - 10}\" text-anchor=\"middle\" font-family=\"'Segoe UI'\" font-size=\"10\" fill=\"#777\">© generated</text>");
            }

            sb.AppendLine("</svg>");
            return sb.ToString();

            static string Escape(string s) => System.Security.SecurityElement.Escape(s ?? "");
        }
    }
}