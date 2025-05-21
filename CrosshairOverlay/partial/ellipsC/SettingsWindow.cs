using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace CrosshairOverlay
{
    // Event handlers: EllipsC
    public partial class SettingsWindow
    {
        private void DecreaseUnrestrictedWidth_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.DecreaseUnrestrictedWidth(MultiplierIsChecked());
            UpdateUnrestrictedWidthValueText();
            SaveConfig();

            if (WH_Merge.IsChecked == true)
            {
                DecreaseUnrestrictedHeight_Click();
            }
        }

        private void DecreaseUnrestrictedWidth_Click()
        {
            ellipsC.DecreaseUnrestrictedWidth(MultiplierIsChecked());
            UpdateUnrestrictedWidthValueText();
            SaveConfig();
        }

        private void IncreaseUnrestrictedWidth_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.IncreaseUnrestrictedWidth(MultiplierIsChecked());
            UpdateUnrestrictedWidthValueText();
            SaveConfig();

            if (WH_Merge.IsChecked == true)
            {
                IncreaseUnrestrictedHeight_Click();
            }
        }

        private void IncreaseUnrestrictedWidth_Click()
        {
            ellipsC.IncreaseUnrestrictedWidth(MultiplierIsChecked());
            UpdateUnrestrictedWidthValueText();
            SaveConfig();
        }

        private void UpdateUnrestrictedWidthValueText()
        {
            UnrestrictedWidthValueText.Text = config.UnrestrictedWidth.ToString();
        }

        private void DecreaseUnrestrictedHeight_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.DecreaseUnrestrictedHeight(MultiplierIsChecked());
            UpdateUnrestrictedHeightValueText();
            SaveConfig();

            if (WH_Merge.IsChecked == true)
            {
                DecreaseUnrestrictedWidth_Click();
            }
        }

        private void DecreaseUnrestrictedHeight_Click()
        {
            ellipsC.DecreaseUnrestrictedHeight(MultiplierIsChecked());
            UpdateUnrestrictedHeightValueText();
            SaveConfig();
        }

        private void IncreaseUnrestrictedHeight_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.IncreaseUnrestrictedHeight(MultiplierIsChecked());
            UpdateUnrestrictedHeightValueText();
            SaveConfig();

            if (WH_Merge.IsChecked == true)
            {
                IncreaseUnrestrictedWidth_Click();
            }
        }

        private void IncreaseUnrestrictedHeight_Click()
        {
            ellipsC.IncreaseUnrestrictedHeight(MultiplierIsChecked());
            UpdateUnrestrictedHeightValueText();
            SaveConfig();
        }

        private void UpdateUnrestrictedHeightValueText()
        {
            UnrestrictedHeightValueText.Text = config.UnrestrictedHeight.ToString();
        }

        private void DecreaseUnrestrictedThickness_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.DecreaseUnrestrictedThickness(MultiplierIsChecked());
            UpdateUnrestrictedThicknessValueText();
            SaveConfig();
        }

        private void IncreaseUnrestrictedThickness_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.IncreaseUnrestrictedThickness(MultiplierIsChecked());
            UpdateUnrestrictedThicknessValueText();
            SaveConfig();
        }

        private void UpdateUnrestrictedThicknessValueText()
        {
            UnrestrictedThicknessValueText.Text = config.UnrestrictedTickness.ToString();
        }

        private void UnrestrictedColorPicker_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.UnrestrictedColorPicker();
            UpdateUnrestrictedColorValueText();
            setBackgroundUnrestrictedColorIndicatorButton();
            SaveConfig();
        }

        private void UpdateUnrestrictedColorValueText()
        {
            UnrestrictedColorValueText.Text = config.UnrestrictedColor.ToString();
        }

        private void setBackgroundUnrestrictedColorIndicatorButton()
        {
            UnrestrictedCrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(config.UnrestrictedColor);
        }

        private void DecreaseUnrestrictedOpacity_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.DecreaseUnrestrictedOpacity();
            UpdateUnrestrictedOpacityValueText();
            SaveConfig();
        }

        private void IncreaseUnrestrictedOpacity_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.IncreaseUnrestrictedOpacity();
            UpdateUnrestrictedOpacityValueText();
            SaveConfig();
        }

        private void UpdateUnrestrictedOpacityValueText()
        {
            UnrestrictedOpacityValueText.Text = config.UnrestrictedOpacity.ToString();
        }

        private void DecreaseUnrestrictedOffsetX_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.DecreaseUnrestrictedOffsetX(MultiplierIsChecked());
            UpdateUnrestrictedOffsetXValueText();
            SaveConfig();
        }

        private void IncreaseUnrestrictedOffsetX_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.IncreaseUnrestrictedOffsetX(MultiplierIsChecked());
            UpdateUnrestrictedOffsetXValueText();
            SaveConfig();
        }

        private void UpdateUnrestrictedOffsetXValueText()
        {
            UnrestrictedOffsetXValueText.Text = config.UnrestrictedOffsetX.ToString();
        }

        private void DecreaseUnrestrictedOffsetY_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.DecreaseUnrestrictedOffsetY(MultiplierIsChecked());
            UpdateUnrestrictedOffsetYValueText();
            SaveConfig();
        }

        private void IncreaseUnrestrictedOffsetY_Click(object sender, RoutedEventArgs e)
        {
            ellipsC.IncreaseUnrestrictedOffsetY(MultiplierIsChecked());
            UpdateUnrestrictedOffsetYValueText();
            SaveConfig();
        }

        private void UpdateUnrestrictedOffsetYValueText()
        {
            UnrestrictedOffsetYValueText.Text = config.UnrestrictedOffsetY.ToString();
        }
    }
}
