using Fuzky.UI.Common;

namespace Fuzky.UI.Windows.MessageBox
{
    public interface IMessageBoxViewModel : IWindowViewModel
    {
        string Title { get; set; }
        string Message { get; set; }
    }
}