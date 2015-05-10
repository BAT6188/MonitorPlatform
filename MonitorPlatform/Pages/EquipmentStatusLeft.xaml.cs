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
using MonitorPlatform.Controls;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// EquipmentStatusLeft.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentStatusLeft : Page
    {
        Style bigsection;
        Style bigsectionwithout;
        public EquipmentStatusLeft()
        {
            InitializeComponent();
            bigsection = (Style)Application.Current.TryFindResource("BigPopupSection");
            bigsectionwithout = (Style)Application.Current.TryFindResource("BigPopupWithoutBorderSection");
            line1Border.MouseUp += new MouseButtonEventHandler(line1Border_MouseUp);
            line2Border.MouseUp += new MouseButtonEventHandler(line2Border_MouseUp);
        }

        void line2Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            line1Border.Style = bigsectionwithout;
            line2Border.Style = bigsection;
            txtLineNumber.Text = "2";
            borderLineNumber.Background = new SolidColorBrush(Colors.Red);
            borderLines.Child = new Line2Status();
        }

        void line1Border_MouseUp(object sender, MouseButtonEventArgs e)
        {

            txtLineNumber.Text = "1";
            line1Border.Style = bigsection;
            line2Border.Style = bigsectionwithout;
            borderLineNumber.Background = new SolidColorBrush(Colors.Green);
            borderLines.Child = new Line1Status();
        }
    }
}

