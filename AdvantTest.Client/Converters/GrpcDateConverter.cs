using Google.Protobuf.WellKnownTypes;
using System;
using System.Globalization;
using System.Windows.Data;

namespace AdvantTest.Client.Converters
{
    public class GrpcDateConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if(value is null)
            {
                return null;
            }
            return  (value as Timestamp).ToDateTime();
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }
            return Timestamp.FromDateTimeOffset((DateTime)value);
        }
    }
}
