using Ponito.Core.Asyncs.Compilations;
using Ponito.Core.Asyncs.Interfaces;

namespace Ponito.Core.Asyncs
{
    public interface Awaitable<out T> : Movable where T : Movable
    {
        public T    GetAwaiter();
        public bool IsCompleted { get; }
    }
}