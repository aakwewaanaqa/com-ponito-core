using System;

namespace Ponito.Core.Asyncs
{
    public interface Awaitable<out T>
    {
        public T    GetAwaiter();
        public bool IsCompleted { get; }
        public void OnCompleted(Action continuation);
    }
}