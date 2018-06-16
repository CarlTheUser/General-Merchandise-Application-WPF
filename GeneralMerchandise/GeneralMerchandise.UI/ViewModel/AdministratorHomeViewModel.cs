using GeneralMerchandise.UI.Command;
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
        public ObservableCollection<HomePageOption> UIOptions { get; private set; }

        public ICommand UINavigationCommand { get; private set; }

        public AdministratorHomeViewModel()
        {
            UIOptions = new ObservableCollection<HomePageOption>
            {
                new HomePageOption("Users", "View, add and enable or disable system users.", ""),
                new HomePageOption("Products", "View, modify existing products or add a new one.", ""),
                new HomePageOption("Profile", "View your account or edit account details.", "", NavigateToProfile),
                new HomePageOption("Settings", "Change settings.", ""),
                new HomePageOption("Reports and Analyses", "View Store performance's timely analysis and reports.", "")
            };

            UINavigationCommand = new ParameterizedRelayCommand<HomePageOption>(Name, null);
        }

        private void Name(HomePageOption parameter)
        {
            parameter?.SelectionAction?.Invoke();
        }

        private void NavigateToProfile()
        {
            App.Current.MainView.UserNavigation.Navigate(new Navigation.NavigationItem(Pages.ApplicationPage.ProfilePage));
        }



    }
}
