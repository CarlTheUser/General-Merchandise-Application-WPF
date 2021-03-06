﻿using GeneralMerchandise.Common.Type;
using GeneralMerchandise.Data.Client;
using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.UI.Command;
using GeneralMerchandise.UI.Model;
using GeneralMerchandise.UI.ViewLauncher;
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

        private bool enabled = true;
        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        public LoginViewModel()
        {
            LoginOperation = new LoginOperation();
            LoginOperation.LoginSucceed += LoginOperation_LoginSucceed;
            LoginOperation.LoginFailed += LoginOperation_LoginFailed;

            LoginCommand = new RelayCommand(Login, CanLogin);

            //new AccountOperation().foo();
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
            if (PasswordContainer.HasPassword) LoginOperation.Login(username, PasswordContainer.Password);
            else NotificationHub.GetInstance().ShowMessage("Type your password.");
        }

        private bool CanLogin()
        {
            bool hasUsername = Username.Length > 0;
            return hasUsername && (PasswordContainer != null && PasswordContainer.HasPassword);
        }

        private void LoginOperation_LoginSucceed(object sender, LoginOperation.LoginSuccessfulEventArgs e)
        {
            AccountData account = e.Account;

            UserOperation userOperation = new UserOperation();

            UserData user = userOperation.GetFromAccount(account);

            if(user != null)
            {
                LoginHandle.Instance.LoginAccount(
                AccountModel.FromPersistentStorage(
                    account.Id,
                    account.Username,
                    account.AccessType,
                    account.IsActive));
                NotificationHub.GetInstance().ShowMessage("Account logged in");
            }
            else
            {
                Enabled = false;
                IProfileSetupViewLauncher profileSetupViewLauncher = new ProfileSetupWindowLauncher();
                profileSetupViewLauncher.Launch(new Dictionary<int, object>()
                {
                    { UserProfileCompletionViewModel.USER_ID_PARAMETER, account.Id }
                }); 
            }

            
        }

        private void LoginOperation_LoginFailed(object sender, LoginOperation.LoginFailureEventArgs e)
        {
            NotificationHub.GetInstance().ShowMessage(e.Message);
        }

        

    }
}
