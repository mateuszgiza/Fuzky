using Autofac;
using Fuzky.UI.Common;

namespace Fuzky.UI.Views.FirstChildView
{
    public class FirstChildViewModel : BaseViewModel, IFirstChildViewModel
    {
        public FirstChildViewModel(IFirstChildView view, IComponentContext container)
            : base(view, container)
        {
        }
    }
}