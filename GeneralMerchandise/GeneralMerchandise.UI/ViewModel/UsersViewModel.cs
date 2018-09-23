using GeneralMerchandise.Data.Client;
using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.UI.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneralMerchandise.UI.ViewModel
{
    public class UsersViewModel : ViewModel
    {

        private readonly UserOperation userOperation = new UserOperation();

        public ICommand NewUserCommand { get; private set; }

        public ICommand FilterActiveCommand { get; private set; }
        
        public ObservableCollection<UserDisplayData> UsersDisplay { get; private set; }

        private readonly IEnumerable<UserDisplayData> Users;

        private string search = string.Empty;

        public string Search
        {
            get => search;
            set
            {
                SearchUser(search = value);
                OnPropertyChanged("Search");
                
            }
        }

        public UsersViewModel()
        {
            Users = new List<UserDisplayData>()
            {
                new UserDisplayData
                {
                    Id = 1,
                    Firstname = "John",
                    Lastname = "Santos",
                    IsActive = true,
                    Created = DateTime.Now
                },

                new UserDisplayData
                {
                    Id = 1,
                    Firstname = "Nancy",
                    Lastname = "Drew",
                    IsActive = true,
                    Created = DateTime.MinValue
                },
                new UserDisplayData
                {
                    Id = 1,
                    Firstname = "John Michael",
                    Lastname = "Gaddi",
                    IsActive = true,
                    Created = DateTime.MinValue
                },
                new UserDisplayData
                {
                    Id = 1,
                    Firstname = "Michelle",
                    Lastname = "Flaunders",
                    IsActive = false,
                    Created = DateTime.MinValue
                },
                new UserDisplayData
                {
                    Id = 1,
                    Firstname = "Dan",
                    Lastname = "Erricson",
                    IsActive = true,
                    Created = DateTime.MinValue
                },

            };

            UsersDisplay = new ObservableCollection<UserDisplayData>(Users);

            NewUserCommand = new RelayCommand(NavigateToUserCreation);

            FilterActiveCommand = new ParameterizedRelayCommand<FilterActiveProperty>(Filter);
            
        }

        private void NavigateToUserCreation()
        {
            App.Current.MainView.UserNavigation.Navigate(new Navigation.NavigationItem(Pages.ApplicationPage.Register, false));
        }

        private void Filter(FilterActiveProperty filter)
        {
            switch(filter)
            {
                case FilterActiveProperty.Active: UsersDisplay = new ObservableCollection<UserDisplayData>(Users.Where(x => x.IsActive));
                    break;
                case FilterActiveProperty.Deactivated: UsersDisplay = new ObservableCollection<UserDisplayData>(Users.Where(x => !x.IsActive));
                    break;
                default: UsersDisplay = new ObservableCollection<UserDisplayData>(Users);
                    break;
            }
            OnPropertyChanged("UsersDisplay");
        }

        private void SearchUser(string name)
        {
            if(name.Trim().Length == 0)
            {
                UsersDisplay = new ObservableCollection<UserDisplayData>(Users);
            } else
            {
                name = name.ToLower();
                UsersDisplay = new ObservableCollection<UserDisplayData>(UsersDisplay.Where(x =>
                {
                    if(x.Middlename != null && x.Middlename.Trim().Length > 0) return x.Firstname.ToLower().Contains(name) || x.Middlename.ToLower().Contains(name) || x.Lastname.ToLower().Contains(name);
                    else return x.Firstname.ToLower().Contains(name) || x.Lastname.ToLower().Contains(name);
                }));
            }
            OnPropertyChanged("UsersDisplay");
        }

        public enum FilterActiveProperty
        {
            None,
            Active,
            Deactivated
        }
    }
}
