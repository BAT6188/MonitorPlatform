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
using DevExpress.Xpf.Core.Commands;

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
            view.Tag = new DelegateCommand<object>(OnOpenDetail);   
            gridStation.View.FocusedRowChanged += new DevExpress.Xpf.Grid.FocusedRowChangedEventHandler(View_FocusedRowChanged);
            gridStation.View.FocusedRowHandle = 0;
            chkLine_Click(this, new RoutedEventArgs());
        }

        void OnOpenDetail(object parameter)
        {
            equicode.Text = parameter.ToString();
            carmerainfo.IsOpen = true;
            
        }

        void View_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {

            //string name = griddetail.View.FocusedRowData.CellData[0].Value.ToString();
            if (e.NewRow == null)
            {
                return;
            }
            Station s = e.NewRow as Station; //line.Stations.SingleOrDefault(x => x.Name == name);
            if (s != null)
            {
                stationname.Text = s.Name;
                stationtotal.Text = s.Cameras.Count.ToString();
                stationwarn.Text = s.Cameras.Count(x => x.Status == "异常").ToString();
                griddetail.ItemsSource = s.Cameras;
            }
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
            gridStation.View.FocusedRowHandle = 0;
        }

        private void btnClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            carmerainfo.IsOpen = false;
        }
    }
}
