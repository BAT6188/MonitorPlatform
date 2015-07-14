using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MonitorPlatform.ViewModel
{
    public class Train
    {
        private double location;
        public double Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                NotifyPropertyChanged("Location");
            }
        }

        private string sectionClass;
        public string SectionClass
        {
            get
            {
                return sectionClass;
            }
            set
            {
                sectionClass = value;
                NotifyPropertyChanged("SectionClass");
            }
        }


        private bool isDown = true;
        public bool IsDown
        {
            get
            {
                return isDown;
            }
            set
            {
                isDown = value;
                NotifyPropertyChanged("IsDown");
            }
        }

        //0代表1号线，1代表2号线
        private int lineNo = 0;
        public int LineNo
        {
            get
            {
                return lineNo;
            }
            set
            {
                lineNo = value;
                NotifyPropertyChanged("LineNo");
            }
        }


        private string trainNumber = "SZDT－Z00104";
        public string TrainNumber
        {
            get
            {
                return trainNumber;
            }
            set
            {
                trainNumber = value;
                NotifyPropertyChanged("TrainNumber");
            }
        }

        private string trainType = "TR－RCZW";
        public string TrainType
        {
            get
            {
                return trainType;
            }
            set
            {
                trainType = value;
                NotifyPropertyChanged("TrainType");
            }
        }

        //车厢数（编组
        private int trainCount = 6;
        public int TrainCount
        {
            get
            {
                return trainCount;
            }
            set
            {
                trainCount = value;
                NotifyPropertyChanged("TrainCount");
            }
        }

        //荷载人数
        private int trainQuanutity = 1500;
        public int TrainQuanutity
        {
            get
            {
                return trainQuanutity;
            }
            set
            {
                trainQuanutity = value;
                NotifyPropertyChanged("TrainQuanutity");
            }
        }

        //购买年份
        private string purchaseYear = "2014年";
        public string PurchaseYear
        {
            get
            {
                return purchaseYear;
            }
            set
            {
                purchaseYear = value;
                NotifyPropertyChanged("PurchaseYear");
            }
        }

        //投入使用年度
        private string adoptYear = "2014年";
        public string AdoptYear
        {
            get
            {
                return adoptYear;
            }
            set
            {
                adoptYear = value;
                NotifyPropertyChanged("AdoptYear");
            }
        }

        //车身尺寸
        private string trainSize = "6*3*3";
        public string TrainSize
        {
            get
            {
                return trainSize;
            }
            set
            {
                trainSize = value;
                NotifyPropertyChanged("TrainSize");
            }
        }

        //列车配置
        private string trainConfig = "配置信息";
        public string TrainConfig
        {
            get
            {
                return trainConfig;
            }
            set
            {
                trainConfig = value;
                NotifyPropertyChanged("TrainConfig");
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
