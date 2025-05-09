using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Input;

namespace CrosshairOverlay
{    public partial class MainWindow : Window
    {
        private FileSystemWatcher configWatcher;
        private CrosshairConfig config;
        private SettingsWindow settingsWindow;

        public MainWindow()
        {
            InitializeComponent();
            // Устанавливаем размер окна = размер экрана
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;
            // Устанавливаем положение в 0,0
            this.Left = 0;
            this.Top = 0;
            this.Loaded += MakeWindowTransparentToMouse;
            LoadConfig();
            SetupOverlay();
            MakeWindowTransparentToMouse(null, null);
            StartConfigWatcher();

            this.PreviewKeyDown += MainWindow_PreviewKeyDown;
            this.KeyDown += MainWindow_KeyDown;
            this.Focusable = true;
            this.Focus();
        }

        private void LoadConfig()
        {
            try
            {
                string json = File.ReadAllText("config.json");
                config = JsonConvert.DeserializeObject<CrosshairConfig>(json);
            }
            catch
            {
                config = new CrosshairConfig(); // дефолт
            }
        }

        private void SetupOverlay()
        {
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
            Left = 0;
            Top = 0;

            var outerRadius = config.Radius + config.OutlineThickness;
            var innerRadius = config.Radius;

            var grid = new Grid();

            // Внешний круг (обводка)
            var outline = new Ellipse
            {
                Width = outerRadius * 2,
                Height = outerRadius * 2,
                Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(config.OutlineColor)),
                StrokeThickness = config.OutlineThickness
            };

            // Внутренний круг (прицел)
            var circle = new Ellipse
            {
                Width = innerRadius * 2,
                Height = innerRadius * 2,
                Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(config.StrokeColor)),
                StrokeThickness = config.Thickness
            };

            // Центрируем
            outline.HorizontalAlignment = HorizontalAlignment.Center;
            outline.VerticalAlignment = VerticalAlignment.Center;
            circle.HorizontalAlignment = HorizontalAlignment.Center;
            circle.VerticalAlignment = VerticalAlignment.Center;

            grid.Children.Add(outline);
            grid.Children.Add(circle);
            Content = grid;
        }

        private void MakeWindowTransparentToMouse(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }

        private void StartConfigWatcher()
        {
            configWatcher = new FileSystemWatcher
            {
                Path = AppDomain.CurrentDomain.BaseDirectory,
                Filter = "config.json",
                NotifyFilter = NotifyFilters.LastWrite
            };

            configWatcher.Changed += (s, e) =>
            {
                Dispatcher.Invoke(() =>
                {
                    try
                    {
                        LoadConfig();
                        SetupOverlay();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка при обновлении конфигурации: " + ex.Message);
                    }
                });
            };

            configWatcher.EnableRaisingEvents = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            configWatcher?.Dispose(); // остановить FileSystemWatcher
            base.OnClosed(e);         // вызвать базовую реализацию
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Home)
            {
                if (settingsWindow == null || !settingsWindow.IsVisible)
                {
                    settingsWindow = new SettingsWindow();
                    settingsWindow.Show();
                }
                else
                {
                    settingsWindow.Close();
                    settingsWindow = null;
                }
            }
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e);
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Home)
            {
                var settingsWindow = new SettingsWindow();
                settingsWindow.Owner = this;
                settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settingsWindow.Show();
            }
        }

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_EX_LAYERED = 0x00080000;

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);
    }
}
