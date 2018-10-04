using GeneralMerchandise.Data.Client.Data;
using GeneralMerchandise.Data.Model;
using GeneralMerchandise.Data.Password;
using GeneralMerchandise.Data.Provider;
using GeneralMerchandise.Data.Provider.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Client
{
    public class AccountOperation : IFallibleOperation
    {
        public event EventHandler<FallibleOperationEventArgs> ErrorOccured;

        public void Save(AccountData account, SecureString password)
        {
            if(account == null) throw new ArgumentNullException("account");
            {
                IHashedPassword hashedPassword = new HashedPassword();

                SecuredPassword securedPassword = hashedPassword.HashPassword(password.GetStringFinalize());

                AccountModel accountModel = AccountModel.New(
                    account.Username,
                    securedPassword,
                    account.AccessType);

                AccountPersistence persistence = new MySQLAccountPersistence();

                persistence.Save(accountModel);
            }
        }
    }
}
