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
            colorFilter.DecreaseFilterWidth(MultiplierIsChecked());
            UpdateFilterSizeValueText();
            SaveConfig();
        }

        private void IncreaseFilterSize_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.IncreaseFilterWidth(MultiplierIsChecked());
            UpdateFilterSizeValueText();
            SaveConfig();
        }

        private void UpdateFilterSizeValueText()
        {
            FilterSizeValueText.Text = config.FilterWidth.ToString();
        }

        private void FilterColorPicker_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.FilterColorPicker();
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            SaveConfig();
            SetBackgroundComplementaryColorIndicatorButton();
            UpdateComplementaryColorValueText();
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

        private void StockColorRed_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.SetFilterColor("Red");
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            SaveConfig();
        }

        private void StockColorGreen_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.SetFilterColor("Green");
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            SaveConfig();
        }

        private void StockColorBlue_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.SetFilterColor("Blue");
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            SaveConfig();
        }

        private void StockColorBlack_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.SetFilterColor("Black");
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            SaveConfig();
        }

        private void StockColorGray_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.SetFilterColor("Gray");
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            SaveConfig();
        }

        private void StockColorWhite_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.SetFilterColor("White");
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            SaveConfig();
        }

        private void ComplementaryColorSubmit_Click(object sender, RoutedEventArgs e)
        {
            colorFilter.SetFilterColor(colorFilter.GetComplementaryColor());
            SaveConfig();
        }

        private void SetBackgroundComplementaryColorIndicatorButton()
        {
            ComplementaryColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(colorFilter.GetComplementaryColor());
        }

        private void UpdateComplementaryColorValueText()
        {
            ComplementaryColorValueText.Text = colorFilter.GetComplementaryColor();
        }
    }
}
