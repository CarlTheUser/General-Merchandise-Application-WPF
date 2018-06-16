using GeneralMerchandise.UI.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneralMerchandise.UI.Command
{
    public class NavigateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(typeof(NavigationItem).IsAssignableFrom(parameter.GetType()))
            {
                NavigationItem navigationItem = parameter as NavigationItem;
                App.Current.MainView.UserNavigation.Navigate(navigationItem);
            }
            else throw new ArgumentException("Given parameter is not an instance nor inherits from NavigationItem.", "parameter");
        }
    }
}
