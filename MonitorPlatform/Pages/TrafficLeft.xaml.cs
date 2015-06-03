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
        int[] pointLine2X = {584,556,515,514,513,513,484,449,461,471,417,369,387,404,410,426,436,465,500,517,576,632};
        int[] pointLine2Y = {9,37,37,93,145,193,224,247,288,327,340,352,411,462,492,531,569,591,614,656,657,657};
        int[] pointLine1X = {11,65,119,138,141,181,233,292,346,391,444,493,546,597,658,711,768,827,867,915,955,995,1035,1079};
        int[] pointLine1Y = { 632,623,617,561,506,467,456,445,432,426,416,404,394,383,372,360,350,338,302,299,283,268,253,233 };

        
        int[] pointLine2XCal = new int[22];
        int[] pointLine2YCal = new int[22];
        int[] pointLine1XCal = new int[24];
        int[] pointLine1YCal = new int[24];

        int area = 3;
        public TrafficLeft()
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(TrafficLeft_SizeChanged);
            this.trafficcanvas.MouseMove += new MouseEventHandler(trafficcanvas_MouseMove);
        }

        void trafficcanvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point p= e.GetPosition(trafficcanvas);
            int line = 0;
            int station = 0;
            if (GetHitPosition(p,ref line, ref station))
            {
                if (this.DataContext != null)
                {
                    MonitorDataModel data = this.DataContext as MonitorDataModel;
                    if (line == 1)
                    {
                        sublinename.Text = "1";
                        sublineBorder.Background = new SolidColorBrush(Colors.Green);
                    }
                    else
                    {
                        sublinename.Text = "2";
                        sublineBorder.Background = new SolidColorBrush(Colors.Red);
                    }
                    if (line == 1)
                    {
                        station = 23 - station;
                    }
                    Station s = data.SubWayLines[line - 1].Stations[station];
                    stationName.Text = s.Name;
                    inNumber.Text = s.InNumber.ToString();
                    outNumber.Text = s.OutNumber.ToString();
                }
            }
        }

        bool GetHitPosition(Point p ,ref int  line, ref int station)
        { 
           for (int i = 0; i < pointLine1X.Length;i++ )
            {
                if (pointLine1XCal[i]  -area < p.X
                    && pointLine1XCal[i] + area > p.X)
                {
                    if (pointLine1YCal[i] - area < p.Y
                     && pointLine1YCal[i] + area > p.Y)
                    {
                        line = 1;
                        station = i;
                        return true;
                    }
                }
            }
           for (int i = 0; i < pointLine2X.Length; i++)
           {
               if (pointLine2XCal[i] - area < p.X
                   && pointLine2XCal[i] + area > p.X)
               {
                   if (pointLine2YCal[i] - area < p.Y
                    && pointLine2YCal[i] + area > p.Y)
                   {
                       line = 2;
                       station = i;
                       return true;
                   }
               }
           }
           return false;
        }

        void TrafficLeft_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReCalculateAll();
        }


        private double widthfactor;
        private double heightfactor;
        int compareOrignwidth =  1127;
        int compareOrginheight = 681;
        int orignwidth = 1415;
        int orginheight = 856;

        public void ReCalculateAll()
        {
            widthfactor = trafficcanvas.ActualWidth / orignwidth; 
            heightfactor = trafficcanvas.ActualHeight / orginheight;
            double orgw = orignwidth / (double)compareOrignwidth;
            double orgh = orginheight / (double)compareOrginheight;
            for (int i = 0; i < pointLine1X.Length; i++)
            {
                pointLine1XCal[i] = (int)(orgw * pointLine1X[i] * widthfactor);
                pointLine1YCal[i] = (int)(orgh * pointLine1Y[i] * heightfactor);
            }
            for (int i = 0; i < pointLine2X.Length; i++)
            {
                pointLine2XCal[i] = (int)(orgw * pointLine2X[i] * widthfactor);
                pointLine2YCal[i] = (int)(orgh * pointLine2Y[i] * heightfactor);
            }
        }
    }
}
