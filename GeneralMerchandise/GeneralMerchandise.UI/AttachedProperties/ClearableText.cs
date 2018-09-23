using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeneralMerchandise.UI.AttachedProperties
{
    class ClearableText
    {


        public static bool GetEnableClearTextProperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableClearTextProperty);
        }

        public static void SetEnableClearTextProperty(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableClearTextProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableClearTextProperty =
            DependencyProperty.RegisterAttached("EnableClearTextProperty", typeof(bool), typeof(ClearableText), new UIPropertyMetadata(false));


    }
}
