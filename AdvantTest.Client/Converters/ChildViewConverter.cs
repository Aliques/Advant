using System;
using System.Globalization;
using System.Windows.Data;

namespace AdvantTest.Client.Converters
{
    public class ChildViewConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((bool)value)
            {
                return "Есть";
            }
            else 
            { 
                return "Нет"; 
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
