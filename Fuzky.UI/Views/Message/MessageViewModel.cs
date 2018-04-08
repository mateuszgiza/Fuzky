using Autofac;
using Fuzky.UI.Common;

namespace Fuzky.UI.Views.Message
{
    public class MessageViewModel : BaseViewModel, IMessageViewModel
    {
        public MessageViewModel(IMessageView view, IComponentContext container)
            : base(view, container)
        {
        }

        public string Message { get; set; }
    }
}