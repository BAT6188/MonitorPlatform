using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using ZNJT.WebProxy;
using MonitorPlatform.Data;

namespace MonitorPlatform
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        void  CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception  exception = e.ExceptionObject as Exception;
            MessageBox.Show("程序出现异常,请联系开发者!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            LogCenter.Log(exception);
        }

        void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            LogCenter.Log(e.Exception);
        }
      
    }
}
