using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MonitorPlatform.ViewModel
{
    public class Equipment : INotifyPropertyChanged
    {
        private string owner;
        public string Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
                NotifyPropertyChanged("Owner");
            }
        }


        private string id;
        public string ID
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
        private string equipmentType;
        public string EquipmentType
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

        private string location;
        public string Location
        {
            get
            {
                return location;
            }
            set {
                location = value;
                NotifyPropertyChanged("Location");
            }
        }

        private string use;
        public string Use
        {
            get
            {
                return use;
            }
            set
            {
                use = value;
                NotifyPropertyChanged("Use");
            }
        }


        private string status;
        public string Status
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

        private string warningLevel;
        public string WaringLevel
        {
            get
            {
                return warningLevel;
            }
            set
            {
                warningLevel = value;
                NotifyPropertyChanged("WaringLevel");
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
