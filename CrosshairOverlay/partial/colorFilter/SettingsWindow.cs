using CrosshairOverlay.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace CrosshairOverlay
{
    // Event handlers: Color Filter
    public partial class SettingsWindow
    {
        private void DecreaseFilterSize_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.DecreaseFilterSize(MultiplierIsChecked());
            UpdateFilterSizeValueText();
            FilterTest.Text = config.FilterSize.ToString();
            SaveConfig();
        }

        private void IncreaseFilterSize_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.IncreaseFilterSize(MultiplierIsChecked());
            UpdateFilterSizeValueText();
            FilterTest.Text = config.FilterSize.ToString();
            SaveConfig();
        }

        private void UpdateFilterSizeValueText()
        {
            FilterSizeValueText.Text = config.FilterSize.ToString();
        }

        private void FilterColorPicker_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.FilterColorPicker();
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            SaveConfig();
        }

        private void UpdateFilterColorValueText()
        {
            FilterColorValueText.Text = config.FilterColor;
        }

        private void setBackgroundFilterColorIndicatorButton()
        {
            FilterColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(config.FilterColor);
        }

        private void DecreaseFilterOpacity_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.DecreaseFilterOpacity();
            FilterOpacity.Text = config.FilterOpacity.ToString();
            SaveConfig();
        }

        private void IncreaseFilterOpacity_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.IncreaseFilterOpacity();
            FilterOpacity.Text = config.FilterOpacity.ToString();
            SaveConfig();
        }

        private void UpdateFilterOpacityValueText()
        {
            FilterOpacity.Text = config.FilterOpacity.ToString();
        }
    }
}
