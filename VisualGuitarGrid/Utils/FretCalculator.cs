using System;

namespace VisualGuitarGrid.Utils
{
    public static class FretCalculator
    {
        // Real physics: distance from nut to fret n = scaleLength - (scaleLength / 2^(n/12))
        // We will compute positions in pixels by mapping scaleLengthPx to the drawable width.
        // Returns an array of x offsets from the nut for frets 0..fretsCount (0 is nut = 0).
        public static double[] GetFretPositions(int fretsCount, double scaleLengthPx)
        {
            var xs = new double[fretsCount + 1];
            for (int n = 0; n <= fretsCount; n++)
            {
                if (n == 0)
                {
                    xs[n] = 0.0; // nut at 0
                }
                else
                {
                    // distance from nut to fret n:
                    double d = scaleLengthPx - (scaleLengthPx / Math.Pow(2.0, n / 12.0));
                    xs[n] = d;
                }
            }
            return xs;
        }

        // Map the computed positions to screen coordinates between left and right (leftMargin..leftMargin+width)
        public static int[] MapToScreen(double[] fretPositions, int leftMargin, int width)
        {
            double scale = 1.0;
            if (fretPositions.Length > 1)
            {
                double max = fretPositions[fretPositions.Length - 1];
                if (max > 0) scale = width / max;
            }

            var xs = new int[fretPositions.Length];
            for (int i = 0; i < fretPositions.Length; i++)
                xs[i] = leftMargin + (int)Math.Round(fretPositions[i] * scale);
            return xs;
        }
    }
}