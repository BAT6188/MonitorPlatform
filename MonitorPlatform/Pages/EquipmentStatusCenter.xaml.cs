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
using DevExpress.Xpf.Grid;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// EquipmentStatusCenter.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentStatusCenter : Page
    {
        public EquipmentStatusCenter()
        {
            InitializeComponent();
           
            gridStation.ItemsSource = MonitorDataModel.Instance().SubWayLines[0].Stations;
            gridStation.View.FocusedRowChanged += new DevExpress.Xpf.Grid.FocusedRowChangedEventHandler(View_FocusedRowChanged);
            chk_AFC.Click += new RoutedEventHandler(chk_AFC_Click);
            chk_FAS.Click += new RoutedEventHandler(chk_AFC_Click);
            chk_BAS.Click += new RoutedEventHandler(chk_AFC_Click);
            chk_PSCADA.Click += new RoutedEventHandler(chk_AFC_Click);
            chk_PSD.Click += new RoutedEventHandler(chk_AFC_Click);
            chk_PIS.Click += new RoutedEventHandler(chk_AFC_Click);
            chk_PA.Click += new RoutedEventHandler(chk_AFC_Click);
           
        }

        void chk_AFC_Click(object sender, RoutedEventArgs e)
        {
            string condition = GetSelectChk();
            object data = gridStation.View.FocusedRow;
            if (data != null & data is Station)
            {
                Station s = data as Station; //line.Stations.SingleOrDefault(x => x.Name == name);
                if (s != null)
                {
                    griddetail.ItemsSource = s.Equipments.Where(x => condition.Contains(x.EquipmentType));

                }
            }
        }

        void View_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            int lineid = 0;
            if (chkLine.IsChecked.HasValue)
            {
                if (chkLine.IsChecked.Value)
                {
                    lineid = 0;
                }
                else
                {
                    lineid = 1;
                }
            }
            //string name = griddetail.View.FocusedRowData.CellData[0].Value.ToString();
            if (e.NewRow == null)
            {
                return;
            }
            Station s = e.NewRow as Station; //line.Stations.SingleOrDefault(x => x.Name == name);
            if (s != null)
            {
                DataCenter.Instance.UpdateEquipmentDetailCenter(s.StaGUID, GetSelectChk(), lineid);

                griddetail.ItemsSource = s.Equipments;
               
            }
        }


        private string GetSelectChk()
        {
            string chkstatus = "";
            if (chk_AFC.IsChecked.Value)
            {
                chkstatus += "AFC,";
            }
            if (chk_FAS.IsChecked.Value)
            {
                chkstatus += "FAS,";
            }
            if (chk_BAS.IsChecked.Value)
            {
                chkstatus += "BAS,";
            }
            if (chk_PSCADA.IsChecked.Value)
            {
                chkstatus += "PSCADA,";

            }
            if (chk_PSD.IsChecked.Value)
            {
                chkstatus += "PSD,";
            }
            if (chk_PIS.IsChecked.Value)
            {
                chkstatus += "PIS,";
            }
            if (chk_PA.IsChecked.Value)
            {
                chkstatus += "PA,";
            }

            if (!string.IsNullOrEmpty(chkstatus))
            {
                chkstatus = chkstatus.Trim(',');
            }

            return chkstatus;
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
