using Fuzky.UI.Common;

namespace Fuzky.UI.Views.Message
{
    public interface IMessageViewModel : IViewModel
    {
        string Message { get; set; }
    }
}