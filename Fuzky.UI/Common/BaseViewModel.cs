using Autofac;
using Fuzky.Core.Utils.Interfaces;
using Fuzky.UI.Invokers;
using Fuzky.UI.Windows.Dialog;
using System;

namespace Fuzky.UI.Common
{
    public class BaseViewModel : BaseNotification, IViewModel
    {
        public IView View { get; set; }
        public IWindowViewModel Parent { get; set; }
        public IComponentContext Container { get; set; }

        protected IDialogInvoker DialogInvoker { get; }
        protected IThreadDispatcher Dispatcher { get; }

        public BaseViewModel(IView view, IComponentContext container)
        {
            this.View = view;
            this.View.DataContext = this;

            this.Container = container;
            this.DialogInvoker = this.Container.Resolve<IDialogInvoker>();
            this.Dispatcher = this.Container.Resolve<IThreadDispatcher>();
        }

        protected void ShowDialog<TDialogView>(Action<IDialogViewModel<TDialogView>> dialogSetup = null)
            where TDialogView : IViewModel
        {
            this.Dispatcher.InvokeIfRequired(delegate
            {
                this.DialogInvoker.Show<TDialogView>(this.Parent.Window, dialogSetup);
            });
        }
    }
}