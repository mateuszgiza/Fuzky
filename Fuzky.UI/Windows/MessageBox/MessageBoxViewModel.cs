using Autofac;
using Fuzky.UI.Common;
using System.Windows;
using System.Windows.Input;

namespace Fuzky.UI.Windows.MessageBox
{
    public class MessageBoxViewModel : BaseWindowViewModel, IMessageBoxViewModel
    {
        public MessageBoxViewModel(IMessageBox window, IComponentContext container)
            : base(window, container)
        {
            this.OkCommand = new DelegateCommand(OnOkCommand, CommandExecutionPolicies.OneAtATime);
        }

        public string Title { get; set; } = "Information";
        public string Message { get; set; }

        public ICommand OkCommand { get; }

        private void OnOkCommand(object o)
        {
            (this.Window as Window)?.Close();
        }
    }
}