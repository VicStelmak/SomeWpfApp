using System;
using System.Windows.Input;

namespace SomeWpfApp
{
    internal class RelayCommand : ICommand            
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute; 

        internal RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter) => _execute();

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
    }
}
