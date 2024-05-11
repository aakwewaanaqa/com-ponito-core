using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncs.Interfaces
{
    public interface Movable : INotifyCompletion
    {
        bool    IsCompleted { get; }
        bool    MoveNext();
        void    GetResult();
        Movable GetAwaiter();
    }

    public interface Movable<T> : Movable
    {
        bool           IsCompleted { get; }
        bool           MoveNext();
        new T          GetResult();
        new Movable<T> GetAwaiter();
    }
}