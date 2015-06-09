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
using MonitorPlatform.Data;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// BossLeft.xaml 的交互逻辑
    /// </summary>
    public partial class BossLeft : Page
    {

        private double widthfactor;
        private double heightfactor;
        int orignwidth = 1336;
        int orginheight = 397;
        int calwidth;
        int calheight;

        double orgin_1_start = 5;
        double orgin_2_start = 7;
        double orgin_1_length =1;
        double orgin_2_length = 1;


        int firstlineposY = 1;
        int secondlineposY = 1;
        int thirdlineposY = 1;
        int forthlineposY = 1;
      
        Image image = new Image();
        delegate double cal(double x);

        public BossLeft()
        {
            InitializeComponent();

            this.SizeChanged += new SizeChangedEventHandler(BossLeft_SizeChanged);
        }


        void BossLeft_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ReCalculateAll();
          
        }

        public void ReCalculateAll()
        {
            //5-1300
            //7-1300
            int orgin_firstlineheight = 83;
            int orgin_secondlineheight = 141;
            int orgin_thirdlineheight = 206;
            int orgin_forthlineheight = 262;
   
             orgin_1_length =(1315 -orgin_1_start)/23.0;
             orgin_2_length =(1315-orgin_2_start)/21.0;
             widthfactor = traingrid.ActualWidth / orignwidth;
             heightfactor = traingrid.ActualHeight / orginheight;

             calwidth = (int)(20 * widthfactor);
             calheight = (int)(22 * heightfactor);

             firstlineposY = (int)(orgin_firstlineheight * heightfactor);
             secondlineposY = (int)(orgin_secondlineheight * heightfactor);
             thirdlineposY = (int)(orgin_thirdlineheight * heightfactor);
             forthlineposY = (int)(orgin_forthlineheight * heightfactor);

             cal first = (x) =>    orgin_1_start + orgin_1_length * ( x - 1) ;
             cal second = (x) => orgin_2_start + orgin_2_length * (x - 1);
            if (this.DataContext != null)
            {
                MonitorDataModel data = this.DataContext as MonitorDataModel;
                traingrid.Children.Clear();

                foreach (Train train in data.SubWayLines[0].Trains)
                {
                    Image image = new Image();
                    image.Width = calwidth;
                    image.Height = calheight;
                    image.Stretch = Stretch.Fill;
                    image.Source = new BitmapImage(new Uri("/MonitorPlatform;component/Resource/Car_Normal.png", UriKind.RelativeOrAbsolute));
                    traingrid.Children.Add(image);
                    double orgin_car_posX = orgin_1_start + orgin_1_length * (train.Location - 1);  //first(train.Location);
                    if (train.IsDown)
                    {
                        Canvas.SetTop(image, firstlineposY);
                        Canvas.SetLeft(image, orgin_car_posX * widthfactor);
                    }
                    else
                    {
                        Canvas.SetTop(image, secondlineposY);
                        Canvas.SetLeft(image, orgin_car_posX * widthfactor);
                    }
                }

                foreach (Train train in data.SubWayLines[1].Trains)
                {
                    Image image = new Image();
                    image.Width = calwidth;
                    image.Height = calheight;
                    image.Stretch = Stretch.Fill;
                    image.Source = new BitmapImage(new Uri("/MonitorPlatform;component/Resource/Car_Normal.png", UriKind.RelativeOrAbsolute));
                    traingrid.Children.Add(image);
                    double orgin_car_posX = orgin_2_start + orgin_2_length * (train.Location - 1);
                    if (train.IsDown)
                    {
                        Canvas.SetTop(image, thirdlineposY);
                        Canvas.SetLeft(image, orgin_car_posX * widthfactor);
                    }
                    else
                    {
                        Canvas.SetTop(image, forthlineposY);
                        Canvas.SetLeft(image, orgin_car_posX * widthfactor);
                    }
                }
            }

        }

       



        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

         
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            ReCalculateAll();
        }

        private void Train_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (Window.GetWindow(this) as LeftWindow).ChangeTabByStr("客流信息");
        }

        private void Traffic_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (Window.GetWindow(this) as LeftWindow).ChangeTabByStr("列车位置");
        }

        private void Equipment_MouseUp(object sender, MouseButtonEventArgs e)
        {
            (Window.GetWindow(this) as LeftWindow).ChangeTabByStr("设施设备");
  
        }
    }
}
