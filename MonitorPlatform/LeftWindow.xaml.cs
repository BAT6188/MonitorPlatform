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
using MonitorPlatform.Controls;
using MonitorPlatform.Data;

namespace MonitorPlatform
{
   
    /// <summary>
    /// LeftWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LeftWindow : Window
    {
        RightWindows right;
        CenterWindow center;
        
        public LeftWindow()
        {
            InitializeComponent();
            right = new RightWindows();
            center = new CenterWindow();
            right.Show();
            center.Show();
            DataCenter.Instance.Inital(this);
            DataCenter.Instance.Login();
            DataCenter.Instance.UpdateBoss();
            DataCenter.Instance.UpdateEquipmentLeft();
            DataCenter.Instance.UpdateEquipmentCenter();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
          
            this.DataContext = MonitorDataModel.Instance();
        }

        private void TabSelectControl_TabSelectChanged(object sender, TabSelectChangeEventArgs e)
        {
            switch (e.ChoosedTab)
            {
                case "总裁界面":
                    this.frame.Source = new Uri("Pages/BossLeft.xaml", UriKind.Relative);
                    center.frame.Source = new Uri("Pages/BossCenter.xaml", UriKind.Relative);
                    right.frame.Source = new Uri("Pages/BossRight.xaml", UriKind.Relative);
                    break;
                case "客流信息":
                    this.frame.Source = new Uri("Pages/TrafficLeft.xaml", UriKind.Relative);
                    center.frame.Source = new Uri("Pages/TrafficCenter.xaml", UriKind.Relative);
                    right.frame.Source = new Uri("Pages/TrafficRight.xaml", UriKind.Relative);
                    break;
                case "列车位置":
                    this.frame.Source = new Uri("Pages/TrainLocationLeft.xaml", UriKind.Relative);
                    center.frame.Source = new Uri("Pages/TrainLocationCenter.xaml", UriKind.Relative);
                    right.frame.Source = new Uri("Pages/BossRight.xaml", UriKind.Relative);
                    break;
                case "设施设备":
                    this.frame.Source = new Uri("Pages/EquipmentStatusLeft.xaml",UriKind.Relative);
                    center.frame.Source = new Uri("Pages/EquipmentStatusCenter.xaml", UriKind.Relative);
                    right.frame.Source = new Uri("Pages/BossRight.xaml",UriKind.Relative);
                    break;
                case "视频监控":
                    //this.frame.Source = new Uri("Pages/BossLeft.xaml,UriKind.Relative");
                    //center.frame.Source = new Uri("Pages/BossCenter.xaml",UriKind.Relative);
                    //right.frame.Source = new Uri("Pages/BossRight.xaml",UriKind.Relative);
                    break;
                case "地理信息":
                    //this.frame.Source = new Uri("Pages/BossLeft.xaml,UriKind.Relative");
                    //center.frame.Source = new Uri("Pages/BossCenter.xaml",UriKind.Relative);
                    //right.frame.Source = new Uri("Pages/BossRight.xaml",UriKind.Relative);
                    break;

            }
        }
    }
}

