using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.Data.Login;
using GeneralMerchandise.Data.Model;
using GeneralMerchandise.Data.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Client
{
    public class LoginOperation
    {
        public event EventHandler<LoginSuccessfulEventArgs> LoginSucceed;

        public event EventHandler<LoginFailureEventArgs> LoginFailed;

        private readonly LoginService loginService;

        public LoginOperation()
        {
            loginService = new StubLoginService();
            loginService.LoginSucceed += LoginService_LoginSucceed;
            loginService.LoginFailed += LoginService_LoginFailed;
        }
        
        public void Login(string accountIdentifier, string inputPassword)
        {
            if(accountIdentifier.Trim().Length == 0)
            {
                OnLoginFailed("Account identifier is blank");
                return;
            }

            if (inputPassword.Trim().Length == 0)
            {
                OnLoginFailed("Input password is blank");
                return;
            }
            loginService.Login(accountIdentifier, inputPassword);

        }

        private void LoginService_LoginSucceed(object sender, LoginService.LoginSuccessfulEventArgs e)
        {
            AccountModel account = e.Account;
            OnLoginSucceed(new AccountData
            {
                Id = account.Identity,
                Username = account.Username,
                AccessType = account.AccessType,
                IsActive = account.IsActive
            });
        }

        private void LoginService_LoginFailed(object sender, LoginService.LoginFailureEventArgs e)
        {
            OnLoginFailed(e.Message);
        }

        private void OnLoginSucceed(AccountData account)
        {
            if (LoginSucceed != null) LoginSucceed.Invoke(this, new LoginSuccessfulEventArgs(account));
            else
            {
                //log no listeners
            }
        }

        private void OnLoginFailed(string message)
        {
            if (LoginFailed != null) LoginFailed.Invoke(this, new LoginFailureEventArgs(message));
            else
            {
                //log no listeners
            }
        }

        public class LoginSuccessfulEventArgs : EventArgs
        {

            public AccountData Account { get; }

            public LoginSuccessfulEventArgs(AccountData account)
            {
                Account = account;
            }
        }

        public class LoginFailureEventArgs : EventArgs
        {
            public string Message { get; private set; }
            public LoginFailureEventArgs(string message)
            {
                Message = message;
            }
        }
    }
}
