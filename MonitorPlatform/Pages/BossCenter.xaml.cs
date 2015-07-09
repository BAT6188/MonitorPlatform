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

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// BossCenter.xaml 的交互逻辑
    /// </summary>
    public partial class BossCenter : Page
    {
        public BossCenter()
        {
            InitializeComponent();
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
    }
}
