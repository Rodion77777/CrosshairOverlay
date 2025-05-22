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
        private EllipsA ellipsA;
        private EllipsB ellipsB;
        private EllipsC ellipsC;
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
            ellipsA = new EllipsA(config);
            ellipsB = new EllipsB(config);
            ellipsC = new EllipsC(config);
            colorFilter = new ColorFilter(config);
        }

        private void LoadConfig()
        {
            try
            {
                config = configManager.LoadConfig(); // Загружаем конфиг из файла
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
            // Параметры CounterStrafe
            isCounterStrafeEnabled = false;
            csPressureDuration = 100;

            config = new CrosshairConfig();
            ellipsA = new EllipsA(config);
            ellipsB = new EllipsB(config);
            ellipsC = new EllipsC(config);
            colorFilter = new ColorFilter(config);
        }

        private void UpdateConfigDisplay()
        {
            // Параметры Ellips A
            UpdateRadiusValueText();
            UpdateThicknessValueText();
            UpdateColorValueText();
            setBackgroundCrosshairColorIndicatorButton();
            UpdateOpacityValueText();
            ellipsA = new EllipsA(config);
            // Параметры Ellips B
            UpdateOutlineRadiusValueText();
            UpdateOutlineThicknessValueText();
            UpdateOutlineColorValueText();
            setBackgroundOutlineCrosshairColorIndicatorButton();
            UpdateOutlineOpacityValueText();
            UpdateOutlineOffsetXValueText();
            UpdateOutlineOffsetYValueText();
            ellipsB = new EllipsB(config);
            // Параметры Ellips C
            UpdateUnrestrictedWidthValueText();
            UpdateUnrestrictedHeightValueText();
            UpdateUnrestrictedThicknessValueText();
            UpdateUnrestrictedColorValueText();
            setBackgroundUnrestrictedColorIndicatorButton();
            UpdateUnrestrictedOpacityValueText();
            UpdateUnrestrictedOffsetXValueText();
            UpdateUnrestrictedOffsetYValueText();
            ellipsC = new EllipsC(config);
            // Параметры Color Filter
            UpdateFilterSizeValueText();
            UpdateFilterColorValueText();
            setBackgroundFilterColorIndicatorButton();
            UpdateFilterOpacityValueText();
            colorFilter = new ColorFilter(config);
            // Параметры CounterStrafe
            UpdateCSCheckbox();
            UpdateCSDurationValueText();

            SaveConfig();
        }

        private void SaveConfig()
        {
            try
            {
                // Параметры CounterStrafe
                config.IsCounterStrafeEnabled = isCounterStrafeEnabled;
                config.csPressureDuration = csPressureDuration;

                configManager.SaveConfig(config); // Сохраняем конфиг в файл
            }
            catch (Exception ex)
            {
                MessageNoticeShow("Error saving: " + ex.Message, false);
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
                MessageNoticeShow("Enter the correct file name!", false);
                return;
            }

            try
            {
                configManager.SaveConfig(config, fileName); // Сохраняем файл
                LoadConfigList(); // Обновляем список конфигов
                MessageNoticeShow($"Config '{fileName}' successfully saved!", true);
            }
            catch (Exception ex)
            {
                MessageNoticeShow($"Error when saving: {ex.Message}", false);
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
                MessageNoticeShow("Select a config to download!", false);
                return;
            }
            try
            {
                config = configManager.LoadConfig(fileName); // Загружаем конфиг

                // Параметры СounterStrafe
                isCounterStrafeEnabled = config?.IsCounterStrafeEnabled ?? false;
                csPressureDuration = config?.csPressureDuration ?? 100;
                // Параметры Color Filter
                ellipsA = new EllipsA(config);
                ellipsB = new EllipsB(config);
                ellipsC = new EllipsC(config);
                colorFilter = new ColorFilter(config);
                SaveConfig(); // Сохраняем конфиг в файл
                UpdateConfigDisplay(); // Обновляем отображение
                MessageNoticeShow($"Config '{fileName}' successfully loaded!", true);
            }
            catch (FileNotFoundException)
            {
                MessageNoticeShow("File not found!", false);
            }
            catch (Exception ex)
            {
                MessageNoticeShow($"Error during download: {ex.Message}", false);
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
                MessageNoticeShow($"Config '{fileName}' successfully deleted!", true);
            }
            catch (Exception ex)
            {
                MessageNoticeShow($"Deletion error: {ex.Message}", false);
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
                MessageNoticeShow("Config reset to default settings!", true);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MessageNoticeShow(string text, bool ok)
        {
            if (ok) Message_notice.Foreground = Brushes.Green;
            else Message_notice.Foreground = Brushes.Red;
            Message_notice.Text = text;
        }
    }
}
