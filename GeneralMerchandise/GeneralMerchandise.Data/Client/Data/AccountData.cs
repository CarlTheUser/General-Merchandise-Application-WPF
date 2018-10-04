using GeneralMerchandise.Common.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Client.Data
{
    //DTO class
    public sealed class AccountData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public AccessType AccessType { get; set; }
        public bool IsActive { get; set; }
    }
}
