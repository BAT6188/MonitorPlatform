using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MonitorPlatform.ViewModel
{
    public class SubLine : INotifyPropertyChanged
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
        private float innumber;
        public float InNumber
        {
            get
            {
                return innumber;
            }
            set
            {
                innumber = value;
                NotifyPropertyChanged("InNumber");
            }
        }

        private float outnumber;
        public float OutNumber
        {
            get
            {
                return outnumber;
            }
            set
            {
                outnumber = value;
                NotifyPropertyChanged("OutNumber");
            }
        }

        private float totalnumber;
        public float TotalNumber
        {
            get
            {
                return totalnumber;
            }
            set
            {
                totalnumber = value;
                NotifyPropertyChanged("TotalNumber");
            }
        }

        // 进站人数/总人数
        private ObservableCollection<InOutTotal> totalrate = new ObservableCollection<InOutTotal>();
        public ObservableCollection<InOutTotal> TotalRate
        {
            get
            {
                return totalrate;
            }
            set
            {
                totalrate = value;
                NotifyPropertyChanged("TotalRate");
            }
        }


        private bool trainIsWarn;
        public bool TrainIsWarn
        {
            get
            {
                return trainIsWarn;
            }
            set
            {
                trainIsWarn = value;
                NotifyPropertyChanged("TotalRate");
            }
        }

        private bool passIsWarn;
        public bool PassIsWarn
        {
            get
            {
                return passIsWarn;
            }
            set
            {
                passIsWarn = value;
                NotifyPropertyChanged("PassIsWarn");
            }
        }

        private bool equipIsWarn;
        public bool EquipIsWarn
        {
            get
            {
                return equipIsWarn;
            }
            set
            {
                equipIsWarn = value;
                NotifyPropertyChanged("EquipIsWarn");
            }
        }


        private int cameraTotalNumber;
        public int CameraTotalNumber
        {
            get
            {
                return cameraTotalNumber;
            }
            set
            {
                cameraTotalNumber = value;
                NotifyPropertyChanged("CameraTotalNumber");
            }
        }


        private int personsPercent;
        public int PersonsPercent
        {
            get
            {
                return personsPercent;
            }
            set
            {
                personsPercent = value;
                NotifyPropertyChanged("PersonsPercent");
            }
        }

        private ObservableCollection<Station> stations = new ObservableCollection<Station>();
        public ObservableCollection<Station> Stations
        {
            get
            {
                return stations;
            }
            set
            {
                stations = value;
                NotifyPropertyChanged("Stations");
            }
        }

        private ObservableCollection<HistoryStation> hisstations = new ObservableCollection<HistoryStation>();
        public ObservableCollection<HistoryStation> History_Stations
        {
            get
            {
                return hisstations;
            }
            set
            {
                hisstations = value;
                NotifyPropertyChanged("History_Stations");
            }
        }


        //上行车 开始时间
        private string uptrainStartTime;
        public string UptrainStartTime
        {
            get
            {
                return uptrainStartTime;
            }
            set
            {
                uptrainStartTime = value;
                NotifyPropertyChanged("UptrainStartTime");
            }
        }

        //上行车 结束时间
        private string uptrainEndTime;
        public string UptrainEndTime
        {
            get
            {
                return uptrainEndTime;
            }
            set
            {
                uptrainEndTime = value;
                NotifyPropertyChanged("UptrainEndTime");
            }
        }


        //上行车 数量
        private int uptrainCount;
        public int UptrainCount
        {
            get
            {
                return uptrainCount;
            }
            set
            {
                uptrainCount = value;
                NotifyPropertyChanged("UptrainCount");
            }
        }



        //下行车 开始时间
        private string downtrainStartTime;
        public string DowntrainStartTime
        {
            get
            {
                return downtrainStartTime;
            }
            set
            {
                downtrainStartTime = value;
                NotifyPropertyChanged("DowntrainStartTime");
            }
        }

        //下行车 结束时间
        private string downtrainEndTime;
        public string DowntrainEndTime
        {
            get
            {
                return downtrainEndTime;
            }
            set
            {
                downtrainEndTime = value;
                NotifyPropertyChanged("DowntrainEndTime");
            }
        }


        //下行车 数量
        private int downtrainCount;
        public int DowntrainCount
        {
            get
            {
                return downtrainCount;
            }
            set
            {
                downtrainCount = value;
                NotifyPropertyChanged("DowntrainCount");
            }
        }



        //历史上某一天 进站人数


        private ObservableCollection<InOutTotal> totalrate_history = new ObservableCollection<InOutTotal>();
        public ObservableCollection<InOutTotal> TotalRate_history
        {
            get
            {
                return totalrate_history;
            }
            set
            {
                totalrate_history = value;
                NotifyPropertyChanged("TotalRate_history");
            }
        }

        //历史上某天 进出站总人数
        private int history_totalnumber;
        public int History_totalnumber
        {
            get
            {
                return history_totalnumber;
            }
            set
            {
                history_totalnumber = value;
                NotifyPropertyChanged("History_totalnumber");
            }
        }

        //分时进站人数 7点，8点，9点....
        private ObservableCollection<PersonsRateSum> linePersonrates = new ObservableCollection<PersonsRateSum>();
        public ObservableCollection<PersonsRateSum> LinePersonrates
        {
            get
            {
                return linePersonrates;
            }
            set
            {
                linePersonrates = value;
                NotifyPropertyChanged("LinePersonrates");
            }
        }


        //拥挤站点数
        private int crowdStation;
        public int CrowdStation
        {
            get
            {
                return crowdStation;
            }
            set
            {
                crowdStation = value;
                NotifyPropertyChanged("CrowdStation");
            }
        }


        //拥堵站点数
        private int blockStation;
        public int BlockStation
        {
            get
            {
                return blockStation;
            }
            set
            {
                blockStation = value;
                NotifyPropertyChanged("BlockStation");
            }
        }

        private ObservableCollection<TroubleStatusSum> troubles = new ObservableCollection<TroubleStatusSum>();
        public ObservableCollection<TroubleStatusSum> Troubles
        {
            get
            {
                return troubles;
            }
            set
            {
                troubles = value;
                NotifyPropertyChanged("Troubles");
            }
        }

        //月度，季度，年度，全部，累计 统计
        private ObservableCollection<PersonsRateAnalyze> rateReportData = new ObservableCollection<PersonsRateAnalyze>();
        public ObservableCollection<PersonsRateAnalyze> RateReportData
        {
            get
            {
                return rateReportData;
            }
            set
            {
                rateReportData = value;
                NotifyPropertyChanged("RateReportData");
            }
        }


        private ObservableCollection<StationTroubleStatus> statroubles = new ObservableCollection<StationTroubleStatus>();
        public ObservableCollection<StationTroubleStatus> StaTroubles
        {
            get
            {
                return statroubles;
            }
            set
            {
                statroubles = value;
                NotifyPropertyChanged("StaTroubles");
            }
        }


        private ObservableCollection<VoltageStatus> voltagestat = new ObservableCollection<VoltageStatus>();
        public ObservableCollection<VoltageStatus> VoltageStat
        {
            get
            {
                return voltagestat;
            }
            set
            {
                voltagestat = value;
                NotifyPropertyChanged("VoltageStatus");
            }
        }


        private ObservableCollection<Train> trains = new ObservableCollection<Train>();
        public ObservableCollection<Train> Trains
        {
            get
            {
                return trains;
            }
            set
            {
                trains = value;
                NotifyPropertyChanged("Trains");
            }
        }


        public void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


    public class TroubleStatusSum : INotifyPropertyChanged
    {

        private String equipmentType;
        public String EquipmentType
        {
            get
            {
                return equipmentType;
            }
            set
            {
                equipmentType = value;
                NotifyPropertyChanged("EquipmentType");
            }
        }


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

        private int badNumber;
        public int BadNumber
        {
            get
            {
                return badNumber;
            }
            set
            {
                badNumber = value;
                NotifyPropertyChanged("badNumber");
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

    public class PersonsRateSum : INotifyPropertyChanged
    {
        private String time;
        public String Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                NotifyPropertyChanged("Time");
            }
        }


        private int innumber;
        public int InNumber
        {
            get
            {
                return innumber;
            }
            set
            {
                innumber = value;
                NotifyPropertyChanged("InNumber");
            }
        }


        private int outnumber;
        public int Outnumber
        {
            get
            {
                return outnumber;
            }
            set
            {
                outnumber = value;
                NotifyPropertyChanged("Outnumber");
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

    public class InOutTotal : INotifyPropertyChanged
    {

        private String name;
        public String Name
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


        private int totalRate;
        public int TotalRate
        {
            get
            {
                return totalRate;
            }
            set
            {
                totalRate = value;
                NotifyPropertyChanged("TotalRate");
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

    public class StationTroubleStatus : INotifyPropertyChanged
    {
        private int id;
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


        private int warncount;
        public int WarnCount
        {
            get
            {
                return warncount;
            }
            set
            {
                warncount = value;
                NotifyPropertyChanged("WarnCount");
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

    public class VoltageStatus : INotifyPropertyChanged
    {
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


        private int val_110;
        public int Val_110
        {
            get
            {
                return val_110;
            }
            set
            {
                val_110 = value;
                NotifyPropertyChanged("Val_110");
            }
        }

        private int val_35;
        public int Val_35
        {
            get
            {
                return val_35;
            }
            set
            {
                val_35 = value;
                NotifyPropertyChanged("Val_35");
            }
        }

        private int cabtemp;
        public int CabTemp
        {
            get
            {
                return cabtemp;
            }
            set
            {
                cabtemp = value;
                NotifyPropertyChanged("CabTemp");
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



    public class PersonsRateAnalyze : INotifyPropertyChanged
    {
        private String time;
        public String Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
                NotifyPropertyChanged("Time");
            }
        }


 


        private int totalnumber;
        public int TotalNumber
        {
            get
            {
                return totalnumber;
            }
            set
            {
                totalnumber = value;
                NotifyPropertyChanged("TotalNumber");
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
