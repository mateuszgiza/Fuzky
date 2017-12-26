using Fuzky.UI.Common;
using System;

namespace Fuzky.UI.Invokers
{
    public interface IDialogInvoker
    {
        void Show<TView>(IWindow parent)
            where TView : IWindowViewModel;

        void Show<TView>(IWindow parent, Action<TView> dialogSetup)
            where TView : IWindowViewModel;
    }
}