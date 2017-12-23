using System;

namespace Fuzky.Core.Utils
{
    public static class EventAssigner
    {
        public static void Subscribe<T>(this EventSource<T> source, EventHandler<T> handler)
        {
            source.Event += handler;
        }
    }
}