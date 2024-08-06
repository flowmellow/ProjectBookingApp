using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ProjectBookingApp.Models;
using ProjectBookingApp.DataManage;
using ProjectBookingApp.Utility;
using ProjectBookingApp.Views;

namespace ProjectBookingApp.Converters
{
    public class ListContainsToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<int> list && parameter is int id)
            {
                return list.Contains(id) ? Colors.Gray : Colors.Blue;
            }
            return Colors.AliceBlue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
