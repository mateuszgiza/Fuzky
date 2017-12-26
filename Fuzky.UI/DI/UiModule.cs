using Autofac;
using Fuzky.UI.Common;
using Fuzky.UI.Invokers;
using Fuzky.UI.Utils;
using Fuzky.UI.Windows.Dialog;

namespace Fuzky.UI.DI
{
    public class UiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterWindows(builder);
            this.RegisterWindowsViewModels(builder);
            this.RegisterViews(builder);
            this.RegisterViewsViewModels(builder);
            this.RegisterInvokers(builder);
            this.RegisterUtils(builder);
        }

        private void RegisterWindows(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IWindow>()
                .Where(x => !x.IsAssignableTo<IDialog>())
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterType<Dialog>()
                .AsImplementedInterfaces();
        }

        private void RegisterWindowsViewModels(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IWindowViewModel>()
                //.Where(x => x.IsAssignableTo(typeof(IDialogViewModel<>)))
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterGeneric(typeof(DialogViewModel<>))
                .As(typeof(IDialogViewModel<>));
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

        private void RegisterInvokers(ContainerBuilder builder)
        {
            builder.RegisterType<DialogInvoker>()
                .AsImplementedInterfaces();
        }

        private void RegisterUtils(ContainerBuilder builder)
        {
            builder.RegisterType<ThreadDispatcher>()
                .AsImplementedInterfaces();
        }
    }
}