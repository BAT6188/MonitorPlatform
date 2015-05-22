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
    /// BossRight.xaml 的交互逻辑
    /// </summary>
    public partial class BossRight : Page
    {
        public BossRight()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(BossRight_Loaded);
        }

        void BossRight_Loaded(object sender, RoutedEventArgs e)
        {
            videoControl.SetLayout(4);
        }
       
    }
}
