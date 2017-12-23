using Autofac;
using Fuzky.UI.Common;
using Fuzky.UI.Windows.MessageBox;

namespace Fuzky.UI
{
    public class UiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<MainWindow>()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            //builder.RegisterType<LoginView>()
            //    .AsImplementedInterfaces();

            //builder.RegisterType<FirstChildView>()
            //    .AsImplementedInterfaces();

            //builder.RegisterType<MainWindowViewModel>()
            //    .AsImplementedInterfaces()
            //    .SingleInstance();

            //builder.RegisterType<LoginViewModel>()
            //    .AsImplementedInterfaces();

            //builder.RegisterType<FirstChildViewModel>()
            //    .AsImplementedInterfaces();

            this.RegisterWindows(builder);
            this.RegisterWindowsViewModels(builder);
            this.RegisterViews(builder);
            this.RegisterViewsViewModels(builder);
        }

        private void RegisterWindows(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IWindow>()
                .Where(x => !x.IsAssignableTo<IMessageBox>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<MessageBox>()
                .AsImplementedInterfaces();
        }

        private void RegisterWindowsViewModels(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IWindowViewModel>()
                .Where(x => !x.IsAssignableTo<IMessageBoxViewModel>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<MessageBoxViewModel>()
                .AsImplementedInterfaces();
        }

        private void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IView>()
                .AsImplementedInterfaces();
        }

        private void RegisterViewsViewModels(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IViewModel>()
                .AsImplementedInterfaces();
        }
    }
}