using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralMerchandise.UI.Navigation
{
    public interface IUserNavigation
    {
        event EventHandler<PrenavigationEventArgs> Prenavigate;

        bool Navigate(NavigationItem navigationItem);

        void NavigateBack();

        void NavigateHome();

    }
}
