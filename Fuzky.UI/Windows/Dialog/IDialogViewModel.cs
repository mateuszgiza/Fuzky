using Fuzky.UI.Common;

namespace Fuzky.UI.Windows.Dialog
{
    public interface IDialogViewModel<out TView> : IWindowViewModel
        where TView : IViewModel
    {
        string Title { get; set; }

        TView View { get; }
    }
}