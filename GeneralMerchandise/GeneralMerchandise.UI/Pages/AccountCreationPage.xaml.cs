using GeneralMerchandise.UI.ViewModel;
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

namespace GeneralMerchandise.UI.Pages
{
    /// <summary>
    /// Interaction logic for RegisterUserPage.xaml
    /// </summary>
    public partial class AccountCreationPage : BasePage, IPasswordContainer, IConfirmPassword
    {
        public AccountCreationPage()
        {
            InitializeComponent();
        }

        public bool ArePasswordsMatch => PasswordField.Password.Equals(PasswordConfirmField.Password);

        public SecureString SecurePassword => PasswordField.SecurePassword;

        public string Password => PasswordField.Password;

        public bool HasPassword => PasswordField.Password.Trim().Length > 0;

        public void ClearPassword()
        {
            throw new NotImplementedException();
        }

        public override ViewModel.ViewModel GetViewModel()
        {
            return VM;
        }

        private void BasePage_Initialized(object sender, EventArgs e)
        {
            VM.Parameters = new Dictionary<int, object>()
            {
                { AccountCreationViewModel.PASSWORD_CONTAINER_PARAMETER, this },
                { AccountCreationViewModel.CONFIRM_PASSWORD_PARAMETER, this }
            };
        }
    }
}
