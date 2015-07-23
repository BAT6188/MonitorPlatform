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
using System.Collections.ObjectModel;

namespace MonitorPlatform.Pages
{
    /// <summary>
    /// EventCenter.xaml 的交互逻辑
    /// </summary>
    public partial class EventCenter : Page
    {
        public EventCenter()
        {
            InitializeComponent();
            SetDataSource();
        }

        public void SetDataSource()
        {
            if (this.lines == null || this.datePicker1 ==null || this.status ==null || this.type == null || this.grade == null)
            {
                return;
            }
            ObservableCollection<EventData> source= new ObservableCollection<EventData>();
            ObservableCollection<StationInOut> troubles = new ObservableCollection<StationInOut>();
            troubles.Add(new StationInOut() { Name = "1号线", Number = MonitorDataModel.Instance().SubWayLines[0].EventDatas.Count });
            troubles.Add(new StationInOut() { Name = "2号线", Number = MonitorDataModel.Instance().SubWayLines[1].EventDatas.Count });

            IEnumerable<EventData> data1 = null;
            IEnumerable<EventData> data2 = null;
            string selectedText = "";
            ComboBoxItem cbi = (ComboBoxItem)lines.SelectedItem;
            if(cbi!=null)
             selectedText = cbi.Content.ToString();
            //Status：0-已处理,1-未处里,2-忽略
            if (selectedText == "1号线" || selectedText == "全部" || selectedText =="")
            {
                data1 = FilterByTime(FilterByStatus(FilterByType(FilterByGrade(MonitorDataModel.Instance().SubWayLines[0].EventDatas))));
            }
            if (selectedText == "2号线" || selectedText == "全部" || selectedText == "")
            {
                data2 = FilterByTime(FilterByStatus(FilterByType(FilterByGrade(MonitorDataModel.Instance().SubWayLines[1].EventDatas))));
            }
            source.Clear();
            if (data1 != null)
            {
                foreach (EventData eve in data1)
                {
                    source.Add(eve);
                }
            }
            if (data2 != null)
            {
                foreach (EventData eve in data2)
                {
                    source.Add(eve);
                }
            }
            this.gridEvent.ItemsSource = source;
            this.lineEventChart.ItemsSource = troubles;
        }

        public IEnumerable<EventData> FilterByStatus(IEnumerable<EventData>  data)
        {
            string selectedText = "";
            ComboBoxItem cbi = (ComboBoxItem)status.SelectedItem;
            if (cbi != null)
                selectedText = cbi.Content.ToString();
            if (selectedText == "未处理")
            {
                return data.Where(x => x.Status == "未处理");
            }
            else if (selectedText == "已处理")
            {
                return data.Where(x => x.Status == "已处理");
            }
            return data;
        }

        public IEnumerable<EventData> FilterByTime(IEnumerable<EventData> data)
        {
            DateTime? time = datePicker1.SelectedDate;
            if (time.HasValue)
            {
               return data.Where(s =>
                {
                    if (!string.IsNullOrEmpty(s.OccurTime))
                    {
                        DateTime temp;
                        DateTime.TryParse(s.OccurTime, out temp);
                        if (time.Value.Date == temp.Date)
                        {
                            return true;
                        }
                    }
                    return false;
                });
            }
            
            return data;
        }

        public IEnumerable<EventData> FilterByType(IEnumerable<EventData> data)
        {
            string selectedText = "";
            ComboBoxItem cbi = (ComboBoxItem)type.SelectedItem;
            if (cbi != null)
                selectedText = cbi.Content.ToString();
            if (selectedText == "轨道公交接驳事件")
            {
                return data.Where(x => x.AType == "轨道公交接驳事件");
            }
            else if (selectedText == "其他")
            {
                return data.Where(x => x.AType != "轨道公交接驳事件");
            }
            return data;
        }

        public IEnumerable<EventData> FilterByGrade(IEnumerable<EventData> data)
        {
            string selectedText = "";
            ComboBoxItem cbi = (ComboBoxItem)grade.SelectedItem;
            if (cbi != null)
                selectedText = cbi.Content.ToString();
            if (selectedText == "一级事件")
            {
                return data.Where(x => x.ALevel == "一级事件");
            }
            else if (selectedText == "二级事件")
            {
                return data.Where(x => x.ALevel == "二级事件");
            }
            else if (selectedText == "三级事件")
            {
                return data.Where(x => x.ALevel == "三级事件");
            }
            else if (selectedText == "四级事件")
            {
                return data.Where(x => x.ALevel == "四级事件");
            }
            return data;
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            SetDataSource();
        }

      
    }


}
