using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using GeneralMerchandise.UI.ViewModel;

namespace GeneralMerchandise.UI.Pages
{
    public abstract class BasePage : Page, IApplicationView
    {

        public static readonly IDictionary<ApplicationPage, Type> ApplicationPageRegistry;

        static BasePage()
        {
            ApplicationPageRegistry = new Dictionary<ApplicationPage, Type>()
            {
                { ApplicationPage.Nothing, typeof(NothingPage) },
                { ApplicationPage.Login, typeof(LoginPage) },
                { ApplicationPage.AdministratorPage, typeof(AdministratorHomePage) },
                { ApplicationPage.ProfilePage, typeof(ProfilePage) },
                { ApplicationPage.Products, typeof(ProductsPage) },
                { ApplicationPage.Users, typeof(UsersPage) },
                { ApplicationPage.AdministratorSettings, typeof(AdministratorSettingsPage) },
                { ApplicationPage.Reports, typeof(ReportsAndAnalysesPage) }
            };
        }

        public abstract ViewModel.ViewModel GetViewModel();

    }
}
