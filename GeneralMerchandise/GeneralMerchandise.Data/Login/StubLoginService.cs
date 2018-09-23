using GeneralMerchandise.Data.Model;
using GeneralMerchandise.Data.Password;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Login
{
    internal class StubLoginService : LoginService
    {
        private readonly IHashedPassword hashedPassword;

        public StubLoginService()
        {
            hashedPassword = new HashedPassword();
        }

        public override void Login(string accountIdentifier, string password)
        {
            string sampleSalt = "bc7c6c875d779ac45e6294f6f41fea99";
            string sampleHash = "fc70bd64743f733484ab51c52a2924a5f5ecd1f8";

            SecuredPassword securedPassword = new SecuredPassword(sampleSalt, sampleHash);

            if(accountIdentifier == "admin")
            {
                if(hashedPassword.VerifyPassword(password, securedPassword))
                {
                    OnLoginSucceed(AccountModel.Existing(123234, "admin", sampleSalt, sampleHash, Common.Type.AccessType.Administrator, true));
                }
                else
                {
                    OnLoginFailed("Wrong password for admin");
                }
            }
        }


        
    }
}
