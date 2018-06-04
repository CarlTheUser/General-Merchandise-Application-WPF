using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GeneralMerchandise.UI.ViewModel;

namespace GeneralMerchandise.UI
{
    interface IApplicationView
    {
        ViewModel.ViewModel GetViewModel();
    }
}
