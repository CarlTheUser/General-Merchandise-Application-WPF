using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Password
{
    interface IHashedPassword
    {
        /// <summary>
        /// Takes a password input and generates a salt
        /// and a hashed password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns>A Secured Password containing a hash and salt.</returns>
        SecuredPassword HashPassword(string password);


        bool VerifyPassword(string input, SecuredPassword password);
    }


}
