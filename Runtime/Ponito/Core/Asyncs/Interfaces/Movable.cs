using System;
using System.Runtime.CompilerServices;

namespace Ponito.Core.Asyncs.Interfaces
{
    public interface Movable : INotifyCompletion
    {
        Exception Exception   { set; get; }
        bool      IsCompleted { get; }
        bool      MoveNext();
        void      GetResult();
        Movable   GetAwaiter();
    }

    public interface Movable<out T> : Movable
    {
        new T          GetResult();
        new Movable<T> GetAwaiter();
    }
}