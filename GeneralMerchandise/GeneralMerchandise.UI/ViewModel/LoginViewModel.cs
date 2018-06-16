using GeneralMerchandise.Data.Client;
using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.UI.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneralMerchandise.UI.ViewModel
{
    public class LoginViewModel : ViewModel
    {
        public static readonly int PASSWORD_CONTAINER_PARAMETER = 1;

        private IPasswordContainer PasswordContainer;

        private string username = string.Empty;

        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public ICommand LoginCommand { get; private set; }

        private readonly LoginOperation LoginOperation;

        public LoginViewModel()
        {
            LoginOperation = new LoginOperation();
            LoginOperation.LoginSucceed += LoginOperation_LoginSucceed;
            LoginOperation.LoginFailed += LoginOperation_LoginFailed;

            LoginCommand = new RelayCommand(Login, CanLogin);
        }
        
        protected override void OnParameterSet(IDictionary<int, object> parameters)
        {
            base.OnParameterSet(parameters);
            if(parameters.ContainsKey(PASSWORD_CONTAINER_PARAMETER))
            {
                object passwordContainer = parameters[PASSWORD_CONTAINER_PARAMETER];
                if (passwordContainer != null && typeof(IPasswordContainer).IsAssignableFrom(passwordContainer.GetType()))
                {
                    PasswordContainer = passwordContainer as IPasswordContainer;
                }
            }
            
        }

        private void Login()
        {
            LoginOperation.Login(username, PasswordContainer.Password);
        }

        private bool CanLogin()
        {
            bool hasUsername = Username.Length > 0;
            bool hasPassword = PasswordContainer != null ? PasswordContainer.Password.Length > 0 : false;
            return hasUsername && hasPassword;
        }

        private void LoginOperation_LoginSucceed(object sender, LoginOperation.LoginSuccessfulEventArgs e)
        {
            AccountData account = e.Account;
            LoginHandle.Instance.LoginAccount(new Model.AccountModel
            {
                Id = account.Id,
                Username = account.Username,
                AccessType = account.AccessType,
                IsActive = account.IsActive
            });
        }

        private void LoginOperation_LoginFailed(object sender, LoginOperation.LoginFailureEventArgs e)
        {
            //Show message
        }

        

    }
}
