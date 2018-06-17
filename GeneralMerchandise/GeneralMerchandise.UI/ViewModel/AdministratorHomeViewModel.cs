using GeneralMerchandise.UI.Command;
using GeneralMerchandise.UI.Pages;
using GeneralMerchandise.UI.UIModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneralMerchandise.UI.ViewModel
{
    class AdministratorHomeViewModel : ViewModel
    {
        public ObservableCollection<HomePageOption> HomePageOptions { get; private set; }

        public ICommand UINavigationCommand { get; private set; }

        public AdministratorHomeViewModel()
        {
            HomePageOptions = new ObservableCollection<HomePageOption>
            {
                new HomePageOption("Users", "View, add and enable or disable system users.", "pack://application:,,,/Images/Icons/users-black-128.png", NavigateToUsers),
                new HomePageOption("Products", "View, modify existing products or add a new one.", "pack://application:,,,/Images/Icons/bag-black-128.png", NavigateToProducts),
                new HomePageOption("Profile", "View your account or edit account details.", "pack://application:,,,/Images/Icons/profile-black-128.png", NavigateToProfile),
                new HomePageOption("Settings", "Change settings.", "pack://application:,,,/Images/Icons/settings-thin-black-128.png", NavigateToSettings),
                new HomePageOption("Reports and Analyses", "View Store performance's timely analysis and reports.", "pack://application:,,,/Images/Icons/analysis-black-128.png", NavigateToReports)
            };

            UINavigationCommand = new ParameterizedRelayCommand<HomePageOption>(HomePageOptionSelected, null);
        }

        private void HomePageOptionSelected(HomePageOption parameter)
        {
            parameter?.SelectionAction?.Invoke();
        }

        private void NavigateToUsers()
        {
            NavigateTo(Pages.ApplicationPage.Users);
        }

        private void NavigateToProfile()
        {
            NavigateTo(Pages.ApplicationPage.ProfilePage);
        }

        private void NavigateToProducts()
        {
            NavigateTo(Pages.ApplicationPage.Products);
        }

        private void NavigateToSettings()
        {
            NavigateTo(ApplicationPage.AdministratorSettings);
        }

        private void NavigateToReports()
        {
            NavigateTo(ApplicationPage.Reports);
        }

        private void NavigateTo(ApplicationPage applicationPage)
        {
            App.Current.MainView.UserNavigation.Navigate(new Navigation.NavigationItem(applicationPage));
        }

    }
}
