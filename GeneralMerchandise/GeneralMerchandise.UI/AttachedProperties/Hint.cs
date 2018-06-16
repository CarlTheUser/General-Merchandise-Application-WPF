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

        #region Hint Text Property

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
            DependencyProperty.RegisterAttached("TextProperty", typeof(string), typeof(Hint), new UIPropertyMetadata(string.Empty));

        #endregion

        //Code for passswordbox placeholer borrowed from
        //https://stackoverflow.com/questions/1607066/wpf-watermark-passwordbox-from-watermark-textbox/1610761#1610761

        #region PasswordBoxHintEnabled Property

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

        #endregion

        #region PasswordBoxHasValue Property

        public static bool GetPasswordBoxHasValueProperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(PasswordBoxHasValueProperty);
        }

        public static void SetPasswordBoxHasValueProperty(DependencyObject obj, bool value)
        {
            obj.SetValue(PasswordBoxHasValueProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordBoxHasValueProperty =
            DependencyProperty.RegisterAttached("PasswordBoxHasValueProperty", typeof(bool), typeof(Hint), new UIPropertyMetadata(false));

        #endregion


        private static void Hint_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            SetPasswordBoxHasValueProperty(passwordBox, passwordBox.SecurePassword.Length > 0);
        }
    }
}
