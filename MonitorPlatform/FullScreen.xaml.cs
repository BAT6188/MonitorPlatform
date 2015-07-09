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
using System.Windows.Forms;

namespace MonitorPlatform
{
    /// <summary>
    /// FullScreen.xaml 的交互逻辑
    /// </summary>
    public partial class FullScreen : Window
    {
        public FullScreen()
        {
            InitializeComponent();
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.Loaded += new RoutedEventHandler(FullScreen_Loaded);
        }

        void FullScreen_Loaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Forms.Screen[] screens = System.Windows.Forms.Screen.AllScreens;
            //this.Top = screens[0].WorkingArea.Top;
            //this.Left = screens[0].WorkingArea.Left;
            //int totalwidth =0;
            //int totalheight =0;
            //foreach(Screen s  in screens)
            //{

            //}
            //if (screens.Length == 3)
            //{
                
            //    this.Width= screens[]

            // ;
            //}
        }
    }
}
