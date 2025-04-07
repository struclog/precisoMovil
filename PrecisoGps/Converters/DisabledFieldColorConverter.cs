using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace PrecisoGps.Converters
{
    public class DisabledFieldColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isEnabled = (bool)value;
            return isEnabled ? Color.White : Color.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
