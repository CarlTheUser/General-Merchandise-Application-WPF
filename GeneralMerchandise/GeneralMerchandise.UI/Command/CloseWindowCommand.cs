using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GeneralMerchandise.UI.Command
{
    public class CloseWindowCommand : ICommand
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
            if (typeof(Window).IsAssignableFrom(parameter.GetType())) (parameter as Window).Close();
            else throw new ArgumentException("Given parameter is not an instance nor inherits from Window.", "parameter");
        }
    }
}
