using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrosshairOverlay.entity
{
    internal class EllipsC
    {
        private int unrestrictedWidth;
        private int unrestrictedHeight;
        private int unrestrictedTickness;
        private string unrestrictedColor;
        private double unrestrictedOpacity;
        private int unrestrictedOffsetX;
        private int unrestrictedOffsetY;
        private CrosshairConfig config;

        public EllipsC(CrosshairConfig config)
        {
            this.config = config;
            this.unrestrictedWidth = config.UnrestrictedWidth;
            this.unrestrictedHeight = config.UnrestrictedHeight;
            this.unrestrictedTickness = config.UnrestrictedTickness;
            this.unrestrictedColor = config.UnrestrictedColor;
            this.unrestrictedOpacity = config.UnrestrictedOpacity;
            this.unrestrictedOffsetX = config.UnrestrictedOffsetX;
            this.unrestrictedOffsetY = config.UnrestrictedOffsetY;
        }

        public void DecreaseUnrestrictedWidth(int decreaseValue)
        {
            if (unrestrictedWidth > 0)
            {
                unrestrictedWidth -= decreaseValue;
                if (unrestrictedWidth < 0) unrestrictedWidth = 0;
                config.UnrestrictedWidth = unrestrictedWidth;
            }
        }

        public void IncreaseUnrestrictedWidth(int increaseValue)
        {
            if (unrestrictedWidth < Limits.GetUnrestrictedWidth(unrestrictedTickness))
            {
                unrestrictedWidth += increaseValue;
                if (unrestrictedWidth > Limits.GetUnrestrictedWidth(unrestrictedTickness))
                    unrestrictedWidth = Limits.GetUnrestrictedWidth(unrestrictedTickness);
                config.UnrestrictedWidth = unrestrictedWidth;
            }
        }

        public void DecreaseUnrestrictedHeight(int decreaseValue)
        {
            if (unrestrictedHeight > 0)
            {
                unrestrictedHeight -= decreaseValue;
                if (unrestrictedHeight < 0) unrestrictedHeight = 0;
                config.UnrestrictedHeight = unrestrictedHeight;
            }
        }

        public void IncreaseUnrestrictedHeight(int increaseValue)
        {
            if (unrestrictedHeight < Limits.GetUnrestrictedHeight(unrestrictedTickness))
            {
                unrestrictedHeight += increaseValue;
                if (unrestrictedHeight > Limits.GetUnrestrictedHeight(unrestrictedTickness))
                    unrestrictedHeight = Limits.GetUnrestrictedHeight(unrestrictedTickness);
                config.UnrestrictedHeight = unrestrictedHeight;
            }
        }

        public void DecreaseUnrestrictedThickness(int decreaseValue)
        {
            if (unrestrictedTickness > 0)
            {
                unrestrictedTickness -= decreaseValue;
                if (unrestrictedTickness < 0) unrestrictedTickness = 0;
                config.UnrestrictedTickness = unrestrictedTickness;
            }
        }

        public void IncreaseUnrestrictedThickness(int increaseValue)
        {
            if (unrestrictedTickness < Limits.unrestrictedThickness)
            {
                unrestrictedTickness += increaseValue;
                if (unrestrictedTickness > Limits.unrestrictedThickness)
                    unrestrictedTickness = Limits.unrestrictedThickness;
                config.UnrestrictedTickness = unrestrictedTickness;
            }
        }

        public void UnrestrictedColorPicker()
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    unrestrictedColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    config.UnrestrictedColor = unrestrictedColor;
                }
            }
        }

        public void DecreaseUnrestrictedOpacity()
        {
            if (unrestrictedOpacity > Limits.minOpacity)
            {
                unrestrictedOpacity -= 0.1;
                config.UnrestrictedOpacity = unrestrictedOpacity;
            }
        }

        public void IncreaseUnrestrictedOpacity()
        {
            if (unrestrictedOpacity < Limits.maxOpacity)
            {
                unrestrictedOpacity += 0.1;
                config.UnrestrictedOpacity = unrestrictedOpacity;
            }
        }

        public void DecreaseUnrestrictedOffsetX(int decreaseValue)
        {
            int limitX = -Limits.GetUnrestrictedOffsetX(unrestrictedWidth, unrestrictedTickness);
            if (unrestrictedOffsetX > limitX)
            {
                unrestrictedOffsetX -= decreaseValue;
                if (unrestrictedOffsetX < limitX) unrestrictedOffsetX = limitX;
                config.UnrestrictedOffsetX = unrestrictedOffsetX;
            }
        }

        public void IncreaseUnrestrictedOffsetX(int increaseValue)
        {
            int limitX = Limits.GetUnrestrictedOffsetX(unrestrictedWidth, unrestrictedTickness);
            if (unrestrictedOffsetX < limitX)
            {
                unrestrictedOffsetX += increaseValue;
                if (unrestrictedOffsetX > limitX) unrestrictedOffsetX = limitX;
                config.UnrestrictedOffsetX = unrestrictedOffsetX;
            }
        }

        public void DecreaseUnrestrictedOffsetY(int decreaseValue)
        {
            int limitY = -Limits.GetUnrestrictedOffsetY(unrestrictedHeight, unrestrictedTickness);
            if (unrestrictedOffsetY > limitY)
            {
                unrestrictedOffsetY -= decreaseValue;
                if (unrestrictedOffsetY < limitY) unrestrictedOffsetY = limitY;
                config.UnrestrictedOffsetY = unrestrictedOffsetY;
            }
        }

        public void IncreaseUnrestrictedOffsetY(int increaseValue)
        {
            int limitY = Limits.GetUnrestrictedOffsetY(unrestrictedHeight, unrestrictedTickness);
            if (unrestrictedOffsetY < limitY)
            {
                unrestrictedOffsetY += increaseValue;
                if (unrestrictedOffsetY > limitY) unrestrictedOffsetY = limitY;
                config.UnrestrictedOffsetY = unrestrictedOffsetY;
            }
        }
    }
}
