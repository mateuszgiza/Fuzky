namespace Fuzky.UI.Common
{
    public interface ICommandExecutionPolicy
    {
        bool CanExecute(object param);
        void BeforeExecution();
        void AfterExecution();
    }
}