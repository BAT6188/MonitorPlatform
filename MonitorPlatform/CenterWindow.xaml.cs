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
using System.Windows.Shapes;
using MonitorPlatform.ViewModel;
using MonitorPlatform.Data;

namespace MonitorPlatform
{
    /// <summary>
    /// CenterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CenterWindow : Window
    {
        public CenterWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //this.WindowState = WindowState.Maximized;
            //this.WindowStyle = System.Windows.WindowStyle.None;
            this.DataContext = MonitorDataModel.Instance();
           
        }
    }
}
