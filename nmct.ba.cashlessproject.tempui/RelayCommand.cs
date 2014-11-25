using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace nmct.ba.cashlessproject.tempui
{
    class RelayCommand : ICommand
    {
        private Action _action;
        public RelayCommand(Action a)
        {
            _action = a;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            _action();
        }

    }
}
