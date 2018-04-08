using Fuzky.UI.Common;
using Fuzky.UI.Windows.Dialog;
using System;

namespace Fuzky.UI.Invokers
{
    public interface IDialogInvoker
    {
        void Show<TView>(IWindow parent)
            where TView : IViewModel;

        void Show<TView>(IWindow parent, Action<IDialogViewModel<TView>> dialogSetup)
            where TView : IViewModel;
    }
}