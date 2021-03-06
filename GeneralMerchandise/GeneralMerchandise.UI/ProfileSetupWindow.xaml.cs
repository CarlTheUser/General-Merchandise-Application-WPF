﻿using GeneralMerchandise.UI.Pages;
using GeneralMerchandise.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeneralMerchandise.UI
{
    /// <summary>
    /// Interaction logic for ProfileSetupWindow.xaml
    /// </summary>
    public partial class ProfileSetupWindow : Window, IApplicationView
    {
        UserProfileCompletionPage page = new UserProfileCompletionPage();

        public ProfileSetupWindow()
        {
            InitializeComponent();
            PageDisplay.Navigate(page);
        }

        public ViewModel.ViewModel GetViewModel()
        {
            return page.GetViewModel();
        }
    }
}
