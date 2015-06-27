using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MonitorPlatform.ViewModel
{
    public class Station : INotifyPropertyChanged
    {

        private int id = 0;
        public int ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                NotifyPropertyChanged("ID");
            }
        }
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


        private int cameraNumber;
        public int CameraNumber
        {
            get
            {
                return cameraNumber;
            }
            set
            {
                cameraNumber = value;
                NotifyPropertyChanged("CameraNumber");
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

        //1 is normal, 2 is yellow 3 is busy.
        private int status = 1;
        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }

        //上行 第一班还有多久到达
        private int upFirstTime = 3;
        public int UpFirstTime
        {
            get
            {
                return upFirstTime;
            }
            set
            {
                upFirstTime = value;
                NotifyPropertyChanged("UpFirstTime");
            }
        }

        //上行 第二班到达时间
        private int upSecondTime = 3;
        public int UpSecondTime
        {
            get
            {
                return upSecondTime;
            }
            set
            {
                upSecondTime = value;
                NotifyPropertyChanged("UpSecondTime");
            }
        }



        //下行 第一班还有多久到达
        private int downFirstTime = 3;
        public int DownFirstTime
        {
            get
            {
                return downFirstTime;
            }
            set
            {
                downFirstTime = value;
                NotifyPropertyChanged("DownFirstTime");
            }
        }

        //下行 第二班到达时间
        private int downSecondTime = 3;
        public int DownSecondTime
        {
            get
            {
                return downSecondTime;
            }
            set
            {
                downSecondTime = value;
                NotifyPropertyChanged("DownSecondTime");
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

        private ObservableCollection<Equipment> equipments = new ObservableCollection<Equipment>();
        public ObservableCollection<Equipment> Equipments
        {
            get
            {
                return equipments;
            }
            set
            {
                equipments = value;
                NotifyPropertyChanged("Equipments");
            }
        }


        private ObservableCollection<Camera> cameras = new ObservableCollection<Camera>();
        public ObservableCollection<Camera> Cameras
        {
            get
            {
                return cameras;
            }
            set
            {
                cameras = value;
                NotifyPropertyChanged("Cameras");
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

    //进出站人数。用于饼图
    public class StationInOut : INotifyPropertyChanged
    {
        private int number;
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                NotifyPropertyChanged("Number");
            }
        }


        private string name;
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
