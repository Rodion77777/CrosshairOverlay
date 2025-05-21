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
        private void IncreaseOutlineRadius_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.IncreaseOutlineRadius(MultiplierIsChecked());
            UpdateOutlineRadiusValueText();
            SaveConfig();
        }

        private void DecreaseOutlineRadius_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.DecreaseOutlineRadius(MultiplierIsChecked());
            UpdateOutlineRadiusValueText();
            SaveConfig();
        }

        private void UpdateOutlineRadiusValueText()
        {
            OutlineRadiusValueText.Text = config.OutlineRadius.ToString();
        }

        private void IncreaseOutlineThickness_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.IncreaseOutlineThickness();
            UpdateOutlineThicknessValueText();
            SaveConfig();
        }

        private void DecreaseOutlineThickness_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.DecreaseOutlineThickness();
            UpdateOutlineThicknessValueText();
            SaveConfig();
        }

        private void UpdateOutlineThicknessValueText()
        {
            OutlineThicknessValueText.Text = config.OutlineThickness.ToString();
        }

        private void OutlineColorPicker_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.OutlineColorPicker();
            UpdateOutlineColorValueText();
            setBackgroundOutlineCrosshairColorIndicatorButton();
            SaveConfig();
        }

        private void setBackgroundOutlineCrosshairColorIndicatorButton()
        {
            OutlineCrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(config.OutlineColor);
        }

        private void UpdateOutlineColorValueText()
        {
            OutlineColorValueText.Text = config.OutlineColor.ToString();
        }

        private void IncreaseOutlineOpacity_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.IncreaseOutlineOpacity();
            UpdateOutlineOpacityValueText();
            SaveConfig();
        }

        private void DecreaseOutlineOpacity_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.DecreaseOutlineOpacity();
            UpdateOutlineOpacityValueText();
            SaveConfig();
        }

        private void UpdateOutlineOpacityValueText()
        {
            OutlineCrosshairOpacity.Text = config.OutlineOpacity.ToString();
        }

        private void DecreaseOutlineOffsetX_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.DecreaseOutlineOffsetX(MultiplierIsChecked());
            UpdateOutlineOffsetXValueText();
            SaveConfig();
        }

        private void IncreaseOutlineOffsetX_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.IncreaseOutlineOffsetX(MultiplierIsChecked());
            UpdateOutlineOffsetXValueText();
            SaveConfig();
        }

        private void UpdateOutlineOffsetXValueText()
        {
            OutlineCrosshairOffsetX.Text = config.OutlineOffsetX.ToString();
        }

        private void DecreaseOutlineOffsetY_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.DecreaseOutlineOffsetY(MultiplierIsChecked());
            UpdateOutlineOffsetYValueText();
            SaveConfig();
        }

        private void IncreaseOutlineOffsetY_Click(object sender, RoutedEventArgs e)
        {
            ellipsB.IncreaseOutlineOffsetY(MultiplierIsChecked());
            UpdateOutlineOffsetYValueText();
            SaveConfig();
        }

        private void UpdateOutlineOffsetYValueText()
        {
            OutlineCrosshairOffsetY.Text = config.OutlineOffsetY.ToString();
        }
    }
}
