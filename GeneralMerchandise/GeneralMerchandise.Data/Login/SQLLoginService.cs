using GeneralMerchandise.Data.Model;
using GeneralMerchandise.Data.Password;
using GeneralMerchandise.Data.Provider;
using GeneralMerchandise.Data.Provider.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Login
{
    class SQLLoginService : LoginService
    {
       

        private readonly IHashedPassword hashedPassword;

        public SQLLoginService()
        {
            hashedPassword = new HashedPassword();
        }

        public override ILoginResult Login(string accountIdentifier, string password)
        {

            Query<AccountModel, string> query = new AccountQuery
            {
                Filter = new AccountQuery.UsernameFilter(accountIdentifier)
            };

            var results = query.Execute().ToList();

            if (results != null && results.Count > 0)
            {
                AccountModel account = results[0];
                if (hashedPassword.VerifyPassword(password, account.Password))
                {
                    return new LoginResult(account);
                }
                else return new LoginResult(string.Format("Wrong password for {0}", account.Username));
            }
            else return new LoginResult("No accounts found with given username");
        }
    }
}
