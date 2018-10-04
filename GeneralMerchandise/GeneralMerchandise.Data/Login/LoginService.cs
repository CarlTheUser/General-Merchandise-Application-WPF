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

        public abstract ILoginResult Login(string accountIdentifier, string password);

        //Creates an interface so I can hide the constructor of implementing protected class
        //while interface is publicly viewable but couldnt be instantiated
        public interface ILoginResult
        {
            bool IsSuccessful { get; }
            AccountModel Account { get; }
            string Message { get; }
        }

        protected class LoginResult : ILoginResult
        {
            public bool IsSuccessful { get; private set; } = false;

            public AccountModel Account { get; private set; }

            public string Message { get; private set; }

            public LoginResult(AccountModel account)
            {
                IsSuccessful = (Account = account) != null;
            }

            public LoginResult(string message)
            {
                Message = message;
            }
        }

    }
}
