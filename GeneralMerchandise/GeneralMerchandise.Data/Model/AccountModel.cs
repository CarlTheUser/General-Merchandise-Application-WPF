using GeneralMerchandise.Common.Type;
using GeneralMerchandise.Data.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Model
{
    internal sealed class AccountModel : PersistibleModel<int>
    {
        private static readonly int DEAFULT_IDENTITY = 0;

        #region Static Factory Methods

        public static AccountModel New(string username, SecuredPassword password, AccessType accountType)
        {
            return new AccountModel
            {
                Identity = DEAFULT_IDENTITY,
                Username = username,
                Password = password,
                AccessType = accountType,
                IsActive = true
            };
        }

        public static AccountModel FromDB(int identity, string username, string salt, string passwordhash, AccessType accessType, bool active)
        {
            return new AccountModel
            {
                Identity = identity,
                Username = username,
                Password = new SecuredPassword(salt, passwordhash),
                AccessType = accessType,
                IsActive = active
            };
        }

        #endregion

        private AccountModel() { }

        public override int Identity { get; set; }
        public string Username { get; set; }
        public SecuredPassword Password { get; private set; }
        public AccessType AccessType { get; set; }
        public bool IsActive { get; set; }
    }
}
