using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MonitorPlatform.ViewModel
{
    public class HistoryStation : INotifyPropertyChanged
    {
        private string name = "";
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }

        private DateTime upBeginTime;
        public DateTime UpBeginTime
        {
            get
            {
                return upBeginTime;
            }
            set
            {
                upBeginTime = value;
                NotifyPropertyChanged("UpBeginTime");
            }
        }

        private DateTime downBeginTime;
        public DateTime DownBeginTime
        {
            get
            {
                return downBeginTime;
            }
            set
            {
                downBeginTime = value;
                NotifyPropertyChanged("DownBeginTime");
            }
        }


        private DateTime upEndTime;
        public DateTime UpEndTime
        {
            get
            {
                return upEndTime;
            }
            set
            {
                upEndTime = value;
                NotifyPropertyChanged("UpEndTime");
            }
        }

        private DateTime downEndTime;
        public DateTime DownEndTime
        {
            get
            {
                return downEndTime;
            }
            set
            {
                downEndTime = value;
                NotifyPropertyChanged("DownEndTime");
            }
        }
        //入站数目
        private int inNumber;
        public int InNumber
        {
            get
            {
                return inNumber;
            }
            set
            {
                inNumber = value;
                NotifyPropertyChanged("InNumber");
            }
        }
        //出站数目
        private int outNumber;
        public int OutNumber
        {
            get
            {
                return outNumber;
            }
            set
            {
                outNumber = value;
                NotifyPropertyChanged("OutNumber");
            }
        }

        private int totalNumber;
        public int TotalNumber
        {
            get
            {
                return totalNumber;
            }
            set
            {
                totalNumber = value;
                NotifyPropertyChanged("TotalNumber");
            }
        }

        //拥堵数量
        private int trafficJam;
        public int TrafficJam
        {
            get
            {
                return trafficJam;
            }
            set
            {
                trafficJam = value;
                NotifyPropertyChanged("TrafficJam");
            }
        }

        private string staguid = "";
        public string StaGUID
        {
            get
            {
                return staguid;
            }
            set
            {
                staguid = value;
                NotifyPropertyChanged("StaGUID");
            }
        }

        //损坏数量
        private int brokenNumber;
        public int BrokenNumber
        {
            get
            {
                return brokenNumber;
            }
            set
            {
                brokenNumber = value;
                NotifyPropertyChanged("BrokenNumber");
            }
        }

        //分时进站人数 7点，8点，9点....
        private ObservableCollection<PersonsRateSum> personrates = new ObservableCollection<PersonsRateSum>();
        public ObservableCollection<PersonsRateSum> Personrates
        {
            get
            {
                return personrates;
            }
            set
            {
                personrates = value;
                NotifyPropertyChanged("Personrates");
            }
        }
        private ObservableCollection<StationInOut> stationInOut = new ObservableCollection<StationInOut>();
        public ObservableCollection<StationInOut> StationInOut
        {
            get
            {
                return stationInOut;
            }
            set
            {
                stationInOut = value;
                NotifyPropertyChanged("StationInOut");
            }
        }

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
