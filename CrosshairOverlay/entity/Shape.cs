using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosshairOverlay.entity
{
    internal class Shape
    {
        private int width;
        private int height;
        private int tickness;
        private string color;
        private double opacity;
        private int offsetX;
        private int offsetY;
        private CrosshairConfig config;

        public Shape(CrosshairConfig config)
        {
            this.config = config;
            this.width = config.UnrestrictedWidth;
            this.height = config.UnrestrictedHeight;
            this.tickness = config.UnrestrictedTickness;
            this.color = config.UnrestrictedColor;
            this.opacity = config.UnrestrictedOpacity;
            this.offsetX = config.UnrestrictedOffsetX;
            this.offsetY = config.UnrestrictedOffsetY;
        }

        public void DecreaseWidth(int decreaseValue)
        {
            if (width > 0)
            {
                width -= decreaseValue;
                if (width < 0) width = 0;
                config.UnrestrictedWidth = width;
            }
        }

        public void IncreaseWidth(int increaseValue)
        {
            if (width < Limits.GetUnrestrictedWidth(width))
            {
                width += increaseValue;
                if (width > Limits.GetUnrestrictedWidth(tickness))
                    width = Limits.GetUnrestrictedWidth(tickness);
                config.UnrestrictedWidth = width;
            }
        }

        public void DecreaseHeight(int decreaseValue)
        {
            if (height > 0)
            {
                height -= decreaseValue;
                if (height < 0) height = 0;
                config.UnrestrictedHeight = height;
            }
        }

        public void IncreaseUnrestrictedHeight(int increaseValue)
        {
            if (height < Limits.GetUnrestrictedHeight(tickness))
            {
                height += increaseValue;
                if (height > Limits.GetUnrestrictedHeight(tickness))
                    height = Limits.GetUnrestrictedHeight(tickness);
                config.UnrestrictedHeight = height;
            }
        }

        public void DecreaseUnrestrictedThickness(int decreaseValue)
        {
            if (tickness > 0)
            {
                tickness -= decreaseValue;
                if (tickness < 0) tickness = 0;
                config.UnrestrictedTickness = tickness;
            }
        }

        public void IncreaseUnrestrictedThickness(int increaseValue)
        {
            if (tickness < Limits.unrestrictedThickness)
            {
                tickness += increaseValue;
                if (tickness > Limits.unrestrictedThickness)
                    tickness = Limits.unrestrictedThickness;
                config.UnrestrictedTickness = tickness;
            }
        }

        public void UnrestrictedColorPicker()
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    color = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    config.UnrestrictedColor = color;
                }
            }
        }

        public void DecreaseUnrestrictedOpacity()
        {
            if (opacity > Limits.minOpacity)
            {
                opacity -= 0.1;
                config.UnrestrictedOpacity = opacity;
            }
        }

        public void IncreaseUnrestrictedOpacity()
        {
            if (opacity < Limits.maxOpacity)
            {
                opacity += 0.1;
                config.UnrestrictedOpacity = opacity;
            }
        }

        public void DecreaseUnrestrictedOffsetX(int decreaseValue)
        {
            int limitX = -Limits.GetUnrestrictedOffsetX(width, tickness);
            if (offsetX > limitX)
            {
                offsetX -= decreaseValue;
                if (offsetX < limitX) offsetX = limitX;
                config.UnrestrictedOffsetX = offsetX;
            }
        }

        public void IncreaseUnrestrictedOffsetX(int increaseValue)
        {
            int limitX = Limits.GetUnrestrictedOffsetX(width, tickness);
            if (offsetX < limitX)
            {
                offsetX += increaseValue;
                if (offsetX > limitX) offsetX = limitX;
                config.UnrestrictedOffsetX = offsetX;
            }
        }

        public void DecreaseUnrestrictedOffsetY(int decreaseValue)
        {
            int limitY = -Limits.GetUnrestrictedOffsetY(height, tickness);
            if (offsetY > limitY)
            {
                offsetY -= decreaseValue;
                if (offsetY < limitY) offsetY = limitY;
                config.UnrestrictedOffsetY = offsetY;
            }
        }

        public void IncreaseUnrestrictedOffsetY(int increaseValue)
        {
            int limitY = Limits.GetUnrestrictedOffsetY(height, tickness);
            if (offsetY < limitY)
            {
                offsetY += increaseValue;
                if (offsetY > limitY) offsetY = limitY;
                config.UnrestrictedOffsetY = offsetY;
            }
        }
    }
}
