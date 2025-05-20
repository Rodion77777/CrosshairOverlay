using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using CrosshairOverlay.entity;

namespace CrosshairOverlay
{
    // Event handlers: EllipsA
    public partial class SettingsWindow
    {
        private void IncreaseRadius_Click(object sender, RoutedEventArgs e)
        {
            ellipsA.IncreaseRadius(MultiplierIsChecked());
            UpdateRadiusValueText();
            SaveConfig();
        }

        private void DecreaseRadius_Click(object sender, RoutedEventArgs e)
        {
            ellipsA.DecreaseRadius(MultiplierIsChecked());
            UpdateRadiusValueText();
            SaveConfig();
        }

        private void UpdateRadiusValueText()
        {
            RadiusValueText.Text = config.Radius.ToString();
        }

        private void IncreaseThickness_Click(object sender, RoutedEventArgs e)
        {
            ellipsA.IncreaseThickness();
            UpdateThicknessValueText();
            SaveConfig();
        }

        private void DecreaseThickness_Click(object sender, RoutedEventArgs e)
        {
            ellipsA.DecreaseThickness();
            UpdateThicknessValueText();
            SaveConfig();
        }

        private void UpdateThicknessValueText()
        {
            ThicknessValueText.Text = config.Thickness.ToString();
        }

        private void ColorPicker_Click(object sender, RoutedEventArgs e)
        {
            ellipsA.ColorPicker();
            UpdateColorValueText();
            setBackgroundCrosshairColorIndicatorButton();
            SaveConfig();
        }

        private void UpdateColorValueText()
        {
            ColorValueText.Text = config.StrokeColor.ToString();
        }

        private void setBackgroundCrosshairColorIndicatorButton()
        {
            CrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(config.StrokeColor);
        }

        private void IncreaseOpacity_Click(object sender, RoutedEventArgs e)
        {
            ellipsA.IncreaseOpacity();
            UpdateOpacityValueText();
            SaveConfig();
        }

        private void DecreaseOpacity_Click(object sender, RoutedEventArgs e)
        {
            ellipsA.DecreaseOpacity();
            UpdateOpacityValueText();
            SaveConfig();
        }

        private void UpdateOpacityValueText()
        {
            CrosshairOpacity.Text = config.StrokeOpacity.ToString();
        }
    }
}
