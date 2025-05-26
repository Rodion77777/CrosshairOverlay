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
using System.Threading.Tasks;

namespace CrosshairOverlay
{   public partial class MainWindow : Window
    {
        private FileSystemWatcher configWatcher;
        private CrosshairConfig config;

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

            var innerRadius = config.Radius;
            var outerRadius = config.OutlineRadius + config.OutlineThickness - 1;
            var unrestrictedWidth = config.UnrestrictedWidth + config.UnrestrictedTickness;
            var unrestrictedHeight = config.UnrestrictedHeight + config.UnrestrictedTickness;
            var filterWidth = config.FilterWidth;
            var filterHeight = config.FilterHeight;

            var grid = new Grid();

            // Внутренний круг (прицел)
            var circle = new Ellipse
            {
                Width = innerRadius,
                Height = innerRadius,
                Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(config.StrokeColor)),
                StrokeThickness = config.Thickness,
                Opacity = config.StrokeOpacity
            };

            // Внешний круг (обводка)
            var outline = new Ellipse
            {
                Width = outerRadius,
                Height = outerRadius,
                Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(config.OutlineColor)),
                StrokeThickness = config.OutlineThickness,
                Opacity = config.OutlineOpacity,
                Margin = new Thickness(config.OutlineOffsetX, -config.OutlineOffsetY, 0, 0)
            };

            // Свободный элемент (неограниченная фигура)
            var unrestricted = new Ellipse
            {
                Width = unrestrictedWidth,
                Height = unrestrictedHeight,
                Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom(config.UnrestrictedColor)),
                StrokeThickness = config.UnrestrictedTickness,
                Opacity = config.UnrestrictedOpacity,
                Margin = new Thickness(config.UnrestrictedOffsetX, -config.UnrestrictedOffsetY, 0, 0)
            };

            // Filter
            var rectangle = new Rectangle
            {
                Width = filterWidth,
                Height = filterHeight,
                Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom(config.FilterColor)),
                Opacity = config.FilterOpacity
            };

            // Центрируем
            circle.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            circle.VerticalAlignment = VerticalAlignment.Center;
            outline.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            outline.VerticalAlignment = VerticalAlignment.Center;
            unrestricted.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            unrestricted.VerticalAlignment = VerticalAlignment.Center;
            rectangle.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            rectangle.VerticalAlignment = VerticalAlignment.Center;

            grid.Children.Add(circle);
            grid.Children.Add(outline);
            grid.Children.Add(unrestricted);
            grid.Children.Add(rectangle);
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
