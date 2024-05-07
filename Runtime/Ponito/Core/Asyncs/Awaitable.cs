using Ponito.Core.Asyncs.Compilations;

namespace Ponito.Core.Asyncs
{
    public interface Awaitable<out T> : Movable
    {
        public T    GetAwaiter();
        public bool IsCompleted { get; }
    }
}