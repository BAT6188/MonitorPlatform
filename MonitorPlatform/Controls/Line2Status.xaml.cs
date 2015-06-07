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

namespace MonitorPlatform.Controls
{
    /// <summary>
    /// Line2Status.xaml 的交互逻辑
    /// </summary>
    public partial class Line2Status : LineStatusControl
    {
        public Line2Status()
        {
            InitializeComponent();
        }

        public override int StationCount
        {
            get
            {
                return 22;
            }
        }
    }
}
