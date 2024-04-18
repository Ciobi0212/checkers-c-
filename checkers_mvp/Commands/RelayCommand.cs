using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace checkers_mvp.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> Execute { get; set; }
        private Predicate<object> CanExecute { get; set; }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            Execute = execute;
            CanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute(parameter);
        }

        void ICommand.Execute(object parameter)
        {
           Execute(parameter);
        }
    }
}
