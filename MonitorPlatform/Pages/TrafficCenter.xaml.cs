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
using MonitorPlatform.Data;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// TrafficCenter.xaml 的交互逻辑
    /// </summary>
    public partial class TrafficCenter : Page
    {
        public TrafficCenter()
        {
            InitializeComponent();
            DateTime temp = DateTime.Now;
            datePicker1.SelectedDate = temp;
            DataCenter.Instance.SelectTime = temp;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            chart.Animate();
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker1.SelectedDate.HasValue)
            {
                DataCenter.Instance.SelectTime = datePicker1.SelectedDate.Value;
                DataCenter.Instance.UpdateTrafficCenter(datePicker1.SelectedDate.Value);
                DataCenter.Instance.UpdateTrafficRight(datePicker1.SelectedDate.Value,0);
                DataCenter.Instance.UpdateTrafficRight(datePicker1.SelectedDate.Value, 1);

                //Defualt, Open month report
                DataCenter.Instance.UpdateTrafficCenterReport(datePicker1.SelectedDate.Value, MonitorPlatform.Data.DataCenter.QueryType.Month);
            }
        }

        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker1.SelectedDate.HasValue)
            {
                DataCenter.Instance.UpdateTrafficCenterReport(datePicker1.SelectedDate.Value,MonitorPlatform.Data.DataCenter.QueryType.Month);
            }
        }
        private void btnQuator_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker1.SelectedDate.HasValue)
            {
                DataCenter.Instance.UpdateTrafficCenterReport(datePicker1.SelectedDate.Value, MonitorPlatform.Data.DataCenter.QueryType.Quarter);
            }
        }
        private void btnYear_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker1.SelectedDate.HasValue)
            {
                DataCenter.Instance.UpdateTrafficCenterReport(datePicker1.SelectedDate.Value, MonitorPlatform.Data.DataCenter.QueryType.Year);
            }

        }
        private void btnAll_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker1.SelectedDate.HasValue)
            {
                DataCenter.Instance.UpdateTrafficCenterReport(datePicker1.SelectedDate.Value, MonitorPlatform.Data.DataCenter.QueryType.All);
            }
        }
        private void btnAddup_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker1.SelectedDate.HasValue)
            {
                DataCenter.Instance.UpdateTrafficCenterReport(datePicker1.SelectedDate.Value, MonitorPlatform.Data.DataCenter.QueryType.Addup);
            }
        }
    }
}
