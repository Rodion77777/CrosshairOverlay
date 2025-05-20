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
        // Параметры Ellips C
        private int unrestrictedWidth;
        private int unrestrictedHeight;
        private int unrestrictedTickness;
        private string unrestrictedColor;
        private double unrestrictedOpacity;
        private int unrestrictedOffsetX;
        private int unrestrictedOffsetY;

        // Параметры Ellips C
        private void DecreaseUnrestrictedWidth_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedWidth > 0)
            {
                unrestrictedWidth -= MultiplierIsChecked();
                if (unrestrictedWidth < 0) unrestrictedWidth = 0;
                UpdateUnrestrictedWidthValueText();
                SaveConfig();
            }

            if (WH_Merge.IsChecked == true)
            {
                DecreaseUnrestrictedHeight_Click();
            }
        }

        private void DecreaseUnrestrictedWidth_Click()
        {
            if (unrestrictedWidth > 0)
            {
                unrestrictedWidth -= MultiplierIsChecked();
                if (unrestrictedWidth < 0) unrestrictedWidth = 0;
                UpdateUnrestrictedWidthValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedWidth_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedWidth < Limits.GetUnrestrictedWidth(unrestrictedTickness))
            {
                unrestrictedWidth += MultiplierIsChecked();
                UpdateUnrestrictedWidthValueText();
                SaveConfig();
            }

            if (WH_Merge.IsChecked == true)
            {
                IncreaseUnrestrictedHeight_Click();
            }
        }

        private void IncreaseUnrestrictedWidth_Click()
        {
            if (unrestrictedWidth < Limits.GetUnrestrictedWidth(unrestrictedTickness))
            {
                unrestrictedWidth += MultiplierIsChecked();
                UpdateUnrestrictedWidthValueText();
                SaveConfig();
            }
        }

        private void UpdateUnrestrictedWidthValueText()
        {
            UnrestrictedWidthValueText.Text = unrestrictedWidth.ToString();
        }

        private void DecreaseUnrestrictedHeight_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedHeight > 0)
            {
                unrestrictedHeight -= MultiplierIsChecked();
                if (unrestrictedHeight < 0) unrestrictedHeight = 0;
                UpdateUnrestrictedHeightValueText();
                SaveConfig();
            }

            if (WH_Merge.IsChecked == true)
            {
                DecreaseUnrestrictedWidth_Click();
            }
        }

        private void DecreaseUnrestrictedHeight_Click()
        {
            if (unrestrictedHeight > 0)
            {
                unrestrictedHeight -= MultiplierIsChecked();
                if (unrestrictedHeight < 0) unrestrictedHeight = 0;
                UpdateUnrestrictedHeightValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedHeight_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedHeight < Limits.GetUnrestrictedHeight(unrestrictedTickness))
            {
                unrestrictedHeight += MultiplierIsChecked();
                UpdateUnrestrictedHeightValueText();
                SaveConfig();
            }

            if (WH_Merge.IsChecked == true)
            {
                IncreaseUnrestrictedWidth_Click();
            }
        }

        private void IncreaseUnrestrictedHeight_Click()
        {
            if (unrestrictedHeight < Limits.GetUnrestrictedHeight(unrestrictedTickness))
            {
                unrestrictedHeight += MultiplierIsChecked();
                UpdateUnrestrictedHeightValueText();
                SaveConfig();
            }
        }

        private void UpdateUnrestrictedHeightValueText()
        {
            UnrestrictedHeightValueText.Text = unrestrictedHeight.ToString();
        }

        private void DecreaseUnrestrictedThickness_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedTickness > 0)
            {
                unrestrictedTickness -= MultiplierIsChecked();
                if (unrestrictedTickness < 0) unrestrictedTickness = 0;
                UpdateUnrestrictedThicknessValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedThickness_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedTickness < Limits.unrestrictedThickness)
            {
                unrestrictedTickness += MultiplierIsChecked();
                if (unrestrictedTickness > Limits.unrestrictedThickness) unrestrictedTickness = Limits.unrestrictedThickness;
                UpdateUnrestrictedThicknessValueText();
                SaveConfig();
            }
        }

        private void UpdateUnrestrictedThicknessValueText()
        {
            UnrestrictedThicknessValueText.Text = unrestrictedTickness.ToString();
        }

        private void UnrestrictedColorPicker_Click(object sender, RoutedEventArgs e)
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    unrestrictedColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    UpdateUnrestrictedColorValueText();
                    setBackgroundUnrestrictedColorIndicatorButton(unrestrictedColor);
                    SaveConfig();
                }
            }
        }

        private void UpdateUnrestrictedColorValueText()
        {
            UnrestrictedColorValueText.Text = unrestrictedColor.ToString();
        }

        private void setBackgroundUnrestrictedColorIndicatorButton(string unrestrictedColor)
        {
            UnrestrictedCrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(unrestrictedColor);
        }

        private void DecreaseUnrestrictedOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedOpacity > Limits.minOpacity)
            {
                unrestrictedOpacity -= 0.1;
                UpdateUnrestrictedOpacityValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedOpacity < Limits.maxOpacity)
            {
                unrestrictedOpacity += 0.1;
                UpdateUnrestrictedOpacityValueText();
                SaveConfig();
            }
        }

        private void UpdateUnrestrictedOpacityValueText()
        {
            UnrestrictedOpacityValueText.Text = unrestrictedOpacity.ToString();
        }

        private void DecreaseUnrestrictedOffsetX_Click(object sender, RoutedEventArgs e)
        {
            int limitX = -Limits.GetUnrestrictedOffsetX(unrestrictedWidth, unrestrictedTickness);
            if (unrestrictedOffsetX > limitX)
            {
                unrestrictedOffsetX -= MultiplierIsChecked();
                if (unrestrictedOffsetX < limitX) unrestrictedOffsetX = limitX;
                UpdateUnrestrictedOffsetXValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedOffsetX_Click(object sender, RoutedEventArgs e)
        {
            int limitX = Limits.GetUnrestrictedOffsetX(unrestrictedWidth, unrestrictedTickness);
            if (unrestrictedOffsetX < limitX)
            {
                unrestrictedOffsetX += MultiplierIsChecked();
                if (unrestrictedOffsetX > limitX) unrestrictedOffsetX = limitX;
                UpdateUnrestrictedOffsetXValueText();
                SaveConfig();
            }
        }

        private void UpdateUnrestrictedOffsetXValueText()
        {
            UnrestrictedOffsetXValueText.Text = unrestrictedOffsetX.ToString();
        }

        private void DecreaseUnrestrictedOffsetY_Click(object sender, RoutedEventArgs e)
        {
            int limitY = -Limits.GetUnrestrictedOffsetY(unrestrictedHeight, unrestrictedTickness);
            if (unrestrictedOffsetY > limitY)
            {
                unrestrictedOffsetY -= MultiplierIsChecked();
                if (unrestrictedOffsetY < limitY) unrestrictedOffsetY = limitY;
                UpdateUnrestrictedOffsetYValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedOffsetY_Click(object sender, RoutedEventArgs e)
        {
            int limitY = Limits.GetUnrestrictedOffsetY(unrestrictedHeight, unrestrictedTickness);
            if (unrestrictedOffsetY < limitY)
            {
                unrestrictedOffsetY += MultiplierIsChecked();
                if (unrestrictedOffsetY > limitY) unrestrictedOffsetY = limitY;
                UpdateUnrestrictedOffsetYValueText();
                SaveConfig();
            }
        }

        private int MultiplierIsChecked()
        {
            int multiplier = 1;
            if (CommonMultiplier_x10.IsChecked == true) multiplier *= 10;
            if (CommonMultiplier_x100.IsChecked == true) multiplier *= 100;
            return multiplier;
        }

        private void UpdateUnrestrictedOffsetYValueText()
        {
            UnrestrictedOffsetYValueText.Text = unrestrictedOffsetY.ToString();
        }


    }
}
