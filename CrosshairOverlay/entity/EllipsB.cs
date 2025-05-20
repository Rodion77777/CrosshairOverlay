using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosshairOverlay.entity
{
    internal class EllipsB
    {
        private int outlineRadius;
        private int outlineThickness;
        private string outlineColor;
        private double outlineOpacity;
        private int outlineOffsetX;
        private int outlineOffsetY;
        private CrosshairConfig config;

        public EllipsB(CrosshairConfig config)
        {
            this.config = config;
            this.outlineRadius = config.OutlineRadius;
            this.outlineThickness = config.OutlineThickness;
            this.outlineColor = config.OutlineColor;
            this.outlineOpacity = config.OutlineOpacity;
            this.outlineOffsetX = config.OutlineOffsetX;
            this.outlineOffsetY = config.OutlineOffsetY;
        }

        public void IncreaseOutlineRadius(int increaseValue)
        {
            if (outlineRadius < Limits.GetOutlineMaxRadius(outlineThickness))
            {
                outlineRadius += increaseValue;
                if (outlineRadius > Limits.GetOutlineMaxRadius(outlineThickness))
                    outlineRadius = Limits.GetOutlineMaxRadius(outlineThickness);
                config.OutlineRadius = outlineRadius;
            }
        }

        public void DecreaseOutlineRadius(int decreaseValue)
        {
            if (outlineRadius > Limits.minRadius)
            {
                outlineRadius -= decreaseValue;
                if (outlineRadius < Limits.minRadius)
                    outlineRadius = Limits.minRadius;
                config.OutlineRadius = outlineRadius;
            }
        }

        public void IncreaseOutlineThickness()
        {
            if (outlineThickness < Limits.outlineThickness && outlineRadius < Limits.GetOutlineMaxRadius(outlineThickness))
            {
                outlineThickness += 1;
                config.OutlineThickness = outlineThickness;
            }
        }

        public void DecreaseOutlineThickness()
        {
            if (outlineThickness > Limits.minThickness)
            {
                outlineThickness -= 1;
                config.OutlineThickness = outlineThickness;
            }
        }

        public void OutlineColorPicker()
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    outlineColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    config.OutlineColor = outlineColor;
                }
            }
        }

        public void IncreaseOutlineOpacity()
        {
            if (outlineOpacity < Limits.maxOpacity)
            {
                outlineOpacity += 0.1;
                config.OutlineOpacity = outlineOpacity;
            }
        }

        public void DecreaseOutlineOpacity()
        {
            if (outlineOpacity > Limits.minOpacity)
            {
                outlineOpacity -= 0.1;
                config.OutlineOpacity = outlineOpacity;
            }
        }

        public void IncreaseOutlineOffsetX(int increaseValue)
        {
            if (outlineOffsetX < Limits.offsetX)
            {
                outlineOffsetX += increaseValue;
                if (outlineOffsetX > Limits.offsetX)
                    outlineOffsetX = Limits.offsetX;
                config.OutlineOffsetX = outlineOffsetX;
            }
        }

        public void DecreaseOutlineOffsetX(int decreaseValue)
        {
            if (outlineOffsetX > -Limits.offsetX)
            {
                outlineOffsetX -= decreaseValue;
                if (outlineOffsetX < -Limits.offsetX)
                    outlineOffsetX = -Limits.offsetX;
                config.OutlineOffsetX = outlineOffsetX;
            }
        }

        public void IncreaseOutlineOffsetY(int increaseValue)
        {
            if (outlineOffsetY < Limits.offsetY)
            {
                outlineOffsetY += increaseValue;
                if (outlineOffsetY > Limits.offsetY)
                    outlineOffsetY = Limits.offsetY;
                config.OutlineOffsetY = outlineOffsetY;
            }
        }

        public void DecreaseOutlineOffsetY(int decreaseValue)
        {
            if (outlineOffsetY > -Limits.offsetY)
            {
                outlineOffsetY -= decreaseValue;
                if (outlineOffsetY < -Limits.offsetY)
                    outlineOffsetY = -Limits.offsetY;
                config.OutlineOffsetY = outlineOffsetY;
            }
        }
    }
}
