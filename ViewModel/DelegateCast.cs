using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Smiles.MvvM.Lib
{
    public class DelegateCast : ICommand 
    {
         private readonly Action _action;

        public DelegateCast(Action action)
        {
            _action = action;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

#pragma warning disable 67
        public event EventHandler CanExecuteChanged; 
#pragma warning restore 67
    }
}
