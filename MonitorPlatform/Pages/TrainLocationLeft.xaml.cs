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
using System.Windows.Controls.Primitives;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// TrainLocationLeft.xaml 的交互逻辑
    /// </summary>
    public partial class TrainLocationLeft : Page
    {
        public TrainLocationLeft()
        {
            InitializeComponent();
        }

        void chart_MouseMove(object sender, MouseEventArgs e)
        {
            
                pointTooltip.Placement = PlacementMode.RelativePoint;
                pointTooltip.PlacementTarget = line1;
                pointTooltip.HorizontalOffset = 0;
                pointTooltip.VerticalOffset = 0;
                pointTooltip.IsOpen = true;
                Cursor = Cursors.Hand;
           
        }
        void chart_MouseLeave(object sender, MouseEventArgs e)
        {
            pointTooltip.IsOpen = false;
        }
    }
}
