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
using MonitorPlatform.Data;
using System.Windows.Media.Animation;

namespace MonitorPlatform
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Login_Loaded);
        }

        void Login_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = System.Windows.WindowStyle.None;
        }

        private void btnLogin_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Storyboard shake = (Storyboard)this.Resources["shake"];
            this.txtAlert.Text = "";
            if (string.IsNullOrEmpty(txtUser.Text))
            {
                shake.Begin();
                this.txtAlert.Text = "请输入帐号!";
                return;
            }
            if (string.IsNullOrEmpty(this.txtPassword.Password))
            {
                shake.Begin();
                this.txtAlert.Text = "请输入密码!.";
                return; 
            }
            if (DataCenter.Instance.Login(this.txtUser.Text.Trim(), this.txtPassword.Password.Trim()))
            {
                this.Hide();
                LeftWindow left = new LeftWindow();
                left.Show();
                
            }
            else
            {
                shake.Begin();
                this.txtAlert.Text = "账户或者密码错误!.";
                return; 
            }
        }
    }
}
