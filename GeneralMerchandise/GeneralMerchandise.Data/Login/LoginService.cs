using GeneralMerchandise.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Login
{
    internal abstract class LoginService
    {

        public event EventHandler<LoginSuccessfulEventArgs> LoginSucceed;

        public event EventHandler<LoginFailureEventArgs> LoginFailed;

        public abstract void Login(string accountIdentifier, string password);

        protected virtual void OnLoginSucceed(AccountModel account)
        {
            if (LoginSucceed != null) LoginSucceed.Invoke(this, new LoginSuccessfulEventArgs(account));
            else
            {
                //log no listeners
            }
        }

        protected virtual void OnLoginFailed(string message)
        {
            if (LoginFailed != null) LoginFailed.Invoke(this, new LoginFailureEventArgs(message));
            else
            {
                //log no listeners
            }
        }

        public class LoginSuccessfulEventArgs : EventArgs
        {
            public AccountModel Account { get; }

            public LoginSuccessfulEventArgs(AccountModel account)
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
