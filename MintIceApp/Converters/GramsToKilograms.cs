using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MintIceApp.Converters
{
    public class GramsToKilogramsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value * 1000;
        }
    }
}
