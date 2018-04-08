using Autofac;
using Fuzky.UI.Common;
using System.Windows;
using System.Windows.Input;

namespace Fuzky.UI.Windows.Dialog
{
    public class DialogViewModel<TView> : BaseWindowViewModel, IDialogViewModel<TView>
        where TView : IViewModel
    {
        public DialogViewModel(IDialog window, IComponentContext container)
            : base(window, container)
        {
            base.ShowView<TView>();

            this.OkCommand = new DelegateCommand(CloseDialogCommand, CommandExecutionPolicies.OneAtATime);
        }

        public string Title { get; set; } = "Information";

        TView IDialogViewModel<TView>.View => (TView)base.ViewModel;

        public ICommand OkCommand { get; }

        internal void CloseDialogCommand(object o)
        {
            (this.Window as Window)?.Close();
        }
    }
}