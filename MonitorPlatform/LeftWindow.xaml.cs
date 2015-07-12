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
using System.Threading;


namespace MonitorPlatform
{
   
    /// <summary>
    /// LeftWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LeftWindow : Window
    {
        RightWindows right;
        CenterWindow center;
        Timer timer;
        DateTime lastupdate_traffic;
        DateTime lastupdate_boss;
        public LeftWindow()
        {
            InitializeComponent();
            right = new RightWindows();
            center = new CenterWindow();
            this.txtTime.Text = System.DateTime.Now.ToString("yyyy年MM月dd日 dddd");
            this.txtName.Text = DataCenter.Instance.LoginUser;
            System.Windows.Forms.Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            WindowManager.Instance.left = this;
            WindowManager.Instance.center = center;
            WindowManager.Instance.right = right;

            if (screens.Length == 3)
            {
                this.Top = screens[0].WorkingArea.Top;
                this.Left = screens[0].WorkingArea.Left;
                //this.WindowState = WindowState.Maximized;
                //this.WindowStyle = System.Windows.WindowStyle.None;

                center.Top = screens[1].WorkingArea.Top;
                center.Left = screens[1].WorkingArea.Left;
                //center.WindowState = WindowState.Maximized;
                //center.WindowStyle = System.Windows.WindowStyle.None;


                right.Top = screens[2].WorkingArea.Top;
                right.Left = screens[2].WorkingArea.Left;
                //right.WindowState = WindowState.Maximized;
                //right.WindowStyle = System.Windows.WindowStyle.None;
            }
            right.Show();
            center.Show();
            DataCenter.Instance.Inital(this);

            //DataCenter.Instance.Login("test1", "1");
            DataCenter.Instance.UpdateBoss();
            DataCenter.Instance.UpdateTrafficLeft();
            DataCenter.Instance.UpdaeTrainLocationLeft();
            
            DataCenter.Instance.UpdateEquipmentLeft();
            DataCenter.Instance.UpdateEquipmentCenter();
            DataCenter.Instance.UpdateCameraInfo();
            
             lastupdate_traffic = DateTime.Now;
             lastupdate_boss = DateTime.Now;
            //15秒,触发一次
             timer = new Timer(new TimerCallback(ScheduleTask), null, 15000, 15000);
            
        }

        private void ScheduleTask(object key)
        {
            DateTime current = DateTime.Now;
            if ((current - lastupdate_traffic).Seconds >= 30)
            {
                DataCenter.Instance.UpdateBoss();
                DataCenter.Instance.UpdaeTrainLocationLeft();
                DataCenter.Instance.UpdateEquipmentLeft();

                lastupdate_traffic = current;
            }
            if ((current - lastupdate_boss).Minutes >= 15)
            {
                DataCenter.Instance.UpdateTrafficLeft();
                DataCenter.Instance.UpdateEquipmentCenter();
                DataCenter.Instance.UpdateCameraInfo();
                lastupdate_boss = current;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.DataContext = MonitorDataModel.Instance();
        }

      

        public void ChangeTab(string tab)
        {
            switch (tab)
            {
                case "系统首页":
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
                    center.frame.Source = new Uri("Pages/BossCenter.xaml", UriKind.Relative);
                    right.frame.Source = new Uri("Pages/BossRight.xaml", UriKind.Relative);
                    break;
                case "设施设备":
                    this.frame.Source = new Uri("Pages/EquipmentStatusLeft.xaml", UriKind.Relative);
                    center.frame.Source = new Uri("Pages/EquipmentStatusCenter.xaml", UriKind.Relative);
                    right.frame.Source = new Uri("Pages/BossRight.xaml", UriKind.Relative);
                    break;
                case "视频监控":
                    this.frame.Source = new Uri("Pages/CameraStatusLeft.xaml",UriKind.Relative);
                    center.frame.Source = new Uri("Pages/BossCenter.xaml",UriKind.Relative);
                    right.frame.Source = new Uri("Pages/BossRight.xaml",UriKind.Relative);
                    break;
                case "地理信息":
                    //this.frame.Source = new Uri("Pages/BossLeft.xaml,UriKind.Relative");
                    //center.frame.Source = new Uri("Pages/BossCenter.xaml",UriKind.Relative);
                    //right.frame.Source = new Uri("Pages/BossRight.xaml",UriKind.Relative);
                    break;

            }
            
        }

        

        private void TabSelectControl_TabSelectChanged(object sender, TabSelectChangeEventArgs e)
        {
            ChangeTab(e.ChoosedTab);
        }

        public void ChangeTabByStr(string input)
        {
            tabControl.SelectItem(input);
        
        }



        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Environment.Exit(0);
        }

        private void btnExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
    }
}

