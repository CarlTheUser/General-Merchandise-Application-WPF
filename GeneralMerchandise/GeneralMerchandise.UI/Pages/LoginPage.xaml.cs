using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GeneralMerchandise.UI.ViewModel;

namespace GeneralMerchandise.UI.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : BasePage, IPasswordContainer
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public SecureString SecurePassword => PasswordField.SecurePassword;

        public string Password => PasswordField.Password;

        public bool HasPassword => PasswordField.Password.Trim().Length > 0;

        public void ClearPassword()
        {
            PasswordField.Password = string.Empty;
        }

        public override ViewModel.ViewModel GetViewModel()
        {
            return VM;
        }

        private void BasePage_Initialized(object sender, EventArgs e)
        {
            VM.Parameters = new Dictionary<int, object>
            {
                { LoginViewModel.PASSWORD_CONTAINER_PARAMETER, this }
            };
        }
    }
}
