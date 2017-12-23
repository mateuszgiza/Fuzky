using System;
using System.Windows.Input;

namespace Fuzky.UI.Common
{
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;
        private readonly ICommandExecutionPolicy policy;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<object> execute)
            : this(execute, (Predicate<object>)null)
        {
        }


        public DelegateCommand(Action<object> execute, ICommandExecutionPolicy policy)
            : this(execute, (Predicate<object>)null)
        {
            this.policy = policy;
        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter)
                   ?? this.policy?.CanExecute(parameter)
                   ?? true;
        }

        public void Execute(object parameter)
        {
            this.policy?.BeforeExecution();
            _execute(parameter);
            this.policy?.AfterExecution();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}