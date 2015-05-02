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
