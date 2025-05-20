using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace CrosshairOverlay
{
    // Event handlers: EllipsA
    public partial class SettingsWindow
    {
        // Параметры Ellips A
        private int radius;
        private int thickness;
        private string strokeColor;
        private double strokeOpacity;

        // Параметры Ellips A
        private void IncreaseRadius_Click(object sender, RoutedEventArgs e)
        {
            if (radius < Limits.radius)
            {
                radius += MultiplierIsChecked();
                if (radius > Limits.radius) radius = Limits.radius;
                UpdateRadiusValueText();
                SaveConfig();
            }
        }

        private void DecreaseRadius_Click(object sender, RoutedEventArgs e)
        {
            if (radius > Limits.minRadius)
            {
                radius -= MultiplierIsChecked();
                if (radius < Limits.minRadius) radius = Limits.minRadius;
                UpdateRadiusValueText();
                SaveConfig();
            }
        }

        private void UpdateRadiusValueText()
        {
            RadiusValueText.Text = radius.ToString();
        }

        private void IncreaseThickness_Click(object sender, RoutedEventArgs e)
        {
            if (thickness < Limits.tickness)
            {
                thickness += 1;
                UpdateThicknessValueText();
                SaveConfig();
            }
        }

        private void DecreaseThickness_Click(object sender, RoutedEventArgs e)
        {
            if (thickness > Limits.minThickness)
            {
                thickness -= 1;
                UpdateThicknessValueText();
                SaveConfig();
            }
        }

        private void UpdateThicknessValueText()
        {
            ThicknessValueText.Text = thickness.ToString();
        }

        private void ColorPicker_Click(object sender, RoutedEventArgs e)
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    strokeColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    UpdateColorValueText();
                    setBackgroundCrosshairColorIndicatorButton(strokeColor);
                    SaveConfig();
                }
            }
        }

        private void UpdateColorValueText()
        {
            ColorValueText.Text = strokeColor.ToString();
        }

        private void setBackgroundCrosshairColorIndicatorButton(string strokeColor)
        {
            CrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(strokeColor);
        }

        private void IncreaseOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (strokeOpacity < Limits.maxOpacity)
            {
                strokeOpacity += 0.1;
                UpdateOpacityValueText();
                SaveConfig();
            }
        }

        private void DecreaseOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (strokeOpacity > Limits.minOpacity)
            {
                strokeOpacity -= 0.1;
                UpdateOpacityValueText();
                SaveConfig();
            }
        }

        private void UpdateOpacityValueText()
        {
            CrosshairOpacity.Text = strokeOpacity.ToString();
        }
    }
}
