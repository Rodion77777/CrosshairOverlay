using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CrosshairOverlay
{
    public partial class SettingsWindow
    {
        private string borderColor = "Purple";
        private string highlightColor = "White";
        private Color testColor;

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
                    Color color = Color.FromArgb(colorDialog.Color.A, colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
                    testColor = color;
                    borderColor = color.ToString();
                    highlightColor = ColorUtil.Lighten(color, 0.3).ToString();
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
            BorderColorValueText.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(highlightColor);
        }

        private void DesignThemeBlack_Click(object sender, RoutedEventArgs e)
        {
            SetDictionary("DarkTheme");
            // Удаляем существующие стили перед добавлением новых
            if (Application.Current.Resources.Contains(typeof(TabControl)))
            {
                Application.Current.Resources.Remove(typeof(TabControl));
            }

            if (Application.Current.Resources.Contains(typeof(TabItem)))
            {
                Application.Current.Resources.Remove(typeof(TabItem));
            }

            // Новый стиль для TabControl
            var tabControlStyle = new Style(typeof(TabControl));
            tabControlStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(testColor)));

            // Новый стиль для TabItem
            var tabItemStyle = new Style(typeof(TabItem));
            tabItemStyle.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(testColor)));

            // Добавляем стили в глобальные ресурсы
            Application.Current.Resources.Add(typeof(TabControl), tabControlStyle);
            Application.Current.Resources.Add(typeof(TabItem), tabItemStyle);
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
