using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GeneralMerchandise.UI.AttachedProperties
{
    class Hint
    {


        public static string GetTextProperty(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetTextProperty(DependencyObject obj, string value)
        {
            obj.SetValue(TextProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("TextProperty", typeof(string), typeof(Hint), new PropertyMetadata(""));




        public static bool GetPasswordBoxHintEnabledProperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(PasswordBoxHintEnabledProperty);
        }

        public static void SetPasswordBoxHintEnabledProperty(DependencyObject obj, bool value)
        {
            obj.SetValue(PasswordBoxHintEnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordBoxHintEnabledProperty =
            DependencyProperty.RegisterAttached("PasswordBoxHintEnabledProperty", typeof(bool), typeof(Hint), new PropertyMetadata(false, PasswordBoxHintEnabledPropertyChanged));

        private static void PasswordBoxHintEnabledPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                ((PasswordBox)d).PasswordChanged += Hint_PasswordChanged;
            }
            else
            {
                ((PasswordBox)d).PasswordChanged -= Hint_PasswordChanged;
            }
        }

        private static void Hint_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if(((PasswordBox)sender).SecurePassword.Length > 0)
            {
                
            }
        }
    }
}
