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

namespace MonitorPlatform.Controls
{
    /// <summary>
    /// TabSelectControl.xaml 的交互逻辑
    /// </summary>
    public partial class TabSelectControl : UserControl
    {
        public TabSelectControl()
        {
            InitializeComponent();
        }

        private void TabSelectControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new MenuItems();
        }
    }
}
