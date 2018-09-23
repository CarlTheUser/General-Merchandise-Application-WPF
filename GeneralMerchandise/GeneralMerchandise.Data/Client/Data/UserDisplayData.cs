using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.Data.Client.Data
{
    public class UserDisplayData
    {
        public int Id { get; set; }
        public string ImageFileLocation { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public bool IsActive { get; set; }
        public DateTime Created { get; set; }
    }
}
