using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI
{
    interface IPasswordContainer
    {
        SecureString SecurePassword { get; }
        string Password { get; }
    }
}
