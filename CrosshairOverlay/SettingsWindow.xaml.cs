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
        // Параметры Ellips A
        private int radius;
        private int thickness;
        private string strokeColor;
        private double strokeOpacity;
        // Параметры Ellips B
        private int outlineRadius;
        private int outlineThickness;
        private string outlineColor;
        private double outlineOpacity;
        private int outlineOffsetX;
        private int outlineOffsetY;
        // Параметры Ellips C
        private int unrestrictedWidth;
        private int unrestrictedHeight;
        private int unrestrictedTickness;
        private string unrestrictedColor;
        private double unrestrictedOpacity;
        private int unrestrictedOffsetX;
        private int unrestrictedOffsetY;
        // Параметры CounterStrafe
        private bool isCounterStrafeEnabled;
        private int csPressureDuration;
        // Экземпляры
        private ConfigManager configManager;

        public SettingsWindow()
        {
            InitializeComponent();
            LoadConfig();
            UpdateConfigDisplay();
            configManager = new ConfigManager();
            LoadConfigList();
        }

        private void LoadConfig()
        {
            try
            {
                string json = File.ReadAllText("config.json");
                var config = JsonConvert.DeserializeObject<CrosshairConfig>(json);
                // Параметры Ellips A
                radius = config?.Radius ?? 10;
                thickness = config?.Thickness ?? 1;
                strokeColor = config?.StrokeColor ?? "#FF0000";
                strokeOpacity = config?.StrokeOpacity ?? 1.0;
                // Параметры Ellips B
                outlineRadius = config?.OutlineRadius ?? 10;
                outlineThickness = config?.OutlineThickness ?? 1;
                outlineColor = config?.OutlineColor ?? "#0000FF";
                outlineOpacity = config?.OutlineOpacity ?? 1.0;
                outlineOffsetX = config?.OutlineOffsetX ?? 0;
                outlineOffsetY = config?.OutlineOffsetY ?? 0;
                // Параметры Ellips C
                unrestrictedWidth = config?.UnrestrictedWidth ?? 0;
                unrestrictedHeight = config?.UnrestrictedHeight ?? 0;
                unrestrictedTickness = config?.UnrestrictedTickness ?? 0;
                unrestrictedColor = config?.UnrestrictedColor ?? "#00FF00";
                unrestrictedOpacity = config?.UnrestrictedOpacity ?? 1;
                unrestrictedOffsetX = config?.UnrestrictedOffsetX ?? 0;
                unrestrictedOffsetY = config?.UnrestrictedOffsetY ?? 0;
                // Параметры CounterStrafe
                isCounterStrafeEnabled = config?.IsCounterStrafeEnabled ?? false;
                csPressureDuration = config?.csPressureDuration ?? 100;
            }
            catch
            {
                LoadConfigDefault();
            }
        }

        private void LoadConfigDefault()
        {
            // Параметры Ellips A
            radius = 24;
            thickness = 1;
            strokeColor = "#FF0000";
            strokeOpacity = 1.0;
            // Параметры Ellips B
            outlineRadius = 25;
            outlineThickness = 1;
            outlineColor = "#0000FF";
            outlineOpacity = 1.0;
            outlineOffsetX = 0;
            outlineOffsetY = 0;
            // Параметры Ellips C
            unrestrictedWidth = 0;
            unrestrictedHeight = 0;
            unrestrictedTickness = 0;
            unrestrictedColor = "#00FF00";
            unrestrictedOpacity = 1;
            unrestrictedOffsetX = 0;
            unrestrictedOffsetY = 0;
            // Параметры CounterStrafe
            isCounterStrafeEnabled = false;
            csPressureDuration = 100;
        }

        private void UpdateConfigDisplay()
        {
            // Параметры Ellips A
            UpdateRadiusValueText();
            UpdateThicknessValueText();
            UpdateColorValueText();
            UpdateOpacityValueText();
            // Параметры Ellips B
            UpdateOutlineRadiusValueText();
            UpdateOutlineThicknessValueText();
            UpdateOutlineColorValueText();
            UpdateOutlineOpacityValueText();
            UpdateOutlineOffsetXValueText();
            UpdateOutlineOffsetYValueText();
            // Параметры Ellips C
            UpdateUnrestrictedWidthValueText();
            UpdateUnrestrictedHeightValueText();
            UpdateUnrestrictedThicknessValueText();
            UpdateUnrestrictedColorValueText();
            UpdateUnrestrictedOpacityValueText();
            UpdateUnrestrictedOffsetXValueText();
            UpdateUnrestrictedOffsetYValueText();
            // Параметры CounterStrafe
            UpdateCSCheckbox();
            UpdateCSDurationValueText();

            setBackgroundCrosshairColorIndicatorButton(strokeColor);
            setBackgroundOutlineCrosshairColorIndicatorButton(outlineColor);
            setBackgroundUnrestrictedColorIndicatorButton(unrestrictedColor);
            SaveConfig();
        }

        private void SaveConfig()
        {
            try
            {
                string json = File.ReadAllText("config.json");
                var config = JsonConvert.DeserializeObject<CrosshairConfig>(json) ?? new CrosshairConfig();
                //var config = JsonConvert.DeserializeObject<CrosshairConfig>(json, new JsonSerializerSettings { MissingMemberHandling = MissingMemberHandling.Ignore });

                // Параметры Ellips A
                config.Radius = radius;
                config.Thickness = thickness;
                config.StrokeColor = strokeColor;
                config.StrokeOpacity = strokeOpacity;
                // Параметры Ellips B
                config.OutlineRadius = outlineRadius;
                config.OutlineThickness = outlineThickness;
                config.OutlineColor = outlineColor;
                config.OutlineOpacity = outlineOpacity;
                config.OutlineOffsetX = outlineOffsetX;
                config.OutlineOffsetY = outlineOffsetY;
                // Параметры Ellips C
                config.UnrestrictedWidth = unrestrictedWidth;
                config.UnrestrictedHeight = unrestrictedHeight;
                config.UnrestrictedTickness = unrestrictedTickness;
                config.UnrestrictedColor = unrestrictedColor;
                config.UnrestrictedOpacity = unrestrictedOpacity;
                config.UnrestrictedOffsetX = unrestrictedOffsetX;
                config.UnrestrictedOffsetY = unrestrictedOffsetY;
                // Параметры CounterStrafe
                config.IsCounterStrafeEnabled = isCounterStrafeEnabled;
                config.csPressureDuration = csPressureDuration;
                File.WriteAllText("config.json", JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        // Параметры Ellips A
        private void IncreaseRadius_Click(object sender, RoutedEventArgs e)
        {
            radius += 1;
            UpdateRadiusValueText();
            SaveConfig();
        }

        private void DecreaseRadius_Click(object sender, RoutedEventArgs e)
        {
            if (radius > 1)
            {
                radius -= 1;
                UpdateRadiusValueText();
                SaveConfig();
            }
        }

        private void UpdateRadiusValueText()
        {
            RadiusValueText.Text = radius.ToString();
        }

        private void IncreaseThickness_Click(object sender, RoutedEventArgs e)
        {
            if (thickness < 5)
            {
                thickness += 1;
                UpdateThicknessValueText();
                SaveConfig();
            }
        }

        private void DecreaseThickness_Click(object sender, RoutedEventArgs e)
        {
            if (thickness > 0)
            {
                thickness -= 1;
                UpdateThicknessValueText();
                SaveConfig();
            }
        }

        private void UpdateThicknessValueText()
        {
            ThicknessValueText.Text = thickness.ToString();
        }

        private void ColorPicker_Click(object sender, RoutedEventArgs e)
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    strokeColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    UpdateColorValueText();
                    setBackgroundCrosshairColorIndicatorButton(strokeColor);
                    SaveConfig();
                }
            }
        }

        private void UpdateColorValueText()
        {
            ColorValueText.Text = strokeColor.ToString();
        }

        private void setBackgroundCrosshairColorIndicatorButton(string strokeColor)
        {
            CrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(strokeColor);
        }

        private void IncreaseOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (strokeOpacity < 1.0)
            {
                strokeOpacity += 0.1;
                UpdateOpacityValueText();
                SaveConfig();
            }
        }

        private void DecreaseOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (strokeOpacity > 0.2)
            {
                strokeOpacity -= 0.1;
                UpdateOpacityValueText();
                SaveConfig();
            }
        }

        private void UpdateOpacityValueText()
        {
            CrosshairOpacity.Text = strokeOpacity.ToString();
        }

        // Параметры Ellips B
        private void IncreaseOutlineRadius_Click(object sender, RoutedEventArgs e)
        {
            outlineRadius += 1;
            UpdateOutlineRadiusValueText();
            SaveConfig();
        }

        private void DecreaseOutlineRadius_Click(object sender, RoutedEventArgs e)
        {
            if (outlineRadius > 1)
            {
                outlineRadius -= 1;
                UpdateOutlineRadiusValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineRadiusValueText()
        {
            OutlineRadiusValueText.Text = outlineRadius.ToString();
        }

        private void IncreaseOutlineThickness_Click(object sender, RoutedEventArgs e)
        {
            if (outlineThickness < 5)
            {
                outlineThickness += 1;
                UpdateOutlineThicknessValueText();
                SaveConfig();
            }
        }

        private void DecreaseOutlineThickness_Click(object sender, RoutedEventArgs e)
        {
            if (outlineThickness > 0)
            {
                outlineThickness -= 1;
                UpdateOutlineThicknessValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineThicknessValueText()
        {
            OutlineThicknessValueText.Text = outlineThickness.ToString();
        }

        private void OutlineColorPicker_Click(object sender, RoutedEventArgs e)
        {
            using (var colorDialog = new System.Windows.Forms.ColorDialog())
            {
                if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    outlineColor = $"#{colorDialog.Color.R:X2}{colorDialog.Color.G:X2}{colorDialog.Color.B:X2}";
                    UpdateOutlineColorValueText();
                    setBackgroundOutlineCrosshairColorIndicatorButton(outlineColor);
                    SaveConfig();
                }
            }
        }

        private void setBackgroundOutlineCrosshairColorIndicatorButton(string outlineColor)
        {
            OutlineCrosshairColorIndicatorButton.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(outlineColor);
        }

        private void UpdateOutlineColorValueText()
        {
            OutlineColorValueText.Text = outlineColor.ToString();
        }

        private void IncreaseOutlineOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOpacity < 1.0)
            {
                outlineOpacity += 0.1;
                UpdateOutlineOpacityValueText();
                SaveConfig();
            }
        }

        private void DecreaseOutlineOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOpacity > 0.2)
            {
                outlineOpacity -= 0.1;
                UpdateOutlineOpacityValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineOpacityValueText()
        {
            OutlineCrosshairOpacity.Text = outlineOpacity.ToString();
        }

        private void DecreaseOutlineOffsetX_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOffsetX > -100)
            {
                outlineOffsetX -= 1;
                UpdateOutlineOffsetXValueText();
                SaveConfig();
            }
        }

        private void IncreaseOutlineOffsetX_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOffsetX < 100)
            {
                outlineOffsetX += 1;
                UpdateOutlineOffsetXValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineOffsetXValueText()
        {
            OutlineCrosshairOffsetX.Text = outlineOffsetX.ToString();
        }

        private void DecreaseOutlineOffsetY_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOffsetY > -100)
            {
                outlineOffsetY -= 1;
                UpdateOutlineOffsetYValueText();
                SaveConfig();
            }
        }

        private void IncreaseOutlineOffsetY_Click(object sender, RoutedEventArgs e)
        {
            if (outlineOffsetY < 100)
            {
                outlineOffsetY += 1;
                UpdateOutlineOffsetYValueText();
                SaveConfig();
            }
        }

        private void UpdateOutlineOffsetYValueText()
        {
            OutlineCrosshairOffsetY.Text = outlineOffsetY.ToString();
        }

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
            unrestrictedWidth += MultiplierIsChecked();
            UpdateUnrestrictedWidthValueText();
            SaveConfig();

            if (WH_Merge.IsChecked == true)
            {
                IncreaseUnrestrictedHeight_Click();
            }
        }

        private void IncreaseUnrestrictedWidth_Click()
        {
            unrestrictedWidth += MultiplierIsChecked();
            UpdateUnrestrictedWidthValueText();
            SaveConfig();
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
            unrestrictedHeight += MultiplierIsChecked();
            UpdateConfigDisplay();
            SaveConfig();

            if (WH_Merge.IsChecked == true)
            {
                IncreaseUnrestrictedWidth_Click();
            }
        }

        private void IncreaseUnrestrictedHeight_Click()
        {
            unrestrictedHeight += MultiplierIsChecked();
            UpdateConfigDisplay();
            SaveConfig();
        }

        private void UpdateUnrestrictedHeightValueText()
        {
            UnrestrictedHeightValueText.Text = unrestrictedHeight.ToString();
        }

        private void DecreaseUnrestrictedThickness_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedTickness > 0)
            {
                unrestrictedTickness -= 1;
                UpdateUnrestrictedThicknessValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedThickness_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedTickness < 5)
            {
                unrestrictedTickness += 1;
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
            if (unrestrictedOpacity > 0.2)
            {
                unrestrictedOpacity -= 0.1;
                UpdateUnrestrictedOpacityValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedOpacity_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedOpacity < 1.0)
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
            if (unrestrictedOffsetX > -100)
            {
                unrestrictedOffsetX -= MultiplierIsChecked();
                if (unrestrictedOffsetX < -100) unrestrictedOffsetX = -100;
                UpdateUnrestrictedOffsetXValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedOffsetX_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedOffsetX < 100)
            {
                unrestrictedOffsetX += MultiplierIsChecked();
                if (unrestrictedOffsetX > 100) unrestrictedOffsetX = 100;
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
            if (unrestrictedOffsetY > -100)
            {
                unrestrictedOffsetY -= MultiplierIsChecked();
                if (unrestrictedOffsetY < -100) unrestrictedOffsetY = -100;
                UpdateUnrestrictedOffsetYValueText();
                SaveConfig();
            }
        }

        private void IncreaseUnrestrictedOffsetY_Click(object sender, RoutedEventArgs e)
        {
            if (unrestrictedOffsetY < 100)
            {
                unrestrictedOffsetY += MultiplierIsChecked();
                if (unrestrictedOffsetY > 100) unrestrictedOffsetY = 100;
                UpdateUnrestrictedOffsetYValueText();
                SaveConfig();
            }
        }

        private int MultiplierIsChecked()
        {
            return CommonMultiplier.IsChecked == true ? 10 : 1;
        }

        private void UpdateUnrestrictedOffsetYValueText()
        {
            UnrestrictedOffsetYValueText.Text = unrestrictedOffsetY.ToString();
        }

        // Параметры CounterStrafe
        private void CounterStrafeCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            isCounterStrafeEnabled = true;
            UpdateCSCheckbox();
            SaveConfig();
        }

        private void CounterStrafeCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            isCounterStrafeEnabled = false;
            UpdateCSCheckbox();
            SaveConfig();
        }


        private void BannyhopCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BannyhopCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateCSCheckbox()
        {
            CounterStrafeCheckbox.IsChecked = isCounterStrafeEnabled;
        }

        private void IncreaseCSDuration_Click(object sender, RoutedEventArgs e)
        {
            if (csPressureDuration < 500)
            {
                csPressureDuration += 10;
                UpdateCSDurationValueText();
                SaveConfig();
            }
        }

        private void DecreaseCSDuration_Click(object sender, RoutedEventArgs e)
        {
            if (csPressureDuration > 0)
            {
                csPressureDuration -= 10;
                UpdateCSDurationValueText();
                SaveConfig();
            }
        }

        private void UpdateCSDurationValueText()
        {
            CounterStrafeDurationText.Text = csPressureDuration.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int maxLength = 60; // Устанавливаем лимит символов
            if (CSTestBox.Text.Length > maxLength)
            {
                CSTestBox.Text = ""; // Очистка, если превышен лимит
            }
        }

        private void SaveConfigButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName = ConfigNameTextBox.Text.Trim(); // Получаем имя из TextBox

            if (string.IsNullOrWhiteSpace(fileName))
            {
                MessageBox.Show("Введите корректное имя файла!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var config = new CrosshairConfig(); // Создаем объект конфигурации
                // Параметры Ellips A
                config.Radius = radius;
                config.Thickness = thickness;
                config.StrokeColor = strokeColor;
                config.StrokeOpacity = strokeOpacity;
                // Параметры Ellips B
                config.OutlineRadius = outlineRadius;
                config.OutlineThickness = outlineThickness;
                config.OutlineColor = outlineColor;
                config.OutlineOpacity = outlineOpacity;
                config.OutlineOffsetX = outlineOffsetX;
                config.OutlineOffsetY = outlineOffsetY;
                // Параметры Ellips C
                config.UnrestrictedWidth = unrestrictedWidth;
                config.UnrestrictedHeight = unrestrictedHeight;
                config.UnrestrictedTickness = unrestrictedTickness;
                config.UnrestrictedColor = unrestrictedColor;
                config.UnrestrictedOpacity = unrestrictedOpacity;
                config.UnrestrictedOffsetX = unrestrictedOffsetX;
                config.UnrestrictedOffsetY = unrestrictedOffsetY;
                // Параметры CounterStrafe
                config.IsCounterStrafeEnabled = isCounterStrafeEnabled;
                config.csPressureDuration = csPressureDuration;
                configManager.SaveConfig(config, fileName); // Сохраняем файл
                LoadConfigList(); // Обновляем список конфигов
                MessageBox.Show($"Конфиг '{fileName}' успешно сохранен!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadConfigList()
        {
            СonfigSelector.Items.Clear(); // Очищаем список перед обновлением
            string[] configs = configManager.ListConfigs(); // Получаем список конфигов

            foreach (string config in configs)
            {
                СonfigSelector.Items.Add(config); // Добавляем файлы в ComboBox
            }

            if (СonfigSelector.Items.Count > 0)
            {
                СonfigSelector.SelectedIndex = 0; // Выбираем первый конфиг по умолчанию
            }
        }

        private void LoadConfigButton_Click(object sender, RoutedEventArgs e)
        {
            //Получаем имя выбранного конфига
            string fileName = СonfigSelector.Text;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                MessageBox.Show("Выберите конфиг для загрузки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var config = configManager.LoadConfig(fileName); // Загружаем конфиг
                // Параметры Ellips A
                radius = config?.Radius ?? 24;
                thickness = config?.Thickness ?? 1;
                strokeColor = config?.StrokeColor ?? "#FF0000";
                strokeOpacity = config?.StrokeOpacity ?? 1.0;
                // Параметры Ellips B
                outlineRadius = config?.OutlineRadius ?? 25;
                outlineThickness = config?.OutlineThickness ?? 1;
                outlineColor = config?.OutlineColor ?? "#0000FF";
                outlineOpacity = config?.OutlineOpacity ?? 1.0;
                outlineOffsetX = config?.OutlineOffsetX ?? 0;
                outlineOffsetY = config?.OutlineOffsetY ?? 0;
                // Параметры Ellips C
                unrestrictedWidth = config?.UnrestrictedWidth ?? 0;
                unrestrictedHeight = config?.UnrestrictedHeight ?? 0;
                unrestrictedTickness = config?.UnrestrictedTickness ?? 0;
                unrestrictedColor = config?.UnrestrictedColor ?? "#00FF00";
                unrestrictedOpacity = config?.UnrestrictedOpacity ?? 1;
                unrestrictedOffsetX = config?.UnrestrictedOffsetX ?? 0;
                unrestrictedOffsetY = config?.UnrestrictedOffsetY ?? 0;
                // Параметры СounterStrafe
                isCounterStrafeEnabled = config?.IsCounterStrafeEnabled ?? false;
                csPressureDuration = config?.csPressureDuration ?? 100;
                UpdateConfigDisplay(); // Обновляем отображение
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Файл не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteConfigButton_Click(object sender, RoutedEventArgs e)
        {
            if (ConfirmDeleteConfig.IsChecked == false) return;
            string fileName = СonfigSelector.Text; // Получаем имя выбранного конфига
            try
            {
                configManager.DeleteConfig(fileName); // Удаляем выбранный конфиг
                ConfirmDeleteConfig.IsChecked = false; // Сбрасываем состояние чекбокса
                LoadConfigList(); // Обновляем список конфигов
                MessageBox.Show($"Конфиг '{fileName}' успешно удален!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CrosshairResetButton_Click(object sender, RoutedEventArgs e)
        {
            if (CrosshairResetConfirmCheckbox.IsChecked == true)
            {
                LoadConfigDefault();
                CrosshairResetConfirmCheckbox.IsChecked = false; // Сбросить состояние чекбокса
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
