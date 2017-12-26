using System;

namespace Fuzky.Core.Utils.Interfaces
{
    public interface IThreadDispatcher
    {
        void InvokeIfRequired(Action action);
    }
}