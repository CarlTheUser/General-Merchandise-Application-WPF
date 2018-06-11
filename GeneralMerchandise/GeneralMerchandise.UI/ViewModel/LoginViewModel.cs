using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.ViewModel
{
    public class LoginViewModel : ViewModel
    {
        private string username = "username";

        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged("Username");
                }
            }
        }
    }
}
