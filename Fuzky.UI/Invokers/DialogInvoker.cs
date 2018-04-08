using Autofac;
using Fuzky.Core;
using Fuzky.UI.Common;
using Fuzky.UI.Windows.Dialog;
using System;

namespace Fuzky.UI.Invokers
{
    public class DialogInvoker : IDialogInvoker
    {
        private readonly IComponentContext container;

        public DialogInvoker(IComponentContext container)
        {
            this.container = container;
        }

        public void Show<TView>(IWindow parent)
            where TView : IViewModel
        {
            Show<TView>(parent, null);
        }

        public void Show<TView>(IWindow parent, Action<IDialogViewModel<TView>> dialogSetup)
            where TView : IViewModel
        {
            var dialogModel = this.container.Resolve<IDialogViewModel<TView>>();
            dialogSetup?.Invoke(dialogModel);

            dialogModel.Window.Owner = parent;

            this.BlurWindow(parent);
            var result = dialogModel.Window.ShowDialog();
            this.ClearEffect(parent);
        }

        private void BlurWindow(IWindow window)
        {
            var effect = this.container.ResolveKeyed<object>(DiKeys.Effects.Blur);
            window.Effect = effect;
        }

        private void ClearEffect(IWindow window)
        {
            window.Effect = null;
        }
    }
}