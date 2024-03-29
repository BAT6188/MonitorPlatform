﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace MonitorPlatform.Convert
{

    public class ValToNotifyStyleTextConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool ischeck;
            if (value != null && bool.TryParse(value.ToString(), out ischeck))
            {
                if (ischeck)
                {
                    return App.Current.Resources["NotifyContainerAlertText"];
                }
                else
                {
                    return App.Current.Resources["NotifyContainerText"];
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
