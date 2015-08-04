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
using System.Windows.Shapes;
using MonitorPlatform.ViewModel;
using MonitorPlatform.Data;
using System.Windows.Resources;
using System.IO;

namespace MonitorPlatform
{
    /// <summary>
    /// CenterWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CenterWindow : Window
    {
        Dictionary<string, Point> points = new Dictionary<string, Point>();
        public CenterWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPoints();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.DataContext = MonitorDataModel.Instance();
            DataCenter.Instance.UpdateTrainLocationEvent += new DataCenter.UpdateTrainLocation(Instance_UpdateTrainLocationEvent);
            this.SizeChanged += new SizeChangedEventHandler(TrainLocationCenter_SizeChanged);
        }
        void Instance_UpdateTrainLocationEvent()
        {
            ReCalculateAll();
        }
        void TrainLocationCenter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReCalculateAll();

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


        public void ReCalculateAll()
        {
            int orign_height = 739;
            int orign_width = 1709;
            double widthfactor = this.ActualWidth / orign_width;
            double heightfactor = this.ActualHeight / orign_height;
            infoborder.Children.Clear();
            foreach (Train train in MonitorDataModel.Instance().SubWayLines[0].Trains)
            {
                DrawTrain(train, widthfactor, heightfactor);
            }
            foreach (Train train in MonitorDataModel.Instance().SubWayLines[1].Trains)
            {
                DrawTrain(train, widthfactor, heightfactor);
            }
        }

        public void DrawTrain(Train train, double widthfactor, double heightfactor)
        {
            if (points.ContainsKey(train.SectionClass))
            {
                Point org = points[train.SectionClass];

                Image image = new Image();
                image.Width = 20;
                image.Height = 20;
                image.Stretch = Stretch.Fill;
                image.Source = new BitmapImage(new Uri("/MonitorPlatform;component/Resource/Car_Normal.png", UriKind.RelativeOrAbsolute));
                Canvas.SetLeft(image, org.X * widthfactor);
                Canvas.SetTop(image, org.Y * heightfactor);
                infoborder.Children.Add(image);
            }

        }


        public void ShowTrafficImage()
        {
            infoborder.Visibility = Visibility.Visible;

        }

        public void CloseTrafficImage()
        {
            infoborder.Visibility = Visibility.Hidden;
        }

          
    }
}
