using CrosshairOverlay.entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
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
        // Экземпляры
        private ConfigManager configManager;
        private CrosshairConfig config;
        private ColorFilter colorFilter;

        public SettingsWindow()
        {
            InitializeComponent();
            configManager = new ConfigManager();
            //DoIconMonoColor();
            LoadConfig();
            UpdateConfigDisplay();
            LoadConfigList();
            // Экземпляр создаем после загрузки передаваемого параметра config
            colorFilter = new ColorFilter(config);
        }

        private void LoadConfig()
        {
            try
            {
                config = configManager.LoadConfig(); // Загружаем конфиг из файла
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
            // Параметры Color Filter
            UpdateFilterSizeValueText();
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            UpdateFilterOpacityValueText();
            colorFilter = new ColorFilter(config);
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
                //var config = configManager.LoadConfig(); // Загружаем конфиг из файла

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

                configManager.SaveConfig(config); // Сохраняем конфиг в файл
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        // Функционал custom window header
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ShowWindowMenu(object sender, MouseButtonEventArgs e)
        {
            var window = Window.GetWindow(this); // Получаем текущее окно
            if (window != null)
            {
                SystemCommands.ShowSystemMenu(window, e.GetPosition(window));
            }
        }

        private void DoIconMonoColor()
        {
            Image img = (Image)FindName("EyeIconImage");
            img.Source = new BitmapImage(new Uri("resources/images/EyeIcon.png", UriKind.Relative));

            BitmapImage original = new BitmapImage(new Uri("resources/images/EyeIcon.png", UriKind.Relative));
            FormatConvertedBitmap converted = new FormatConvertedBitmap(original, PixelFormats.Gray8, null, 0);
            img.Source = converted;
        }

        // Функционал вкладки config
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
                var configTemp = new CrosshairConfig(); // Создаем объект конфигурации
                // Параметры Ellips A
                configTemp.Radius = radius;
                configTemp.Thickness = thickness;
                configTemp.StrokeColor = strokeColor;
                configTemp.StrokeOpacity = strokeOpacity;
                // Параметры Ellips B
                configTemp.OutlineRadius = outlineRadius;
                configTemp.OutlineThickness = outlineThickness;
                configTemp.OutlineColor = outlineColor;
                configTemp.OutlineOpacity = outlineOpacity;
                configTemp.OutlineOffsetX = outlineOffsetX;
                configTemp.OutlineOffsetY = outlineOffsetY;
                // Параметры Ellips C
                configTemp.UnrestrictedWidth = unrestrictedWidth;
                configTemp.UnrestrictedHeight = unrestrictedHeight;
                configTemp.UnrestrictedTickness = unrestrictedTickness;
                configTemp.UnrestrictedColor = unrestrictedColor;
                configTemp.UnrestrictedOpacity = unrestrictedOpacity;
                configTemp.UnrestrictedOffsetX = unrestrictedOffsetX;
                configTemp.UnrestrictedOffsetY = unrestrictedOffsetY;
                // Filter
                configTemp.FilterSize = config.FilterSize;
                configTemp.FilterColor = config.FilterColor;
                configTemp.FilterOpacity = config.FilterOpacity;
                // Параметры CounterStrafe
                configTemp.IsCounterStrafeEnabled = isCounterStrafeEnabled;
                configTemp.csPressureDuration = csPressureDuration;
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
                var configTemp = configManager.LoadConfig(fileName); // Загружаем конфиг
                // Параметры Ellips A
                radius = configTemp?.Radius ?? 24;
                thickness = configTemp?.Thickness ?? 1;
                strokeColor = configTemp?.StrokeColor ?? "#FF0000";
                strokeOpacity = configTemp?.StrokeOpacity ?? 1.0;
                // Параметры Ellips B
                outlineRadius = configTemp?.OutlineRadius ?? 25;
                outlineThickness = configTemp?.OutlineThickness ?? 1;
                outlineColor = configTemp?.OutlineColor ?? "#0000FF";
                outlineOpacity = configTemp?.OutlineOpacity ?? 1.0;
                outlineOffsetX = configTemp?.OutlineOffsetX ?? 0;
                outlineOffsetY = configTemp?.OutlineOffsetY ?? 0;
                // Параметры Ellips C
                unrestrictedWidth = configTemp?.UnrestrictedWidth ?? 0;
                unrestrictedHeight = configTemp?.UnrestrictedHeight ?? 0;
                unrestrictedTickness = configTemp?.UnrestrictedTickness ?? 0;
                unrestrictedColor = configTemp?.UnrestrictedColor ?? "#00FF00";
                unrestrictedOpacity = configTemp?.UnrestrictedOpacity ?? 1;
                unrestrictedOffsetX = configTemp?.UnrestrictedOffsetX ?? 0;
                unrestrictedOffsetY = configTemp?.UnrestrictedOffsetY ?? 0;
                // Параметры СounterStrafe
                isCounterStrafeEnabled = configTemp?.IsCounterStrafeEnabled ?? false;
                csPressureDuration = configTemp?.csPressureDuration ?? 100;
                // Параметры Color Filter
                config = configTemp; // Обновляем текущий конфиг
                colorFilter = new ColorFilter(config);
                SaveConfig(); // Сохраняем конфиг в файл
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

        // Функционал нижней панели окна
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
