using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace CrosshairOverlay
{
    public partial class SettingsWindow
    {
        // Параметры CounterStrafe
        private bool isCounterStrafeEnabled;
        private int csPressureDuration;

        // Параметры CounterStrafe
        private void AdaptiveDurationCheckbox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AdaptiveDurationCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

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


    }
}
