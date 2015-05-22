using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace MonitorPlatform.Convert
{
  
    public class ValToNotifyStyleConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ischeck;
            if (value != null && bool.TryParse(value.ToString(), out ischeck))
            {
                if (ischeck)
                {
                    return App.Current.Resources["NotifyContainerAlert"];
                }
                else
                {
                    return App.Current.Resources["NotifyContainer"];
                }
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }



    }
}
