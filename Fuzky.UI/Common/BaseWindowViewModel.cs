using Autofac;

namespace Fuzky.UI.Common
{
    public class BaseWindowViewModel : BaseNotification, IWindowViewModel
    {
        public BaseWindowViewModel(IWindow window, IComponentContext container)
        {
            this.Window = window;
            this.Window.DataContext = this;

            this.Container = container;
        }

        private IViewModel viewModel;

        public IWindow Window { get; set; }
        public IView View { get; set; }
        public IComponentContext Container { get; set; }

        protected void ShowView<TViewModel>()
            where TViewModel : IViewModel
        {
            if (this.viewModel is TViewModel)
                return;

            this.viewModel = Container.Resolve<TViewModel>();
            this.viewModel.Parent = this;
            this.View = this.viewModel.View;
        }
    }
}