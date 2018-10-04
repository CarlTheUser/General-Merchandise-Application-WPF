using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace GeneralMerchandise.UI.Converter
{
    class ThicknessAdditionConverter : IValueConverter
    {

        public Thickness BaseThickness { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() == typeof(Thickness))
            {
                Thickness t = (Thickness)value;
                t.Left += BaseThickness.Left;
                t.Top += BaseThickness.Top;
                t.Right += BaseThickness.Right;
                t.Bottom += BaseThickness.Bottom;
                return t;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
