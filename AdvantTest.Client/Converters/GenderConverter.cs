using AdvantTestTask.Grpc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace AdvantTest.Client.Converters
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((Gender)value)
            {
                case Gender.Famale:
                    return "Женский";
                case Gender.Male:
                    return "Мужской";
                case Gender.None:
                    return "Не указано";
                default:
                    return "Не указано";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
