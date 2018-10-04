using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GeneralMerchandise.UI.Command
{
    public class ClearTextCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (typeof(TextBox).IsAssignableFrom(parameter.GetType())) (parameter as TextBox).Clear();
            else throw new ArgumentException("Given parameter is not an instance nor inherits from TextBox.", "parameter");
        }
    }
}
