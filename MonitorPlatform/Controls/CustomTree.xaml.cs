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
using System.ComponentModel;

namespace MonitorPlatform.Controls
{
    /// <summary>
    /// CustomTree.xaml 的交互逻辑
    /// </summary>
    public partial class CustomTree : UserControl
    {
        public CustomTree()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TreeItemSourceProperty =
           DependencyProperty.Register("TreeItemSource", typeof(List<PropertyNodeItem>),
           typeof(CustomTree));
        public List<PropertyNodeItem> TreeItemSource
        {
            get { return (List<PropertyNodeItem>)GetValue(TreeItemSourceProperty); }

            set { SetValue(TreeItemSourceProperty, value); }
        }
       
    }
    public class PropertyNodeItem
    {

        public string Icon { get; set; }
        public string EditIcon { get; set; }

        public string DisplayName { get; set; }

        public string Name { get; set; }



        public List<PropertyNodeItem> Children { get; set; }
        public PropertyNodeItem()
        {

            Children = new List<PropertyNodeItem>();

        }

    }
}
