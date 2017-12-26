using Fuzky.UI.Common;

namespace Fuzky.UI.Windows.Dialog
{
    public interface IDialogViewModel<TView> : IWindowViewModel
    {
        string Title { get; set; }
        string Message { get; set; }
    }
}