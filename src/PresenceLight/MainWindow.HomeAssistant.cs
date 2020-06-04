using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresenceLight
{

    public partial class MainWindow : Window
    {
        #region Home Assistant

        private async void SaveHASettings_Click(object sender, RoutedEventArgs e)
        {
            //btnHue.IsEnabled = false;
            await SettingsService.SaveSettings(Config);
            //_hueService = new HueService(Config);
            CheckHueSettings();
            lblHueSaved.Visibility = Visibility.Visible;
            btnHue.IsEnabled = true;
        }
        #endregion
    }
}
