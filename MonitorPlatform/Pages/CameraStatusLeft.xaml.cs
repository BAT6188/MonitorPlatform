using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MonitorPlatform.ViewModel;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// CameraStatusLeft.xaml 的交互逻辑
    /// </summary>
    public partial class CameraStatusLeft : Page
    {
        public CameraStatusLeft()
        {
            InitializeComponent();
            chkLine_Click(this, new RoutedEventArgs());
        }


        private void chkLine_Click(object sender, RoutedEventArgs e)
        {
            if (gridStation == null)
            {
                return;
            }
            bool isfirstline = true;
            if (chkLine.IsChecked.HasValue)
            {
                isfirstline = chkLine.IsChecked.Value;
            }

            if (isfirstline)
            {
                gridStation.ItemsSource = MonitorDataModel.Instance().SubWayLines[0].Stations;
            }
            else
            {
                gridStation.ItemsSource = MonitorDataModel.Instance().SubWayLines[1].Stations;
            }
        }
    }
}
