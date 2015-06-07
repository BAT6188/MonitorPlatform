using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using MonitorPlatform.ViewModel;

namespace MonitorPlatform.Controls
{
    public class LineStatusControl : UserControl
    {
        protected override void OnInitialized(EventArgs e)
        {
            this.DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(LineStatusControl_DataContextChanged);
            base.OnInitialized(e);
        }

        void LineStatusControl_DataContextChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext != null && this.DataContext is ObservableCollection<StationTroubleStatus>)
            {
                UpdateStation(this.DataContext as ObservableCollection<StationTroubleStatus>, StationCount);
            }
            
        }
        public virtual int StationCount
        {
            get
            {
                return 24;
            }
        }

        public virtual void UpdateStation(ObservableCollection<StationTroubleStatus> stat, int numbers)
        { 
            SolidColorBrush yellow = new SolidColorBrush();
            
            yellow.Color = Color.FromRgb(255, 153, 0);

            SolidColorBrush blue = new SolidColorBrush();

            blue.Color = Color.FromRgb(0, 71, 157);

            for (int i = 0; i < numbers; i++)
            {
                (this.FindName("s" + (i+1).ToString()) as System.Windows.Shapes.Path).Fill = blue;
            }
            for(int i=0;i <stat.Count; i++){
                (this.FindName("s" + (i+1).ToString()) as System.Windows.Shapes.Path).Fill = yellow;
            }
            
        }
    }
}
