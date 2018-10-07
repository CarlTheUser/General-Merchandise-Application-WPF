using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.Data.Model;
using GeneralMerchandise.Data.Password;
using GeneralMerchandise.Data.Provider;
using GeneralMerchandise.Data.Provider.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Client
{
    public sealed class LoginOperation
    {

        private readonly IHashedPassword hashedPassword;

        public LoginOperation() => hashedPassword = new HashedPassword(); 

        public ILoginResult Login(string accountIdentifier, string inputPassword)
        {
            Query<AccountModel, string> query = new AccountQuery();

            query.Filter = new AccountQuery.UsernameFilter(accountIdentifier);
            
            var results = query.Execute().ToList();

            if (results != null && results.Count > 0)
            {
                AccountModel account = results[0];

                if (hashedPassword.VerifyPassword(inputPassword, account.Password))
                {
                    if (account.IsActive)
                    {
                        return new LoginResult(new AccountData
                        {
                            Id = account.Identity,
                            Username = account.Username,
                            AccessType = account.AccessType,
                            IsActive = account.IsActive
                        });
                    }
                    else return new LoginResult(string.Format("{0} is deactivated", account.Username));
                }
                else return new LoginResult(string.Format("Wrong password for {0}", account.Username));
            }
            else return new LoginResult("No accounts found with given username");
        }

        public interface ILoginResult
        {
            bool IsSuccessful { get; }
            AccountData Account { get; }
            string Message { get; }
        }

        private class LoginResult : ILoginResult
        {
            public bool IsSuccessful { get; private set; } = false;

            public AccountData Account { get; private set; }

            public string Message { get; private set; }

            public LoginResult(AccountData account) => IsSuccessful = (Account = account) != null;

            public LoginResult(string message) => Message = message; 
        }
    }
}
