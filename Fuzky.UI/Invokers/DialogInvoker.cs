using Autofac;
using Fuzky.Core;
using Fuzky.UI.Common;
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
            where TView : IWindowViewModel
        {
            Show<TView>(parent, null);
        }

        public void Show<TView>(IWindow parent, Action<TView> dialogSetup)
            where TView : IWindowViewModel
        {
            var dialogModel = this.container.Resolve<TView>();
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