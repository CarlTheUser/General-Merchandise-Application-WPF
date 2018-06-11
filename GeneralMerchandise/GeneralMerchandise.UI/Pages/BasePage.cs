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
                { ApplicationPage.Login, typeof(LoginPage) },
                { ApplicationPage.ProfilePage, typeof(ProfilePage) }
            };
        }

        public abstract ViewModel.ViewModel GetViewModel();

    }
}
