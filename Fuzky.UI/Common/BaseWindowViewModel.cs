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

        protected IViewModel ViewModel;

        public IWindow Window { get; set; }
        public IView View { get; set; }
        public IComponentContext Container { get; set; }

        protected void ShowView<TViewModel>()
            where TViewModel : IViewModel
        {
            if (this.ViewModel is TViewModel)
                return;

            this.ViewModel = Container.Resolve<TViewModel>();
            this.ViewModel.Parent = this;
            this.View = this.ViewModel.View;
        }
    }
}