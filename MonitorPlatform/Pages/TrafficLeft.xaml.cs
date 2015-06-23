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
    /// TrafficLeft.xaml 的交互逻辑
    /// </summary>
    public partial class TrafficLeft : Page
    {
        //int[] pointLine2X = {584,556,515,514,513,513,484,449,461,471,417,369,387,404,410,426,436,465,500,517,576,632};
        //int[] pointLine2Y = {9,37,37,93,145,193,224,247,288,327,340,352,411,462,492,531,569,591,614,656,657,657};
        //int[] pointLine1X = {11,65,119,138,141,181,233,292,346,391,444,493,546,597,658,711,768,827,867,915,955,995,1035,1079};
        //int[] pointLine1Y = { 632,623,617,561,506,467,456,445,432,426,416,404,394,383,372,360,350,338,302,299,283,268,253,233 };

        
        //int[] pointLine2XCal = new int[22];
        //int[] pointLine2YCal = new int[22];
        //int[] pointLine1XCal = new int[24];
        //int[] pointLine1YCal = new int[24];

        int area = 3;
        public TrafficLeft()
        {
            InitializeComponent();
            //this.SizeChanged += new SizeChangedEventHandler(TrafficLeft_SizeChanged);
            //this.trafficcanvas.MouseMove += new MouseEventHandler(trafficcanvas_MouseMove);

            for (int i = 1; i <= 24;i++ )
            {
                Canvas canvas = (Canvas)this.FindName("S1_" + i.ToString());
                canvas.Cursor = Cursors.Hand;
                canvas.MouseEnter += new MouseEventHandler(subway1_MouseEnter);
                canvas.MouseLeave += new MouseEventHandler(canvas_MouseLeave);
            }
            for (int i = 1; i <= 22; i++)
            {
                Canvas canvas = (Canvas)this.FindName("S2_" + i.ToString());
                canvas.Cursor = Cursors.Hand;
                canvas.MouseEnter += new MouseEventHandler(subway2_MouseEnter);
                canvas.MouseLeave += new MouseEventHandler(canvas_MouseLeave);
            }
        }

        void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            stationinfo.IsOpen = false;
            stationinfo.PlacementTarget = sender as UIElement;
        }

        void subway1_MouseEnter(object sender, MouseEventArgs e)
        {
            UpdateStationInfor(sender, 0);
        }

        void subway2_MouseEnter(object sender, MouseEventArgs e)
        {
            UpdateStationInfor(sender, 1);
        }

        void UpdateStationInfor(object sender, int subway)
        {
            stationinfo.IsOpen = true;
            stationinfo.PlacementTarget = sender as UIElement;
            SubLine line = MonitorDataModel.Instance().SubWayLines[subway];
            string name = (sender as Canvas).Name;
            int index = int.Parse(name.Substring(name.IndexOf("_") + 1));
            Station s = line.Stations[index - 1];
            stationName.Text = s.Name;
            inNumber.Text = s.InNumber.ToString();
            outNumber.Text = s.OutNumber.ToString();

            if (subway == 0)
            {
                sublinename.Text = "1";
                sublineBorder.Background = new SolidColorBrush(Colors.Green);
            }
            else
            {
                sublinename.Text = "2";
                sublineBorder.Background = new SolidColorBrush(Colors.Red);
            }
            
        }


     
    }
}
