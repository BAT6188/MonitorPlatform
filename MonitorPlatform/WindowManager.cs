using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Reflection;

namespace MonitorPlatform
{
    public class WindowManager
    {
        private static WindowManager instance = new WindowManager();
        public LeftWindow left;
        public CenterWindow center;
        public RightWindows right;
        public static WindowManager Instance
        {
            get{
                return instance;
            }
        }

        public void ShowTrainLocationCenter()
        {
            center.frame.Source = new Uri("Pages/TrainLocationCenter.xaml", UriKind.Relative);
        }

        public void ShowEquipmentRight()
        {
            right.frame.Source = new Uri("Pages/EquipmentStatusRight.xaml", UriKind.Relative);
        }

        public void CloseEquipmentRight()
        {
            right.frame.Source = new Uri("Pages/BossRight.xaml", UriKind.Relative);
        }

        public void ShowEvent()
        {
            center.frame.Source = new Uri("Pages/EventCenter.xaml", UriKind.Relative);
        }

         public void ShowEnviorment()
        {
            center.frame.Source = new Uri("Pages/EnviormentCenter.xaml", UriKind.Relative);
        }


        public void ShowTrafficImage()
        {
            left.ShowTrafficImage();
            center.ShowTrafficImage();
            right.ShowTrafficImage();
            InvokeShowTraffic(right);
        }

        private  void InvokeShowTraffic(Window w)
        {
            object obj = w.FindName("frame");
            Frame f = obj as Frame;
            Type pagetype = f.Content.GetType();
            MethodInfo showtraffic = pagetype.GetMethod("ShowTrafficImage");
            if (showtraffic != null)
            {
                showtraffic.Invoke(f.Content, null);
            }
        }

        private void InvokeCloseTrafficImage(Window w)
        {
            object obj = w.FindName("frame");
            Frame f = obj as Frame;
            Type pagetype = f.Content.GetType();
            MethodInfo showtraffic = pagetype.GetMethod("CloseTrafficImage");
            if (showtraffic != null)
            {
                showtraffic.Invoke(f.Content, null);
            }
        }


        public void CloseTrafficImage()
        {
            left.CloseTrafficImage();
            center.CloseTrafficImage();
            right.CloseTrafficImage();
            InvokeCloseTrafficImage(right);
        }

    }
}

