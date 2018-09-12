using GeneralMerchandise.Data.Model;
using GeneralMerchandise.Data.Password;
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
        private readonly AccountQuery query = new AccountQuery();

        private readonly AccountQuery.UsernameFilter usernameFilter = new AccountQuery.UsernameFilter();

        private readonly IHashedPassword hashedPassword;

        public SQLLoginService()
        {
            query.Filter = usernameFilter;
            hashedPassword = new HashedPassword();
        }

        public override void Login(string accountIdentifier, string password)
        {
            usernameFilter.Username = accountIdentifier;
            var results = query.Execute().ToList();
            if (results != null && results.Count > 0)
            {
                AccountModel account = results[0];
                if (hashedPassword.VerifyPassword(password, account.Password))
                {
                    OnLoginSucceed(account);
                }
                else OnLoginFailed(string.Format("Wrong password for {0}", account.Username));
            }
            else OnLoginFailed("No accounts found with given username"); 
        }
    }
}
