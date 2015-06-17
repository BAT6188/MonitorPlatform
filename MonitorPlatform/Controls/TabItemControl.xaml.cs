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

namespace MonitorPlatform.Controls
{
    /// <summary>
    /// TabItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class TabItemControl : UserControl
    {
        public TabItemControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ImageIndexProperty =
          DependencyProperty.Register("ImageIndex", typeof(int),
          typeof(TabItemControl),new UIPropertyMetadata(default(int), new PropertyChangedCallback(OnImgIndexChanged)));
        public int ImageIndex
        {
            get { return (int)GetValue(ImageIndexProperty); }

            set { SetValue(ImageIndexProperty, value); }
        }

        private static void OnImgIndexChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
            var item = target as TabItemControl;
            for (int i=1;i<=6;i++)
            {
                (item.FindName("back" + item.ImageIndex) as System.Windows.Shapes.Path).Visibility = Visibility.Hidden;
            }
            (item.FindName("back" + item.ImageIndex) as  System.Windows.Shapes.Path).Visibility  = Visibility.Visible;
        }

        public static readonly DependencyProperty IsCheckedProperty =
         DependencyProperty.Register("IsChecked", typeof(bool),
         typeof(TabItemControl), new UIPropertyMetadata(default(bool), new PropertyChangedCallback(OnCheckedChanged)));
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }

            set { SetValue(IsCheckedProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
         DependencyProperty.Register("Text", typeof(string),
         typeof(TabItemControl), new UIPropertyMetadata(default(string), new PropertyChangedCallback(OnTextChanged)));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }

            set { SetValue(TextProperty, value); }
        }

        private static void OnTextChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
            var item = target as TabItemControl;

            item.itemtext.Text = item.Text;
            //Ugly implementation
            switch (item.Text)
            {
                case "总裁界面":
                    item.ImageIndex = 1;
                    break;
                case "客流信息":
                    item.ImageIndex = 2;
                    break;
                case "列车位置":
                    item.ImageIndex = 3;
                    break;
                case "设施设备":
                    item.ImageIndex = 4;
                    break;
                case "视频监控":
                    item.ImageIndex = 5;
                    break;
                case "地理信息":
                    item.ImageIndex = 6;
                    break;
                default:
                    item.ImageIndex = 1;
                    break;
            }
        }

        private static void OnCheckedChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if (target == null)
                return;
            var item = target as TabItemControl;
            SolidColorBrush checkedcolor = new SolidColorBrush();
            checkedcolor.Color = Color.FromRgb(255, 255, 255);
            SolidColorBrush uncheckedcolor = new SolidColorBrush();
            uncheckedcolor.Color = Color.FromRgb(0, 101, 203);

            
            System.Windows.Shapes.Path icon = (item.FindName("back" + item.ImageIndex) as System.Windows.Shapes.Path);
            if (item.IsChecked)
            {

                icon.Fill = checkedcolor;
                item.backselect.Visibility = Visibility.Visible;
                item.backrect.Visibility = Visibility.Visible;
                item.itemtext.Foreground = checkedcolor;

            }

            else
            {

                icon.Fill = uncheckedcolor;
                item.backselect.Visibility = Visibility.Hidden;
                item.backrect.Visibility = Visibility.Hidden;
                item.itemtext.Foreground = uncheckedcolor;

            
            }

            
        }
    }
}
