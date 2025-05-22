using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CrosshairOverlay
{
    public partial class SettingsWindow
    {
        private string borderColor = "Purple";

        private void BorderColorPicker_Click(object sender, RoutedEventArgs e)
        {
            ColorPicker();
            SetWindowBorderColor();
            UpdateBorderColorValueText();
            SetBackgroundBorderColorIndicatorButton();
            SaveConfig();
        }

        public void ColorPicker()
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    borderColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    //config.windowBorderColor = borderColor;
                }
            }
        }

        private void SetBackgroundBorderColorIndicatorButton()
        {
            BorderColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(borderColor);
        }

        private void UpdateBorderColorValueText()
        {
            BorderColorValueText.Text = borderColor;
        }

        private void DesignThemeBlack_Click(object sender, RoutedEventArgs e)
        {
            SetDictionary("DarkTheme");
        }

        private void DesignThemeGray_Click(object sender, RoutedEventArgs e)
        {
            SetDictionary("StockTheme");
        }

        private void DesignThemeWhite_Click(object sender, RoutedEventArgs e)
        {
            SetDictionary("LightTheme");
        }

        private void WindowBorderColorBlack_Click(object sender, RoutedEventArgs e)
        {
            borderColor = "Black";
            SetWindowBorderColor();
        }

        private void WindowBorderColorPurple_Click(object sender, RoutedEventArgs e)
        {
            borderColor = "#8000FF";
            SetWindowBorderColor();
        }

        private void WindowBorderColorWhite_Click(object sender, RoutedEventArgs e)
        {
            borderColor = "White";
            SetWindowBorderColor();
        }

        private void SetWindowBorderColor()
        {
            //var color = (Color)ColorConverter.ConvertFromString(borderColor);
            WindowBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#2C2C2C");
            CrosshairWindow.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(borderColor);
            SaveConfig();
        }

        private void SetDictionary(string theme)
        {
            ResourceDictionary dicrionary = new ResourceDictionary();
            string uri = $"pack://application:,,,/CrosshairOverlay;component/style/{theme}.xaml";
            dicrionary.Source = new Uri(uri, UriKind.Absolute);
            ApplyResourceDictionary(dicrionary);
        }

        private void ApplyResourceDictionary(ResourceDictionary dicrionary)
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(dicrionary);
        }
    }
}
