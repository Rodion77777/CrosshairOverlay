using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Drawing;
using System.Threading;

namespace CrosshairOverlay
{
    public partial class MainWindow
    {
        private SettingsWindow settingsWindow;
        private IKeyboardMouseEvents _globalHook;
        private double? lastSettingsLeft;
        private double? lastSettingsTop;
        private bool isGlobalHookEnable = true;
        // Mouse event
        private bool mouseSmoothIsEnambled = true;
        private bool isMouseDown = false;
        private CancellationTokenSource moveTokenSource;
        private POINT originalPos;
        private int moved = 0;

        private void SubscribeGlobalHook()
        {
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyDown += GlobalHook_KeyDown;
            _globalHook.KeyUp += GlobalHook_KeyUp;
            //_globalHook.MouseClick += GlobalHook_MouseClick;
            //_globalHook.MouseDown += GlobalHook_MouseDown;
            //_globalHook.MouseUp += GlobalHook_MouseUp;
        }

        private async void GlobalHook_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && mouseSmoothIsEnambled)
            {
                // Двигаем курсор вниз на 100 пикселей, с шагом 5, пауза 10 мс между шагами
                await MoveCursorDownSmooth(distance: 100, stepDelayMs: 10, stepSize: 5);
            }
        }

        private async Task MoveCursorDownSmooth(int distance, int stepDelayMs, int stepSize)
        {
            GetCursorPos(out POINT startPos);
            int targetY = startPos.Y + distance;

            for (int y = startPos.Y; y < targetY; y += stepSize)
            {
                SetCursorPos(startPos.X, y);
                await Task.Delay(stepDelayMs);
            }
        }

        //private void GlobalHook_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left && !isMouseDown && mouseSmoothIsEnambled)
        //    {
        //        isMouseDown = true;
        //        GetCursorPos(out originalPos);
        //        moveTokenSource = new CancellationTokenSource();
        //        Task.Run(() => MoveCursorDownContinuously(moveTokenSource.Token));
        //    }
        //}

        //private void GlobalHook_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        //{
        //    if (e.Button == System.Windows.Forms.MouseButtons.Left && isMouseDown && mouseSmoothIsEnambled)
        //    {
        //        isMouseDown = false;
        //        moveTokenSource.Cancel();
        //        Task.Run(() => MoveCursorBackToOriginal(originalPos));
        //    }
        //}

        private async Task MoveCursorDownContinuously(CancellationToken token)
        {
            while (!token.IsCancellationRequested && moved < 50)
            {
                if (GetCursorPos(out POINT current))
                {
                    SetCursorPos(current.X, current.Y + 2); // шаг вниз
                    moved += 2; // увеличиваем счетчик перемещения
                }
                await Task.Delay(10); // задержка между шагами
            }
        }

        private async Task MoveCursorBackToOriginal(POINT original)
        {
            GetCursorPos(out POINT current);
            while (current.Y > original.Y)
            {
                int newY = Math.Max(original.Y, current.Y - 2); // шаг вверх
                moved -= 2; // уменьшаем счетчик перемещения
                SetCursorPos(current.X, newY);
                await Task.Delay(10);
                GetCursorPos(out current);
            }
        }

        private void GlobalHook_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Home)
            {
                Dispatcher.Invoke(() =>
                {
                    if (settingsWindow == null || !settingsWindow.IsVisible)
                    {
                        settingsWindow = new SettingsWindow
                        {
                            Owner = this,
                            WindowStartupLocation = WindowStartupLocation.Manual
                        };

                        // Если координаты есть — применяем
                        if (lastSettingsLeft.HasValue && lastSettingsTop.HasValue)
                        {
                            settingsWindow.Left = lastSettingsLeft.Value;
                            settingsWindow.Top = lastSettingsTop.Value;
                        }
                        else
                        {
                            // Первый запуск — центрируем
                            settingsWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        }

                        settingsWindow.Closed += (s, args) =>
                        {
                            // Сохраняем позицию перед закрытием
                            lastSettingsLeft = settingsWindow.Left;
                            lastSettingsTop = settingsWindow.Top;
                            settingsWindow = null;
                        };

                        settingsWindow.Show();
                    }
                    else
                    {
                        settingsWindow.Close();
                        settingsWindow = null;
                    }
                });
            }
        }

        private void GlobalHook_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (!config.IsCounterStrafeEnabled || !isGlobalHookEnable) return;

            switch (e.KeyCode)
            {
                case Keys.A:
                    SimulateKeyPress(Keys.D);
                    break;
                case Keys.D:
                    SimulateKeyPress(Keys.A);
                    break;
                case Keys.W:
                    SimulateKeyPress(Keys.S);
                    break;
                case Keys.S:
                    SimulateKeyPress(Keys.W);
                    break;
            }
        }

        private void SimulateKeyPress(Keys key)
        {
            isGlobalHookEnable = false; // отключаем глобальный хук, чтобы избежать зацикливания
            keybd_event((byte)key, 0, 0, 0); // key down
            Task.Delay(config.csPressureDuration).Wait();
            keybd_event((byte)key, 0, 2, 0); // key up
            isGlobalHookEnable = true;  // включаем глобальный хук обратно
        }

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }
    }
}
