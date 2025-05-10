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
using System.Windows.Forms;
using Gma.System.MouseKeyHook;

namespace CrosshairOverlay
{   public partial class MainWindow : Window
    {
        private FileSystemWatcher configWatcher;
        private CrosshairConfig config;
        private SettingsWindow settingsWindow;
        private IKeyboardMouseEvents _globalHook;

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
            SubscribeGlobalHook();
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

            var outerRadius = config.OutlineRadius + config.OutlineThickness;
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
            outline.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            outline.VerticalAlignment = VerticalAlignment.Center;
            circle.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
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
                        System.Windows.MessageBox.Show("Ошибка при обновлении конфигурации: " + ex.Message);
                    }
                });
            };

            configWatcher.EnableRaisingEvents = true;
        }

        private void SubscribeGlobalHook()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHook_KeyDown;
        }

        private void GlobalHook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Home)
            {
                Dispatcher.Invoke(() =>
                {
                    var settingsWindow = new SettingsWindow
                    {
                        Owner = this,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner
                    };
                    settingsWindow.Show();
                });
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            _globalHook.KeyDown -= GlobalHook_KeyDown;
            _globalHook.Dispose();

            configWatcher?.Dispose(); // остановить FileSystemWatcher
            base.OnClosed(e);         // вызвать базовую реализацию
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
