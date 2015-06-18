using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace MonitorPlatform.ViewModel
{
    public class Camera
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


        private string location = "";
        public string Location
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

        private string code = "";
        public string Code
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
                NotifyPropertyChanged("Code");
            }
        }

        //类型
        private string type = "";
        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                NotifyPropertyChanged("Type");
            }
        }

        private string appid = "";
        public string APIID
        {
            get
            {
                return appid;
            }
            set
            {
                appid = value;
                NotifyPropertyChanged("APIID");
            }
        }

        private string appip = "";
        public string APIIP
        {
            get
            {
                return appip;
            }
            set
            {
                appip = value;
                NotifyPropertyChanged("APIIP");
            }
        }

        private string remark = "";
        public string Remark
        {
            get
            {
                return remark;
            }
            set
            {
                remark = value;
                NotifyPropertyChanged("Remark");
            }
        }

        //所属行业
        private string industry = "";
        public string Industry
        {
            get
            {
                return industry;
            }
            set
            {
                industry = value;
                NotifyPropertyChanged("Industry");
            }
        }

        //在线状态
        private string status = "";
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

        //订阅状态
        private string subscription = "";
        public string Subscription
        {
            get
            {
                return subscription;
            }
            set
            {
                subscription = value;
                NotifyPropertyChanged("Subscription");
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
