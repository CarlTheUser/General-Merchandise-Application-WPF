using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Password
{
    interface IHashedPassword
    {
        SecuredPassword HashPassword(string password);
        bool VerifyPassword(string input, SecuredPassword password);
    }


}
