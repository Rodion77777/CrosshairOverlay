using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CrosshairOverlay
{
    internal static class ColorUtil
    {
        public static System.Windows.Media.Color Lighten(System.Windows.Media.Color color, double factor)
        {
            factor = Math.Max(0, Math.Min(1, factor));
            byte r = (byte)Math.Min(255, color.R + (255 - color.R) * factor);
            byte g = (byte)Math.Min(255, color.G + (255 - color.G) * factor);
            byte b = (byte)Math.Min(255, color.B + (255 - color.B) * factor);
            return System.Windows.Media.Color.FromRgb(r, g, b);
        }

        public static System.Windows.Media.Color Darken(System.Windows.Media.Color color, double factor)
        {
            factor = Math.Max(0, Math.Min(1, factor));
            byte r = (byte)(color.R * (1 - factor));
            byte g = (byte)(color.G * (1 - factor));
            byte b = (byte)(color.B * (1 - factor));
            return System.Windows.Media.Color.FromRgb(r, g, b);
        }

        public static System.Windows.Media.Color GetComplementaryColor(System.Windows.Media.Color input)
        {
            return System.Windows.Media.Color.FromRgb(
                (byte)(255 - input.R),
                (byte)(255 - input.G),
                (byte)(255 - input.B)
            );
        }

        public static System.Windows.Media.Color GetContrastingColor(System.Windows.Media.Color input)
        {
            // Calculate the brightness of the color
            double brightness = (input.R * 299 + input.G * 587 + input.B * 114) / 1000;
            // Return black or white based on brightness
            return brightness > 128 ? Colors.Black : Colors.White;
        }

        public static System.Drawing.Color GetDominantScreenColor()
        {
            Rectangle bounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
            }

            var pixelList = new List<System.Drawing.Color>();
            int step = 20;

            for (int y = 0; y < screenshot.Height; y += step)
            {
                for (int x = 0; x < screenshot.Width; x += step)
                {
                    System.Drawing.Color pixelColor = screenshot.GetPixel(x, y);
                    if (pixelColor.A > 0) // Ignore fully transparent pixels
                    {
                        pixelList.Add(pixelColor);
                    }
                }
            }

            int avgR = (int)pixelList.Average(c => c.R);
            int avgG = (int)pixelList.Average(c => c.G);
            int avgB = (int)pixelList.Average(c => c.B);

            screenshot.Dispose();

            return System.Drawing.Color.FromArgb(avgR, avgG, avgB);
        }
    }
}
