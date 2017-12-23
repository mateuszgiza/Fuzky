namespace Fuzky.UI.Common
{
    public class OneAtATimeCommandPolicy : ICommandExecutionPolicy
    {
        private bool isExecuting;

        public bool CanExecute(object param)
            => !this.isExecuting;

        public void BeforeExecution()
            => this.isExecuting = true;

        public void AfterExecution()
            => this.isExecuting = false;
    }
}