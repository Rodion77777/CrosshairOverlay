using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CrosshairOverlay
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private int radius;
        private int outlineRadius;
        private int thickness;
        private int outlineThickness;
        private string strokeColor;
        private string outlineColor;
        private double strokeOpacity;
        private double outlineOpacity;
        private bool isCounterStrafeEnabled;
        private int csPressureDuration;

        public SettingsWindow()
        {
            InitializeComponent();
            LoadConfig();
            UpdateConfigDisplay();
        }

        private void LoadConfig()
        {
            try
            {
                string json = File.ReadAllText("config.json");
                var config = JsonConvert.DeserializeObject<CrosshairConfig>(json);
                radius = (int)(config?.Radius ?? 10);
                outlineRadius = (int)(config?.OutlineRadius ?? 10);
                thickness = (int)(config?.Thickness ?? 1);
                outlineThickness = (int)(config?.OutlineThickness ?? 1);
                strokeColor = config?.StrokeColor ?? "#FF0000";
                outlineColor = config?.OutlineColor ?? "#0000FF";
                strokeOpacity = config?.StrokeOpacity ?? 1.0;
                outlineOpacity = config?.OutlineOpacity ?? 1.0;
                isCounterStrafeEnabled = config?.IsCounterStrafeEnabled ?? false;
                csPressureDuration = config?.csPressureDuration ?? 100;
            }
            catch
            {
                radius = 10;
                outlineRadius = 10;
                thickness = 1;
                outlineThickness = 1;
                strokeColor = "#FF0000";
                outlineColor = "#0000FF";
                strokeOpacity = 1.0;
                outlineOpacity = 1.0;
                isCounterStrafeEnabled = false;
                csPressureDuration = 100;
            }
        }

        private void UpdateConfigDisplay()
        {
            RadiusValueText.Text = radius.ToString();
            OutlineRadiusValueText.Text = outlineRadius.ToString();
            ThicknessValueText.Text = thickness.ToString();
            OutlineThicknessValueText.Text = outlineThickness.ToString();
            ColorValueText.Text = strokeColor.ToString();
            OutlineColorValueText.Text = outlineColor.ToString();
            CrosshairOpacity.Text = strokeOpacity.ToString();
            OutlineCrosshairOpacity.Text = outlineOpacity.ToString();
            CounterStrafeCheckbox.IsChecked = isCounterStrafeEnabled;
            CounterStrafeDurationText.Text = csPressureDuration.ToString();
            setBackgroundCrosshairColorIndicatorButton(strokeColor);
            setBackgroundOutlineCrosshairColorIndicatorButton(outlineColor);
            SaveConfig();
        }

        private void SaveConfig()
        {
            try
            {
                string json = File.ReadAllText("config.json");
                var config = JsonConvert.DeserializeObject<CrosshairConfig>(json) ?? new CrosshairConfig();
                //var config = JsonConvert.DeserializeObject<CrosshairConfig>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore });

                config.Radius = radius;
                config.OutlineRadius = outlineRadius;
                config.Thickness = thickness;
                config.OutlineThickness = outlineThickness;
                config.StrokeColor = strokeColor;
                config.OutlineColor = outlineColor;
                config.StrokeOpacity = strokeOpacity;
                config.OutlineOpacity = outlineOpacity;
                config.IsCounterStrafeEnabled = isCounterStrafeEnabled;
                config.csPressureDuration = csPressureDuration;
                File.WriteAllText("config.json", JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }
        
        private void IncreaseRadius_Click(object sender, RoutedEventArgs e)
        {
            radius += 1;
            UpdateConfigDisplay();
        }

        private void DecreaseRadius_Click(object sender, RoutedEventArgs e)
        {
            if (radius > 1)
            {
                radius -= 1;
                UpdateConfigDisplay();
            }
        }

        private void IncreaseOutlineRadius_Click(object sender, RoutedEventArgs e)
        {
            outlineRadius += 1;
            UpdateConfigDisplay();
        }

        private void DecreaseOutlineRadius_Click(object sender, RoutedEventArgs e)
        {
            if (outlineRadius > 1)
            {
                outlineRadius -= 1;
                UpdateConfigDisplay();
            }
        }

        private void IncreaseThickness_Click(object sender, RoutedEventArgs e)
        {
            if (thickness < 5)
            {
                thickness += 1;
                UpdateConfigDisplay();
            }
        }

        private void DecreaseThickness_Click(object sender, RoutedEventArgs e)
        {
            if (thickness > 0)
            {
                thickness -= 1;
                UpdateConfigDisplay();
            }
        }

        private void IncreaseOutlineThickness_Click(object sender, RoutedEventArgs e)
        {
            if (outlineThickness < 5)
            {
                outlineThickness += 1;
                UpdateConfigDisplay();
            }
        }

        private void DecreaseOutlineThickness_Click(object sender, RoutedEventArgs e)
        {
            if (outlineThickness > 0)
            {
                outlineThickness -= 1;
                UpdateConfigDisplay();
            }
        }

        private void ColorPicker_Click(object sender, RoutedEventArgs e)
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    strokeColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    UpdateConfigDisplay();
                }
            }
        }

        private void setBackgroundCrosshairColorIndicatorButton(string strokeColor)
        {
            CrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(strokeColor);
        }

        private void OutlineColorPicker_Click(object sender, RoutedEventArgs e)
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    outlineColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    UpdateConfigDisplay();
                }
            }
        }

        private void setBackgroundOutlineCrosshairColorIndicatorButton(string outlineColor)
        {
            OutlineCrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(outlineColor);
        }

        private void IncreaseOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (strokeOpacity < 1.0)
            {
                strokeOpacity += 0.1;
                UpdateConfigDisplay();
            }
        }

        private void DecreaseOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (strokeOpacity > 0.2)
            {
                strokeOpacity -= 0.1;
                UpdateConfigDisplay();
            }
        }

        private void IncreaseOutlineOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOpacity < 1.0)
            {
                outlineOpacity += 0.1;
                UpdateConfigDisplay();
            }
        }

        private void DecreaseOutlineOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOpacity > 0.2)
            {
                outlineOpacity -= 0.1;
                UpdateConfigDisplay();
            }
        }

        private void CounterStrafeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            isCounterStrafeEnabled = true;
            UpdateConfigDisplay();
        }

        private void CounterStrafeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            isCounterStrafeEnabled = false;
            UpdateConfigDisplay();
        }

        private void IncreaseCSDuration_Click(object sender, RoutedEventArgs e)
        {
            if (csPressureDuration < 500)
            {
                csPressureDuration += 10;
                UpdateConfigDisplay();
            }
        }

        private void DecreaseCSDuration_Click(object sender, RoutedEventArgs e)
        {
            if (csPressureDuration > 0)
            {
                csPressureDuration -= 10;
                UpdateConfigDisplay();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            // Закрываем главное окно, а вместе с ним и всё приложение
            Application.Current.Shutdown();
        }
    }
}
