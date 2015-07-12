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
using DevExpress.Xpf.Charts;
using System.Windows.Controls.Primitives;

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
            rdMonth.IsChecked = true;
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
                DataCenter.Instance.UpdateTrafficRight(datePicker1.SelectedDate.Value, 0);
                DataCenter.Instance.UpdateTrafficRight(datePicker1.SelectedDate.Value, 1);

                //Defualt, Open month report
                DataCenter.Instance.UpdateTrafficCenterReport(datePicker1.SelectedDate.Value, MonitorPlatform.Data.DataCenter.QueryType.Month);
            }
        }

        private void btnMonth_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker1.SelectedDate.HasValue)
            {
                DataCenter.Instance.UpdateTrafficCenterReport(datePicker1.SelectedDate.Value, MonitorPlatform.Data.DataCenter.QueryType.Month);
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

        void chart_MouseMove(object sender, MouseEventArgs e)
        {
            ChartControl orgchart = sender as ChartControl;
            Point position = e.GetPosition(orgchart);
            ChartHitInfo hitInfo = orgchart.CalcHitInfo(position);
            if (hitInfo != null && hitInfo.SeriesPoint != null)
            {
                ttContent.Text = string.Format("时间 = {0}\n人数 = {1}",
                       hitInfo.SeriesPoint.Argument, Math.Round(hitInfo.SeriesPoint.NonAnimatedValue, 2));
                pointTooltip.Placement = PlacementMode.RelativePoint;
                pointTooltip.PlacementTarget = orgchart;
                pointTooltip.HorizontalOffset = position.X + 5;
                pointTooltip.VerticalOffset = position.Y + 5;
                pointTooltip.IsOpen = true;
                Cursor = Cursors.Hand;
            }
            else
            {
                pointTooltip.IsOpen = false;
                Cursor = Cursors.Arrow;
            }
        }
        void chart_MouseLeave(object sender, MouseEventArgs e)
        {
            pointTooltip.IsOpen = false;
        }
    }
}
