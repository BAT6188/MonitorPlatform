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

namespace MonitorPlatform.Controls
{
    /// <summary>
    /// TabSelectControl.xaml 的交互逻辑
    /// </summary>
    public partial class TabSelectControl : UserControl
    {
        public TabSelectControl()
        {
            InitializeComponent();
            
        }

        private void TabSelectControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            this.DataContext = new MenuItems();
            
        }

        void mylist_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            this.mylist.SelectedIndex = 0;
        }

        void mylist_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        public static readonly RoutedEvent TabSelectChangedEvent =
           EventManager.RegisterRoutedEvent("TabSelectChanged", RoutingStrategy.Tunnel,
           typeof(EventHandler<TabSelectChangeEventArgs>), typeof(TabSelectControl));

        /// <summary>
        /// 事件添加和删除
        /// </summary>
        public event RoutedEventHandler TabSelectChanged
        {
            add { this.AddHandler(TabSelectChangedEvent, value); }
            remove { this.RemoveHandler(TabSelectChangedEvent, value); }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabSelectChangeEventArgs args = new TabSelectChangeEventArgs(TabSelectChangedEvent,this);
            args.ChoosedTab = (this.DataContext as MenuItems).SelectedChartType;
            this.RaiseEvent(args);
        }

        public void SelectItem(string p)
        {
            mylist.SelectedValue = p;
        }
    }

    /// <summary>
    /// 自定义路由事件
    /// </summary>
    public class TabSelectChangeEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// 调用父类的构造
        /// </summary>
        /// <param name="routedEvent"></param>
        /// <param name="sender"></param>
        public TabSelectChangeEventArgs(RoutedEvent routedEvent, Object sender) : base(routedEvent, sender) { }

        /// <summary>
        /// 定义自己的属性
        /// </summary>
        public String ChoosedTab { get; set; }
    }
}
