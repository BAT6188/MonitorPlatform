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
using MonitorPlatform.Data;
using DevExpress.Xpf.Charts;
using System.Windows.Controls.Primitives;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// TrafficRight.xaml 的交互逻辑
    /// </summary>
    public partial class TrafficRight : Page
    {
        public TrafficRight()
        {
            InitializeComponent();
            grid.ItemsSource = MonitorDataModel.Instance().CurrrentLine.History_Stations;
            grid.View.FocusedRowChanged += new DevExpress.Xpf.Grid.FocusedRowChangedEventHandler(View_FocusedRowChanged);
            grid.View.FocusedRowHandle = 0;
        }

        void View_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
          
            //string name = griddetail.View.FocusedRowData.CellData[0].Value.ToString();
            if (e.NewRow == null)
            {
                return;
            }
            HistoryStation s = e.NewRow as HistoryStation; //line.Stations.SingleOrDefault(x => x.Name == name);
            if (s != null)
            {
                stationInoutChart.ItemsSource = s.StationInOut;
                detailchart.DataSource = s.Personrates;
                //stationTimeOutChart.DataSource = s.Personrates;
                int lineid = 0;
                if (chkLine.IsChecked.HasValue)
                {
                    lineid = chkLine.IsChecked.Value ? 0 : 1;
                }
                DataCenter.Instance.UpdateTrafficRight_DetailStation(DataCenter.Instance.SelectTime, lineid, s.StaGUID);
            }
        }


        private void chkLine_Click(object sender, RoutedEventArgs e)
        {
            if (grid == null)
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
                grid.ItemsSource = MonitorDataModel.Instance().SubWayLines[0].History_Stations;
            }
            else
            {
                grid.ItemsSource = MonitorDataModel.Instance().SubWayLines[1].History_Stations;
            }
            grid.View.FocusedRowHandle = 0;
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
