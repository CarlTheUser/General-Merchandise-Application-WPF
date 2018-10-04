using GeneralMerchandise.Data.Client.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI
{
    public static class DataExtension
    {
        public static string Fullname(this UserDisplayData user)
        {
            return user.Middlename != null && user.Middlename.Trim().Length > 0 ? 
                $"{user.Firstname} {user.Middlename} {user.Lastname}" : $"{user.Firstname} {user.Lastname}";
        }
    }
}
