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
