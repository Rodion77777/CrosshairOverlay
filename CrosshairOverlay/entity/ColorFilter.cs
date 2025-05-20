using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrosshairOverlay.entity
{

    internal class ColorFilter
    {
        private int filterSize;
        private string filterColor;
        private double filterOpacity;
        private CrosshairConfig config;

        public ColorFilter(CrosshairConfig config)
        {
            this.config = config;
            this.filterSize = config.FilterSize;
            this.filterColor = config.FilterColor;
            this.filterOpacity = config.FilterOpacity;
        }

        public void IncreaseFilterSize(int increaseValue)
        {
            if (filterSize < Limits.maxFilterSize)
            {
                filterSize += increaseValue;
                if (filterSize > Limits.maxFilterSize) filterSize = Limits.maxFilterSize;
                config.FilterSize = filterSize;
            }
        }

        public void DecreaseFilterSize(int decreaseValue)
        {
            if (filterSize > Limits.minRadius)
            {
                filterSize -= decreaseValue;
                if (filterSize < Limits.minRadius) filterSize = Limits.minRadius;
                config.FilterSize = filterSize;
            }
        }

        public void FilterColorPicker()
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filterColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    config.FilterColor = filterColor;
                }
            }
        }

        public void IncreaseFilterOpacity()
        {
            if (filterOpacity < Limits.maxOpacity)
            {
                filterOpacity += 0.1;
                config.FilterOpacity = filterOpacity;
            }
        }

        public void DecreaseFilterOpacity()
        {
            if (filterOpacity > Limits.minOpacity)
            {
                filterOpacity -= 0.1;
                config.FilterOpacity = filterOpacity;
            }
        }
    }
}