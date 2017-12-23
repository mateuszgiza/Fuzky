using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Fuzky.UI.Common
{
    public class AsyncCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Func<object, Task> _execute;
        private readonly ICommandExecutionPolicy policy;

        public event EventHandler CanExecuteChanged;

        public AsyncCommand(Func<object, Task> execute)
            : this(execute, (Predicate<object>)null)
        {
        }

        public AsyncCommand(Func<object, Task> execute, ICommandExecutionPolicy policy)
            : this(execute, (Predicate<object>)null)
        {
            this.policy = policy;
        }

        public AsyncCommand(Func<object, Task> execute, Predicate<object> canExecute)
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

        public async void Execute(object parameter)
        {
            this.policy?.BeforeExecution();
            await Task.Factory.StartNew(async () => await this._execute(parameter));
            this.policy?.AfterExecution();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}