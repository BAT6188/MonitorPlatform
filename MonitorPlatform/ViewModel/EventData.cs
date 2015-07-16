using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MonitorPlatform.ViewModel
{
   public class EventData :INotifyPropertyChanged
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

        private string aType;
        public string AType
        {
            get
            {
                return aType;
            }
            set
            {
                aType = value;
                NotifyPropertyChanged("AType");
            }
        }


        private string aLevel;
        public string ALevel
        {
            get
            {
                return aLevel;
            }
            set
            {
                aLevel = value;
                NotifyPropertyChanged("ALevel");
            }
        }


        private string aDesc;
        public string ADesc
        {
            get
            {
                return aDesc;
            }
            set
            {
                aDesc = value;
                NotifyPropertyChanged("ADesc");
            }
        }


      


        private string occurTime;
        public string OccurTime
        {
            get
            {
                return occurTime;
            }
            set
            {
                occurTime = value;
                NotifyPropertyChanged("OccurTime");
            }
        }


        private string occurPlace;
        public string OccurPlace
        {
            get
            {
                return occurPlace;
            }
            set
            {
                occurPlace = value;
                NotifyPropertyChanged("OccurPlace");
            }
        }

        private string occurReason;
        public string OccurReason
        {
            get
            {
                return occurReason;
            }
            set
            {
                occurReason = value;
                NotifyPropertyChanged("OccurReason");
            }
        }
        private string receiveTime;
        public string ReceiveTime
        {
            get
            {
                return receiveTime;
            }
            set
            {
                receiveTime = value;
                NotifyPropertyChanged("ReceiveTime");
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
