using GeneralMerchandise.UI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneralMerchandise.UI.ViewModel
{
    public class UserDisplayViewModel : ViewModel
    {

        public ICommand LogoutCommand { get; private set; }

        public ICommand ViewProfileCommand { get; private set; }

        public UserDisplayViewModel()
        {
            LogoutCommand = new RelayCommand(Logout, HasUser);
            ViewProfileCommand = new RelayCommand(DisplayProfile, HasUser);
        }

        private void Logout()
        {
            LoginHandle.Instance.LogOut();
        }

        private bool HasUser()
        {
            return LoginHandle.Instance.HasLogin;
        }

        private void DisplayProfile()
        {
            App.Current.MainView.UserNavigation.Navigate(new Navigation.NavigationItem(Pages.ApplicationPage.ProfilePage));
        }

    }
}
