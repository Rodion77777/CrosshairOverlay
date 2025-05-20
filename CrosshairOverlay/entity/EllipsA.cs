using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrosshairOverlay.entity
{
    internal class EllipsA
    {
        private int radius;
        private int thickness;
        private string strokeColor;
        private double strokeOpacity;
        private CrosshairConfig config;

        public EllipsA(CrosshairConfig config)
        {
            this.config = config;
            this.radius = config.Radius;
            this.thickness = config.Thickness;
            this.strokeColor = config.StrokeColor;
            this.strokeOpacity = config.StrokeOpacity;
        }

        public void IncreaseRadius(int increaseValue)
        {
            if (radius < Limits.radius)
            {
                radius += increaseValue;
                if (radius > Limits.radius) radius = Limits.radius;
                config.Radius = radius;
            }
        }

        public void DecreaseRadius(int increaseValue)
        {
            if (radius > Limits.minRadius)
            {
                radius -= increaseValue;
                if (radius < Limits.minRadius) radius = Limits.minRadius;
                config.Radius = radius;
            }
        }

        public void IncreaseThickness()
        {
            if (thickness < Limits.tickness)
            {
                thickness += 1;
                config.Thickness = thickness;
            }
        }

        public void DecreaseThickness()
        {
            if (thickness > Limits.minThickness)
            {
                thickness -= 1;
                config.Thickness -= thickness;
            }
        }

        public void ColorPicker()
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    strokeColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    config.FilterColor = strokeColor;
                }
            }
        }

        public void IncreaseOpacity()
        {
            if (strokeOpacity < Limits.maxOpacity)
            {
                strokeOpacity += 0.1;
                config.StrokeOpacity = strokeOpacity;
            }
        }

        public void DecreaseOpacity_Click()
        {
            if (strokeOpacity > Limits.minOpacity)
            {
                strokeOpacity -= 0.1;
                config.StrokeOpacity = strokeOpacity;
            }
        }
    }
}
