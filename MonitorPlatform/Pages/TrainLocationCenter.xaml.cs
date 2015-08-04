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
using MonitorPlatform.Data;
using System.Windows.Resources;
using System.IO;
using MonitorPlatform.ViewModel;
using System.Windows.Controls.Primitives;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// TrainLocationCenter.xaml 的交互逻辑
    /// </summary>
    public partial class TrainLocationCenter : Page
    {

        Dictionary<string, Point> points = new Dictionary<string, Point>();
        public TrainLocationCenter()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(TrainLocationCenter_Loaded);
            LoadPoints();
            DataCenter.Instance.UpdateTrainLocationEvent += new DataCenter.UpdateTrainLocation(Instance_UpdateTrainLocationEvent);
            this.SizeChanged += new SizeChangedEventHandler(TrainLocationCenter_SizeChanged);
            MonitorDataModel.Instance().CurrentTrainChangedEvent += new MonitorDataModel.CurrentTrainChanged(TrainLocationCenter_CurrentTrainChangedEvent);
        }

        void TrainLocationCenter_CurrentTrainChangedEvent()
        {
            SetPropByCurrentTrain();
        }

        public void SetPropByCurrentTrain()
        {
            Train t = MonitorDataModel.Instance().CurrentTrain;

            if (t != null)
            {
                DateTime now = DateTime.Now;
                txtCurrentDate.Text = now.ToString("yyyy-MM-dd");

                SubLine line = MonitorDataModel.Instance().SubWayLines[t.LineNo];
                stationGrid.ItemsSource = line.Stations;
                double nextstationID;
                if (t.IsDown)
                {
                    if (line.Stations.Count > (int)t.Location && (int)t.Location >= 0)
                    {
                        Station next = line.Stations[(int)t.Location];
                        this.txtNextStation.Text = next.Name;
                        this.txtEstimateArrivaTime.Text = now.AddMinutes(next.DownFirstTime).ToString("HH:mm");
                        this.txtPlanArriveTime.Text = this.txtEstimateArrivaTime.Text;
                    }
                    else
                    {
                        this.txtNextStation.Text = "终点站";
                        this.txtPlanArriveTime.Text = now.ToString("HH:mm");
                        this.txtEstimateArrivaTime.Text = now.ToString("HH:mm");
                    }

                }
                else
                {
                    nextstationID = t.Location - 2;
                    if (line.Stations.Count > (int)(t.Location - 2) && (int)(t.Location - 2) >= 0)
                    {
                        Station next = line.Stations[(int)t.Location - 2];
                        this.txtNextStation.Text = next.Name;
                        this.txtEstimateArrivaTime.Text = now.AddMinutes(next.UpFirstTime).ToString("HH:mm");
                        this.txtPlanArriveTime.Text = this.txtEstimateArrivaTime.Text;
                    }
                    else
                    {
                        this.txtNextStation.Text = "终点站";
                        this.txtPlanArriveTime.Text = now.ToString("HH:mm");
                        this.txtEstimateArrivaTime.Text = now.ToString("HH:mm");
                    }
                }

            }
        }


        void TrainLocationCenter_Loaded(object sender, RoutedEventArgs e)
        {

            SetPropByCurrentTrain();
            Window parentwin = Window.GetWindow(this);
            parentwin.LocationChanged += new EventHandler(parentwin_LocationChanged);
            parentwin.SizeChanged += new SizeChangedEventHandler(parentwin_SizeChanged);
           
        }
        void parentwin_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }
        void parentwin_LocationChanged(object sender, EventArgs e)
        {
           
        }

        public void LoadPoints()
        {
            StreamResourceInfo info = Application.GetResourceStream(new Uri("/MonitorPlatform;component/Resource/Points.txt", UriKind.RelativeOrAbsolute));
            StreamReader reader = new StreamReader(info.Stream);
            points.Clear();
            while (!reader.EndOfStream)
            {

                string output = reader.ReadLine();
                if (!string.IsNullOrEmpty(output))
                {
                    string[] contents = output.Split(',');
                    if (contents.Length == 3)
                    {
                        points.Add(contents[0], new Point(int.Parse(contents[1]), int.Parse(contents[2])));
                    }
                }
            }

        }

        void Instance_UpdateTrainLocationEvent()
        {
            //ReCalculateAll();
        }

        void TrainLocationCenter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //ReCalculateAll();

        }
    }

}

