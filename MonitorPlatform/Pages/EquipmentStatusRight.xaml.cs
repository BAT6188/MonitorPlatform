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
    /// EquipmentStatusRight.xaml 的交互逻辑
    /// </summary>
    public partial class EquipmentStatusRight : Page
    {

        Dictionary<string, EquipPoint> points = new Dictionary<string, EquipPoint>();
        public EquipmentStatusRight()
        {
            points["8f621ffc-65a9-40a3-9057-c96ef217d3da"] = new EquipPoint() { location = new Point(600, 550), pictype = "点型火灾探测器-{0}.png" };
            points["6aa5935d-b1ed-4681-a73e-f47e7a989937"] = new EquipPoint() { location = new Point(640, 550), pictype = "气体灭火控制器-{0}.png" };

            points["75d1d61c-be54-4fd0-b7f5-962b46ead3fe"] = new EquipPoint() { location = new Point(710, 530), pictype = "气体灭火控制器-{0}.png" };
            points["3389af01-d05f-4382-97a8-b1c426657b7d"] = new EquipPoint() { location = new Point(770, 530), pictype = "点型火灾探测器-{0}.png" }; 


            InitializeComponent();
            this.Loaded += new RoutedEventHandler(EquipmentStatusRight_Loaded);
        }

        void EquipmentStatusRight_Loaded(object sender, RoutedEventArgs e)
        {
            Station station = MonitorDataModel.Instance().CurrentStation;
            DrawEquipment(station);

        }
        void DrawSignleEquipment(EquipPoint point, double widthfactor, double heightfactor)
        {
            Image image = new Image();

            image.Stretch = Stretch.None;
            BitmapImage bit= new BitmapImage(new Uri("/MonitorPlatform;component/Resource/"+point.picsrc, UriKind.RelativeOrAbsolute));
            image.Width = 40;
            image.Height = 40;
            image.Source = bit;
            Canvas.SetLeft(image, point.location.X * widthfactor);
            Canvas.SetTop(image, point.location.Y * heightfactor);
            infoborder.Children.Add(image);
        }
        void DrawEquipment(Station station)
        {
            IEnumerable<Equipment> eqips = station.Equipments.Where(x => x.Status == "预警");
            foreach (var p in points.Keys)
            {
                EquipPoint eqi = points[p];
                if (eqips.FirstOrDefault(x => x.GUID == p) != null)
                {
                    eqi.picsrc = string.Format(eqi.pictype, 2);
                }
                else
                {
                    eqi.picsrc = string.Format(eqi.pictype, 1);
                }
            }

            int orign_height = 739;
            int orign_width = 1282;
            double widthfactor = infoborder.ActualWidth / orign_width;
            double heightfactor = infoborder.ActualHeight / orign_height;

            infoborder.Children.Clear();
            foreach (var p in points)
            {
                DrawSignleEquipment(p.Value, widthfactor, heightfactor);
            }
        }
    }
    public class EquipPoint
    {
        public Point location;
        public string pictype;
        public string picsrc;
    }
}
