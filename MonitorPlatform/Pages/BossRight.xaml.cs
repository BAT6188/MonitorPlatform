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
using System.Windows.Controls.Primitives;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// BossRight.xaml 的交互逻辑
    /// </summary>
    public partial class BossRight : Page
    {
        public BossRight()
        {
            InitializeComponent();
         
            this.Loaded += new RoutedEventHandler(BossRight_Loaded);
            this.SizeChanged += new SizeChangedEventHandler(BossRight_SizeChanged);
            
        }

        void BossRight_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            videoControl.SetOcxSize((int)xhost.ActualWidth-5, (int)xhost.ActualHeight-5);
        }

        void parentwin_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           // SetPopUpSize();
        }
        void parentwin_LocationChanged(object sender, EventArgs e)
        {
        
        }
        void BossRight_Loaded(object sender, RoutedEventArgs e)
        {

            videoControl.SetLayout(4);
            Window parentwin = Window.GetWindow(this);
            parentwin.LocationChanged += new EventHandler(parentwin_LocationChanged);
            parentwin.SizeChanged += new SizeChangedEventHandler(parentwin_SizeChanged);
            fourraido.IsChecked = true;
        }



        private void btnScreenClick(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            if (radio != null)
            {
                videoControl.SetLayout(int.Parse(radio.Tag.ToString()));
            }
        }
        public void ShowTrafficImage()
        {
            this.xhost.Visibility = Visibility.Hidden;
        }

        public void CloseTrafficImage()
        {
            this.xhost.Visibility = Visibility.Visible;
        }

     

     
    }
}
