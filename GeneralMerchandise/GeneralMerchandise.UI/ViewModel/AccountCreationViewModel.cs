using GeneralMerchandise.Data.Client;
using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.UI.Command;
using GeneralMerchandise.UI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneralMerchandise.UI.ViewModel
{
    public class AccountCreationViewModel : ViewModel
    {
        public AccountModel Account { get; } = AccountModel.NewInstance();

        public UserModel User { get; } = UserModel.NewInstance();

        public ICommand RegisterCommand { get; private set; }

        private readonly RegisterOperation registerOperation = new RegisterOperation();

        public AccountCreationViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register()
        {
            AccountData account = new AccountData
            {
                Username = Account.Username,
                AccessType = Account.AccessType,
                IsActive = Account.IsActive
            };

            UserData user = new UserData
            {
                ImageFileLocation = User.ImageFileLocation,
                Firstname = User.Firstname,
                Middlename = User.Middlename,
                Lastname = User.Lastname,
                Gender = User.Gender,
                BirthDate = User.BirthDate,
                ContactNumber = User.ContactNumber,
                Email = User.Email,
                Address = User.Address
            };

            try
            {
                registerOperation.Register(account, user);
                App.Current.MainView.UserNavigation.NavigateBack();
                NotificationHub.GetInstance().ShowMessage("Registered a new user");
            }
            catch (Exception e)
            {
                NotificationHub.GetInstance().ShowMessage(e.Message);
            }
            
        }



    }
}
