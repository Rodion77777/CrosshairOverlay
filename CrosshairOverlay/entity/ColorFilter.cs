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
            this.filterSize = 0;
            this.filterColor = "#000000";
            this.filterOpacity = 0.1;
        }

        public int IncreaseFilterSize(int increaseValue)
        {
            if (filterSize < Limits.maxFilterSize)
            {
                filterSize += increaseValue;
                if (filterSize > Limits.maxFilterSize) filterSize = Limits.maxFilterSize;
                // Set the filter size in the config
            }
            return filterSize;
        }

        public int DecreaseFilterSize(int decreaseValue)
        {
            if (filterSize > Limits.minRadius)
            {
                filterSize -= decreaseValue;
                if (filterSize < Limits.minRadius) filterSize = Limits.minRadius;
                // Set the filter size in the config
            }
            return filterSize;
        }

        public string FilterColorPicker()
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filterColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    // Set the filter size in the config
                }
            }
            return filterColor;
        }

        public double IncreaseFilterOpacity()
        {
            if (filterOpacity < Limits.maxOpacity)
            {
                filterOpacity += 0.1;
                // Set the filter size in the config
            }
            return filterOpacity;
        }

        public double DecreaseFilterOpacity()
        {
            if (filterOpacity > Limits.minOpacity)
            {
                filterOpacity -= 0.1;
                // Set the filter size in the config
            }
            return filterOpacity;
        }
    }
}