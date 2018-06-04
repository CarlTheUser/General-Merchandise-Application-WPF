using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Password
{
    internal class SecuredPassword
    {

        private readonly SecureString hashedPassword;

        public SecureString HashedPassword => hashedPassword;

        private readonly SecureString salt;

        public SecureString Salt => salt;

        public SecuredPassword(string salt, string hashedPassword)
        {
            if (salt == null) throw new ArgumentNullException("salt");
            if (hashedPassword == null) throw new ArgumentNullException("hashedPassword");

            this.hashedPassword = SecuredStringUtility.CreateFromString(hashedPassword);
            this.salt = SecuredStringUtility.CreateFromString(salt);
        }
    }
}
