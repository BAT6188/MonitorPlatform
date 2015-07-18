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
        }

        void parentwin_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetPopUpSize();
        }
        void parentwin_LocationChanged(object sender, EventArgs e)
        {
            if (inforpic.IsOpen)
            {
                var mi = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                mi.Invoke(inforpic, null);
            }
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


        public void SetPopUpSize()
        {
            Window parentwin = Window.GetWindow(this);
            inforpic.PlacementTarget = parentwin;
            DependencyObject parent = inforpic.Child;

            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;


                    element.Height = parentwin.ActualHeight;// parentwin.Height;
                    element.Width = parentwin.ActualWidth;// parentwin.Width;

                    break;
                }
            }
            while (parent != null);


        }

        public void ShowTrafficImage()
        {
            inforpic.IsOpen = true;
            SetPopUpSize();
        }

        public void CloseTrafficImage()
        {
            inforpic.IsOpen = false;
        }

        private void btnClose_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WindowManager.Instance.CloseTrafficImage();
        }
    }
}
