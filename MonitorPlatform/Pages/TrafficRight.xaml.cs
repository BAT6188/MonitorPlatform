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
                stationTimeInChart.DataSource = s.Personrates;
                stationTimeOutChart.DataSource = s.Personrates;
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
    }
}
