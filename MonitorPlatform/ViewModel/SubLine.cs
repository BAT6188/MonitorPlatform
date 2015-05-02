﻿using System;
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
}
