using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace CrosshairOverlay
{
    // Event handlers: EllipsB
    public partial class SettingsWindow
    {
        // Параметры Ellips B
        private int outlineRadius;
        private int outlineThickness;
        private string outlineColor;
        private double outlineOpacity;
        private int outlineOffsetX;
        private int outlineOffsetY;

        // Параметры Ellips B
        private void IncreaseOutlineRadius_Click(object sender, RoutedEventArgs e)
        {
            if (outlineRadius < Limits.GetOutlineMaxRadius(outlineThickness))
            {
                outlineRadius += MultiplierIsChecked();
                if (outlineRadius > Limits.GetOutlineMaxRadius(outlineThickness)) outlineRadius = Limits.GetOutlineMaxRadius(outlineThickness);
                UpdateOutlineRadiusValueText();
                SaveConfig();
            }
        }

        private void DecreaseOutlineRadius_Click(object sender, RoutedEventArgs e)
        {
            if (outlineRadius > Limits.minRadius)
            {
                outlineRadius -= MultiplierIsChecked();
                if (outlineRadius < Limits.minRadius) outlineRadius = Limits.minRadius;
                UpdateOutlineRadiusValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineRadiusValueText()
        {
            OutlineRadiusValueText.Text = outlineRadius.ToString();
        }

        private void IncreaseOutlineThickness_Click(object sender, RoutedEventArgs e)
        {
            if (outlineThickness < Limits.outlineThickness && outlineRadius < Limits.GetOutlineMaxRadius(outlineThickness))
            {
                outlineThickness += 1;
                UpdateOutlineThicknessValueText();
                SaveConfig();
            }
        }

        private void DecreaseOutlineThickness_Click(object sender, RoutedEventArgs e)
        {
            if (outlineThickness > Limits.minThickness)
            {
                outlineThickness -= 1;
                UpdateOutlineThicknessValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineThicknessValueText()
        {
            OutlineThicknessValueText.Text = outlineThickness.ToString();
        }

        private void OutlineColorPicker_Click(object sender, RoutedEventArgs e)
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    outlineColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    UpdateOutlineColorValueText();
                    setBackgroundOutlineCrosshairColorIndicatorButton(outlineColor);
                    SaveConfig();
                }
            }
        }

        private void setBackgroundOutlineCrosshairColorIndicatorButton(string outlineColor)
        {
            OutlineCrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(outlineColor);
        }

        private void UpdateOutlineColorValueText()
        {
            OutlineColorValueText.Text = outlineColor.ToString();
        }

        private void IncreaseOutlineOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOpacity < Limits.maxOpacity)
            {
                outlineOpacity += 0.1;
                UpdateOutlineOpacityValueText();
                SaveConfig();
            }
        }

        private void DecreaseOutlineOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOpacity > Limits.minOpacity)
            {
                outlineOpacity -= 0.1;
                UpdateOutlineOpacityValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineOpacityValueText()
        {
            OutlineCrosshairOpacity.Text = outlineOpacity.ToString();
        }

        private void DecreaseOutlineOffsetX_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOffsetX > -Limits.offsetX)
            {
                outlineOffsetX -= MultiplierIsChecked();
                if (outlineOffsetX < -Limits.offsetX) outlineOffsetX = -Limits.offsetX;
                UpdateOutlineOffsetXValueText();
                SaveConfig();
            }
        }

        private void IncreaseOutlineOffsetX_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOffsetX < Limits.offsetX)
            {
                outlineOffsetX += MultiplierIsChecked();
                if (outlineOffsetX > Limits.offsetX) outlineOffsetX = Limits.offsetX;
                UpdateOutlineOffsetXValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineOffsetXValueText()
        {
            OutlineCrosshairOffsetX.Text = outlineOffsetX.ToString();
        }

        private void DecreaseOutlineOffsetY_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOffsetY > -Limits.offsetY)
            {
                outlineOffsetY -= MultiplierIsChecked();
                if (outlineOffsetY < -Limits.offsetY) outlineOffsetY = -Limits.offsetY;
                UpdateOutlineOffsetYValueText();
                SaveConfig();
            }
        }

        private void IncreaseOutlineOffsetY_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOffsetY < Limits.offsetY)
            {
                outlineOffsetY += MultiplierIsChecked();
                if (outlineOffsetY > Limits.offsetY) outlineOffsetY = Limits.offsetY;
                UpdateOutlineOffsetYValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineOffsetYValueText()
        {
            OutlineCrosshairOffsetY.Text = outlineOffsetY.ToString();
        }
    }
}
