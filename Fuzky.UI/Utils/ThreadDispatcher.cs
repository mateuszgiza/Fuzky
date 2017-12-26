using Fuzky.Core.Utils.Interfaces;
using System;
using System.Windows;

namespace Fuzky.UI.Utils
{
    public class ThreadDispatcher : IThreadDispatcher
    {
        public void InvokeIfRequired(Action action)
        {
            var invokeRequired = !Application.Current.Dispatcher.CheckAccess();
            if (invokeRequired)
            {
                Application.Current.Dispatcher.Invoke(action);
            }
            else
            {
                action.Invoke();
            }
        }
    }
}