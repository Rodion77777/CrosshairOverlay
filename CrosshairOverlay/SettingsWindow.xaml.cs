using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            }
            catch
            {
                radius = 10;
                outlineRadius = 10;
                thickness = 1;
                outlineThickness = 1;
            }
        }

        private void UpdateConfigDisplay()
        {
            RadiusValueText.Text = radius.ToString();
            OutlineRadiusValueText.Text = outlineRadius.ToString();
            SaveConfig();
        }

        private void SaveConfig()
        {
            try
            {
                string json = File.ReadAllText("config.json");
                var config = JsonConvert.DeserializeObject<CrosshairConfig>(json) ?? new CrosshairConfig();
                config.Radius = radius;
                config.OutlineRadius = outlineRadius;
                config.Thickness = thickness;
                config.OutlineThickness = outlineThickness;
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
            if (thickness < 5)
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
    }
}
