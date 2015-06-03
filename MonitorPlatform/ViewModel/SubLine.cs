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

        private int persons;
        public int Persons
        {
            get
            {
                return persons;
            }
            set
            {
                persons = value;
                NotifyPropertyChanged("Persons");
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

        //上行车 数量
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
                NotifyPropertyChanged("downtrainCount");
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


        private void NotifyPropertyChanged(string property)
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
}
