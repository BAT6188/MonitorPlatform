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
    /// TrainStatusControl.xaml 的交互逻辑
    /// </summary>
    public partial class TrainDownStatusControl : UserControl
    {
        public TrainDownStatusControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty StationStatusProperty =
       DependencyProperty.Register("StationStatus", typeof(int),
       typeof(TrainDownStatusControl));
        public int StationStatus
        {
            get { return (int)GetValue(StationStatusProperty); }

            set { SetValue(StationStatusProperty, value); }
        }

        public static readonly DependencyProperty StationNameProperty =
     DependencyProperty.Register("StationName", typeof(string),
     typeof(TrainDownStatusControl));
        public string StationName
        {
            get { return (string)GetValue(StationNameProperty); }

            set {
                SetValue(StationNameProperty, value); 
            
            }
        }

        public static readonly DependencyProperty TimeNameProperty =
  DependencyProperty.Register("Time", typeof(string),
  typeof(TrainDownStatusControl));
        public string Time
        {
            get { return (string)GetValue(TimeNameProperty); }

            set { SetValue(TimeNameProperty, value); }
        }
    }
}
