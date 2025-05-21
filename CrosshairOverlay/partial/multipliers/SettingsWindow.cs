using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrosshairOverlay
{
    // Only Multipliers checkbox handlers
    public partial class SettingsWindow
    {
        private int MultiplierIsChecked()
        {
            int multiplier = 1;
            if (CommonMultiplier_x10.IsChecked == true) multiplier *= 10;
            if (CommonMultiplier_x100.IsChecked == true) multiplier *= 100;
            return multiplier;
        }

        private void CommonMultiplier_x10_Checked(object sender, RoutedEventArgs e)
        {
            CommonMultiplier_x10_custom.IsChecked = true;
        }

        private void CommonMultiplier_x100_Checked(object sender, RoutedEventArgs e)
        {
            CommonMultiplier_x100_custom.IsChecked = true;
        }

        private void CommonMultiplier_x10_custom_Checked(object sender, RoutedEventArgs e)
        {
            CommonMultiplier_x10.IsChecked = true;
        }

        private void CommonMultiplier_x100_custom_Checked(object sender, RoutedEventArgs e)
        {
            CommonMultiplier_x100.IsChecked = true;
        }

        private void CommonMultiplier_x10_Unchecked(object sender, RoutedEventArgs e)
        {
            CommonMultiplier_x10_custom.IsChecked = false;
        }

        private void CommonMultiplier_x100_Unchecked(object sender, RoutedEventArgs e)
        {
            CommonMultiplier_x100_custom.IsChecked = false;
        }

        private void CommonMultiplier_x10_custom_Unchecked(object sender, RoutedEventArgs e)
        {
            CommonMultiplier_x10.IsChecked = false;
        }

        private void CommonMultiplier_x100_custom_Unchecked(object sender, RoutedEventArgs e)
        {
            CommonMultiplier_x100.IsChecked = false;
        }
    }
}
