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
