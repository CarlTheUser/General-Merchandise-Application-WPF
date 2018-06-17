using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for NothingPage.xaml
    /// </summary>
    public partial class NothingPage : BasePage
    {
        public NothingPage()
        {
            InitializeComponent();
        }

        public override ViewModel.ViewModel GetViewModel()
        {
            return VM;
        }
    }
}
