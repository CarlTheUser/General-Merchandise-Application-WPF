using GeneralMerchandise.Data.Client.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace GeneralMerchandise.UI.Converter
{
    class UserDisplayDataFullnameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value.GetType() == typeof(UserDisplayData))
            {
                UserDisplayData u = (UserDisplayData)value;
                if (u.Middlename != null && u.Middlename.Trim().Length > 0) return $"{u.Firstname} {u.Middlename} {u.Lastname}";
                else return $"{u.Firstname} {u.Lastname}";
            }
            else return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
