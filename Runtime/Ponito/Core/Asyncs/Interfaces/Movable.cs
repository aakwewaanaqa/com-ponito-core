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
}