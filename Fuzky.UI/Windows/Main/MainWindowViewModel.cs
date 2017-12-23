using Autofac;
using Fuzky.UI.Common;
using Fuzky.UI.Views.FirstChildView;
using Fuzky.UI.Views.LoginView;
using System.Windows;

namespace Fuzky.UI.Windows.Main
{
    public class MainWindowViewModel : BaseWindowViewModel, IMainWindowViewModel
    {
        public MainWindowViewModel(IMainWindow window, IComponentContext container)
            : base(window, container)
        {
            this.ExitCommand = new DelegateCommand(OnExitCommand);
            this.ShowFirstChildCommand = new DelegateCommand(OnShowFirstChildCommand);
            this.ShowLoginViewCommand = new DelegateCommand(OnShowLoginViewCommand);
        }

        public DelegateCommand ExitCommand { get; }
        public DelegateCommand ShowFirstChildCommand { get; }
        public DelegateCommand ShowLoginViewCommand { get; }

        private void OnExitCommand(object o)
        {
            Application.Current.Shutdown();
        }

        private void OnShowFirstChildCommand(object o)
        {
            this.ShowView<IFirstChildViewModel>();
        }

        private void OnShowLoginViewCommand(object o)
        {
            this.ShowView<ILoginViewModel>();
        }
    }
}