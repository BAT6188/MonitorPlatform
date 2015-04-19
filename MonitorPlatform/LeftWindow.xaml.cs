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

namespace MonitorPlatform
{
    /// <summary>
    /// LeftWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LeftWindow : Window
    {
        public LeftWindow()
        {
            InitializeComponent();
            //RightWindows right = new RightWindows();
            //CenterWindow center = new CenterWindow();
            //right.Show();
            //center.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            this.DataContext = new MonitorDataModel();
        }
    }
}
