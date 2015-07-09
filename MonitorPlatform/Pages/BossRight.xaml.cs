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
    /// BossRight.xaml 的交互逻辑
    /// </summary>
    public partial class BossRight : Page
    {
        public BossRight()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(BossRight_Loaded);
        }

        void BossRight_Loaded(object sender, RoutedEventArgs e)
        {
            videoControl.SetLayout(4);
        }
        public void ShowTrafficImage()
        {
            inforpic.IsOpen = true;
            Window parentwin = Window.GetWindow(this);
            inforpic.PlacementTarget = parentwin;
            DependencyObject parent = inforpic.Child;

            do
            {
                parent = VisualTreeHelper.GetParent(parent);

                if (parent != null && parent.ToString() == "System.Windows.Controls.Primitives.PopupRoot")
                {
                    var element = parent as FrameworkElement;

                    element.Height = parentwin.Height;
                    element.Width = parentwin.Width;

                    break;
                }
            }
            while (parent != null);


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
