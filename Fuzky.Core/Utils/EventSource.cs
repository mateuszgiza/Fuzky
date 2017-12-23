using System;

namespace Fuzky.Core.Utils
{
    public class EventSource<T>
    {
        public event EventHandler<T> Event = delegate {};

        public void Raise(object sender, T args) => Event.Invoke(sender, args);
    }
}