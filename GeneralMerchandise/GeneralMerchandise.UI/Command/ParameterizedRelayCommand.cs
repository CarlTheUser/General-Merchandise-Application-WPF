﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeneralMerchandise.UI.Command
{
    public class ParameterizedRelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        readonly Action<T> Command;

        readonly Predicate<T> IsExecutable;

        public ParameterizedRelayCommand(Action<T> commandAction) :this(commandAction, null) { }

        public ParameterizedRelayCommand(Action<T> commandAction, Predicate<T> isExecutable)
        {
            if (commandAction == null) throw new ArgumentNullException("commandAction");

            Command = commandAction;
            IsExecutable = isExecutable;
        }

        public bool CanExecute(object parameter)
        {
            return IsExecutable == null ? true : IsExecutable.Invoke((T)parameter);
        }

        public void Execute(object parameter)
        {
            Command.Invoke((T)parameter);
        }
    }
}
