using GeneralMerchandise.Common.Type;
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
        public static int PASSWORD_CONTAINER_PARAMETER = 1;
        public static int CONFIRM_PASSWORD_PARAMETER = 2;

        public AccountModel Account { get; } = AccountModel.NewInstance();

        public ICommand RegisterCommand { get; private set; }

        public AccessType[] AccessTypes { get { return new AccessType[] { AccessType.Administrator, AccessType.Cashier }; } }

        private IPasswordContainer PasswordContainer;

        private IConfirmPassword ConfirmPassword;

        public AccountCreationViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }

        private void Register()
        {

            if (ConfirmPassword.ArePasswordsMatch)
            {
                if (PasswordContainer.HasPassword)
                {
                    try
                    {
                        //AccountOperation accountOperation = new AccountOperation();

                        AccountData account = new AccountData
                        {
                            Username = Account.Username,
                            AccessType = Account.AccessType,
                            IsActive = Account.IsActive = true
                        };

                        //accountOperation.Save(account, PasswordContainer.SecurePassword);

                        App.Current.MainView.UserNavigation.NavigateBack();
                        NotificationHub.GetInstance().ShowMessage("Registered a new user");
                    }
                    catch (Exception e)
                    {
                        NotificationHub.GetInstance().ShowMessage(e.Message);
                    }
                }
                else NotificationHub.GetInstance().ShowMessage("No password found.");
            }
            else NotificationHub.GetInstance().ShowMessage("Passwords do not match.");
            
            
        }

        protected override void OnParameterSet(IDictionary<int, object> parameters)
        {
            if (parameters.ContainsKey(PASSWORD_CONTAINER_PARAMETER))
            {
                PasswordContainer = (IPasswordContainer)parameters[PASSWORD_CONTAINER_PARAMETER];
            }
            if (parameters.ContainsKey(CONFIRM_PASSWORD_PARAMETER))
            {
                ConfirmPassword = (IConfirmPassword)parameters[CONFIRM_PASSWORD_PARAMETER];
            }
            base.OnParameterSet(parameters);
        }

    }
}
